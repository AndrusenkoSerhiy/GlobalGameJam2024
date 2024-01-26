using System.Collections.Generic;
using System.Data;
using Cards;
using Character;
using Location;
using UnityEngine;
using Uobjects;
using Random = UnityEngine.Random;

namespace Rooms{
  public class HouseManager : MonoBehaviour{
    [SerializeField] private List<Room> _curHouse = new();
    [SerializeField] private Room _curRoom;
    [SerializeField] private Room _prevRoom;

    private List<Room> _availableRoom = new();
    private int _houseRooms;

    private List<Actor> _actors = new();
    private List<Uobject> _uobjects = new();
    private LocationData _locationData;

    public void Init(LocationData locData){
      //ClearPrevious
      //_actors.ForEach(e=>Destroy(e.gameObject));
      _actors.Clear();
      foreach (var room in _curHouse){
        room.Clear();
      }

      _curHouse.ForEach(r => Destroy(r.gameObject));
      _curHouse.Clear();
      _curRoom = null;
      _prevRoom = null;
      _availableRoom.Clear();

      //Init new
      _locationData = locData;
      _houseRooms = _locationData.HouseRooms;
      _availableRoom.AddRange(_locationData.AvailableRooms);
      GenerateHouse();
      SetRoomPos();
      SpawnActors();
    }

    private void GenerateHouse(){
      for (int i = 0; i < _houseRooms; i++){
        var rndRoom = GetRandomRoom();
        var room = Instantiate(rndRoom, new Vector3(0, -40, 0), Quaternion.identity);
        room.RoomIndex = i;
        _curHouse.Add(room);
        room.InitJoints();
      }

      SetCurRoom(0);
    }

    private Room GetRandomRoom(){
      var rndIndex = Random.Range(0, _availableRoom.Count);
      var room = _availableRoom[rndIndex];
      _availableRoom.RemoveAt(rndIndex);
      return room;
    }

    private void SetRoomPos(){
      _curRoom.transform.position = new Vector3(0, 0, -10);
      if (_prevRoom != null) _prevRoom.transform.position = new Vector3(0, -40, -40);
    }

    private void SetCurRoom(int index){
      _prevRoom = _curRoom;
      _curRoom = _curHouse[index];
    }

    private void Update(){
      var index = 0;
      if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) index = -1;
      if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) index = 1;
      if (index == 0) return;
      UpdateRoom(index);
    }

    private void UpdateRoom(int direction){
      if (_curRoom.RoomIndex <= 0 && direction < 0 ||
          _curRoom.RoomIndex >= _curHouse.Count - 1 && direction > 0) return;
      GameManager.GameManager.Instance.CurtainsMonitor.ShowCurtains(true, () => {
        var newIndex = _curRoom.RoomIndex + direction;
        SetCurRoom(newIndex);
        SetRoomPos();
        _curRoom.ActorsInRoom.ForEach(a=>a.InitMood());
      });
    }

    private void SpawnActors(){
      List<Transform> spawnPoint = new();
      foreach (var room in _curHouse){
        spawnPoint.AddRange(room.SpawnPoints);
      }

      var i = 0;
      //create actor ant set in position
      foreach (var charData in GameManager.GameManager.Instance.CharactersInLocation){
        var actor = Instantiate(charData.Actor, spawnPoint[i].position, spawnPoint[i].rotation,
          parent: spawnPoint[i].parent);
        actor.Init(charData);
        _actors.Add(actor);
        i++;
      }

      //add actor to room list
      foreach (var room in _curHouse){
        room.ActorsInRoom.AddRange(_actors.GetRange(0, room.SpawnPoints.Count));
        _actors.RemoveRange(0, room.SpawnPoints.Count);
      }
    }

    public bool PlayCard(CardData cardData){
      int positive = 0;
      int negative = 0;
      foreach (var actor in _curRoom.ActorsInRoom){
        var result = actor.CheckTags(cardData.TagsList);
        if (result) positive++;
        else negative++;
      }

      return positive >= negative;
    }
  }
}