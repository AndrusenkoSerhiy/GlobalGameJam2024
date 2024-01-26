using System.Collections.Generic;
using System.Linq;
using Audio;
using Cards;
using Character;
using DefaultNamespace;
using Inventory;
using Location;
using Rooms;
using UI;
using UnityEngine;
using Uobjects;
using Random = UnityEngine.Random;

namespace GameManager{
  public class GameManager : MonoBehaviour{
    public static GameManager Instance;

    public enum GameStageE{
      PreGame = 0,
      Game = 1,
      Lose = 2,
      Win = 3
    }

    public GameStageE GameStage;

    [Header("Current location info")] private int CurLocationIndex = 0;
    public LocationData CurrentLocationData;
    public MimeAnimation MimePlayer;
    public List<CharacterData> CharactersInLocation = new();
    public List<CardData> CardsInLocation = new();

    [Header("Components")] public HouseManager HouseManager;
    public CardsManager CardsManager;
    public UobjectsManager UobjectsManager;
    public CurtainsMonitor CurtainsMonitor;
    public ScoreCounter ScoreCounter;
    public LocationTimerMonitor LocationTimerMonitor;
    public MoodMonitor MoodMonitor;
    public RoomInfoMonitor RoomInfoMonitor;

    [Header("Pools")] public List<LocationData> LocationsPool = new();

    private void Awake(){
      if (Instance){
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      GameStage = GameStageE.PreGame;
    }

    public void StartGame(){
      ScoreCounter.ResetScore();
      GameStage = GameStageE.Game;
      AudioController.Instance.Play();
      Reset(true);
      CurtainsMonitor.HideCurtains();
    }

    public void CompleteStage(){
      GameStage = GameStageE.Win;
      
      InventoryUiController.Instance.Hide();
      
      var plInv = InventoryUiController.Instance.GetPlayerInventory();
      var itemCount = plInv.allItems.Length;
      var itemCost = plInv.allItems.Sum(item => item.price);
      ScoreCounter.AddScore(itemCost);
      plInv.Clear();
      
      CurtainsMonitor.SummaryLabel.text = $"You sold {itemCount} items for {itemCost} coins";// + ScoreCounter.CurrentScore + " coins";
      CurtainsMonitor.ShowCurtains(false);
      
      AudioController.Instance.Win(); //Lose
    }

    public void LoseGame() {
      GameStage = GameStageE.Lose;
      MimePlayer.PlayMimeAnimation(MimeAnimation.MimeAnimEnum.Lose);
      InventoryUiController.Instance.Hide();
      var plInv = InventoryUiController.Instance.GetPlayerInventory();
      plInv.Clear();
      CurtainsMonitor.SummaryLabel.text = $"You got to jail for stealing. Score : " + ScoreCounter.CurrentScore;
      ScoreCounter.ResetScore();
      CurtainsMonitor.ShowCurtains(false);
      
      AudioController.Instance.Lose();
    }

    public void NextLocation(){
      Reset();
      GameStage = GameStageE.Game;
      CurtainsMonitor.HideCurtains();
    }

    public void Reset(bool init = false){
      //ClearOld
      CardsInLocation.Clear();
      CharactersInLocation.Clear();
      MoodMonitor.ClearAll();
      MimePlayer.PlayMimeAnimation(MimeAnimation.MimeAnimEnum.Idle);
      InventoryUiController.Instance.Hide();
      
      var plInv = InventoryUiController.Instance.GetPlayerInventory();
      plInv.Clear();


      //Generate new
      int curIndex = init ? 0 : CurLocationIndex + 1;
      if (curIndex >= LocationsPool.Count) curIndex = Random.Range(0, LocationsPool.Count - 1);
      CurLocationIndex = curIndex;
      CurrentLocationData = LocationsPool[CurLocationIndex];
      CardsInLocation = CurrentLocationData.CardsPool.GetRandomCards(CurrentLocationData.SessionCardsMaxCount);
      CharactersInLocation =
        CurrentLocationData.CharactersPool.GetRandomChars(CurrentLocationData.SessionCharsMaxCount);
      InitHouseManager(CurrentLocationData);
      InitCardsManager();
      InitUobjectsManager();
      LocationTimerMonitor.Init(CurrentLocationData);
    }

    public bool CheckLoseCondition() {
      if (HouseManager.CurRoom.ActorsInRoom.Any(c => c.CurMood <= 0) ) {
        LoseGame();
        return true;
      }

      return false;
    }

    public void InitHouseManager(LocationData locationData){
      HouseManager.Init(locationData);
    }

    public void InitCardsManager(){
      CardsManager.Init(CardsInLocation);
    }

    public void InitUobjectsManager(){
      //UobjectsManager.Init();
    }

    public bool PlayCard(CardData cardData){
      CardsManager.RemoveFromHand(cardData);
      CardsManager.GetCardsToHand(1);
      //ActorsInScene.ForEach(c=>c.CheckTags(cardData.TagsList));
      return HouseManager.PlayCard(cardData);
    }
  }
}