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
    public List<CharacterData> CharactersInLocation = new();
    public List<CardData> CardsInLocation = new();

    [Header("Components")]
    public HouseManager HouseManager;
    public CardsManager CardsManager;
    
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
      CardsInLocation.Clear();
      CharactersInLocation.Clear();
      
      
      //Generate new
      int curIndex = init ? 0 : CurLocationIndex + 1;
      if (curIndex >= LocationsPool.Count) curIndex = Random.Range(0, LocationsPool.Count-1);
      CurLocationIndex = curIndex;
      CurrentLocationData = LocationsPool[CurLocationIndex];
      CardsInLocation = CurrentLocationData.CardsPool.GetRandomCards(CurrentLocationData.SessionCardsMaxCount);
      CharactersInLocation = CurrentLocationData.CharactersPool.GetRandomChars(CurrentLocationData.SessionCharsMaxCount);
      InitHouseManager(CurrentLocationData);
      InitCardsManager();
    }

    public void InitHouseManager(LocationData locationData){
      HouseManager.Init(locationData);
    }

    public void InitCardsManager(){
      CardsManager.Init(CardsInLocation);
    }

    public bool PlayCard(CardData cardData){
      CardsManager.RemoveFromHand(cardData);
      CardsManager.GetCardsToHand(1);
      return HouseManager.PlayCard(cardData);;
    }
  }
}