using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms{
  public class HouseManager : MonoBehaviour{
    [SerializeField] private List<Room> _curHouse = new();
    [SerializeField] private Room _curRoom;
    [SerializeField] private Room _prevRoom;
    
    private List<Room> _availableRoom = new();
    private int _houseRooms;
    
    public void Init(){
      var houseData = GameManager.GameManager.Instance.HouseDatas[0];
      _availableRoom.AddRange(houseData.AvailableRooms);
      _houseRooms = houseData.HouseRooms;
      
      GenerateHouse();
      SetRoomPos();
    }

    private void GenerateHouse(){
      for (int i = 0; i < _houseRooms; i++){
        var rndRoom = GetRandomRoom();
        var room = Instantiate(rndRoom, new Vector3(0, -10, 0), Quaternion.identity);
        room.RoomIndex = i;
        _curHouse.Add(room);
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
      _curRoom.transform.position = new Vector3(0,0,-10);
      if (_prevRoom != null) _prevRoom.transform.position = new Vector3(0,-10,-10);
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
      if (_curRoom.RoomIndex <= 0 && direction < 0 || _curRoom.RoomIndex >= _curHouse.Count - 1 && direction > 0) return;
      var newIndex = _curRoom.RoomIndex + direction;
      SetCurRoom(newIndex);
      SetRoomPos();
    }
  }
}