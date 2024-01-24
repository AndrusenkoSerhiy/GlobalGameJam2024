using System.Collections.Generic;
using Cards;
using Character;
using Location;
using Rooms;
using UnityEngine;

namespace GameManager{
  public class GameManager : MonoBehaviour{
    public static GameManager Instance;

    public LocationData CurrentLocationData;
    
    public List<Actor> ActorsInScene = new();

    public HouseManager HouseManager;
    public List<LocationData> HouseDatas = new(); 

    private void Awake(){
      if (Instance){
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      InitHouseManager();
    }

    public void InitHouseManager(){
      HouseManager.Init();
    }
    public void InitActors(){
      ActorsInScene.ForEach(c=>c.Init());
    }

    public void PlayCard(CardData cardData){
      ActorsInScene.ForEach(c=>c.CheckTags(cardData.TagsList));
    }
  }
}