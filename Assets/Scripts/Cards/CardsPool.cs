using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cards{
  [Serializable]
  public class CardPoolElement{
    public CardData CardData;
    public int Count = 1;
  }
  
  [CreateAssetMenu(menuName = "GAME/Create CardsPool", fileName = "CardsPool", order = 0)]
  public class CardsPool : ScriptableObject{
    public List<CardPoolElement> AllCards = new();

    public List<CardData> GetRandomCards(int count){
      List<CardData> allCards = new();
      foreach (var cardElem in AllCards){
        for (int i = 0; i < cardElem.Count; i++){
          allCards.Add(cardElem.CardData);
        }
      }

      allCards = allCards.OrderBy(r => Random.value).ToList();
      return allCards.GetRange(0, count);
    }
  }
}