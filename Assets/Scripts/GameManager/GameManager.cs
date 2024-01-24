using System;
using System.Collections.Generic;
using Cards;
using Character;
using Location;
using Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameManager{
  public class GameManager : MonoBehaviour{
    public static GameManager Instance;
    [Header("Current location info")]
    private int CurLocationIndex = 0;
    public LocationData CurrentLocationData;
    
    public List<Actor> ActorsInScene = new();

    [Header("Components")]
    public CardsMonitorUI CardsMonitorUI;
    public HouseManager HouseManager;
    
    [Header("Pools")]
    public List<LocationData> LocationsPool = new();

    private void Awake(){
      if (Instance){
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      Reset(true);
    }

    public void Update(){
      if (Input.GetKeyDown(KeyCode.Space)){
        Reset();
      }
    }

    public void Reset(bool init = false){
      //ClearOld
      ActorsInScene.ForEach(c=>Destroy(c.gameObject));
      ActorsInScene.Clear();
      
      //Generate new
      int curIndex = init ? 0 : CurLocationIndex + 1;
      if (curIndex >= LocationsPool.Count) curIndex = Random.Range(0, LocationsPool.Count-1);
      CurLocationIndex = curIndex;
      CurrentLocationData = LocationsPool[CurLocationIndex];
      InitHouseManager(CurrentLocationData);
      InitCardsMonitor(CurrentLocationData);
    }

    public void InitHouseManager(LocationData locationData){
      HouseManager.Init(locationData);
    }

    public void InitCardsMonitor(LocationData locationData){
      
    }
    public void InitActors(){
      //Spawn
      //codeSpawn
      //Init
      ActorsInScene.ForEach(c=>c.Init());
    }

    public void PlayCard(CardData cardData){
      ActorsInScene.ForEach(c=>c.CheckTags(cardData.TagsList));
    }
  }
}