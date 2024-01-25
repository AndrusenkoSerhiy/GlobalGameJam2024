using System.Collections.Generic;
using UnityEngine;

namespace Cards {
  public class CardsManager : MonoBehaviour {
    [Header("Game stats")] public int MaxHandSize = 5;
    public int StartHandSize = 3;

    [Header("Current values")] public List<CardData> InitialCardsPool = new();
    public List<CardData> CurrentCardsPool = new();
    public List<CardData> CurrentHandsPool = new();

    [Header("Components")] public CardsMonitorUI CardsMonitorUI;

    public void Init(List<CardData> cardsList) {
      //Clear
      InitialCardsPool.Clear();
      CurrentCardsPool.Clear();
      CurrentHandsPool.Clear();

      //Init
      InitialCardsPool = new List<CardData>(cardsList);
      CurrentCardsPool = new List<CardData>(cardsList);
      GetCardsToHand(StartHandSize);
    }

    public void RemoveFromHand(CardData cardData) {
      CurrentHandsPool.Remove(cardData);
    }

    public void GetCardsToHand(int count) {
      for (int i = 0; i < count; i++) {
        if (CurrentCardsPool.Count == 0) {
          CurrentCardsPool = new List<CardData>(InitialCardsPool);
          CurrentCardsPool.RemoveAll(c => CurrentHandsPool.Contains(c));
        }

        var nextCard = CurrentCardsPool[i];
        CurrentHandsPool.Add(nextCard);
        CardsMonitorUI.AddCard(nextCard);
        CurrentCardsPool.Remove(nextCard);
      }
    }
  }
}