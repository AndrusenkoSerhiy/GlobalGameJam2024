using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character{
  [Serializable]
  public class CharacterPoolElement{
    public CharacterData CardData;
    public int Count = 1;
  }

  [CreateAssetMenu(menuName = "GAME/Create CharactersPool", fileName = "CharactersPool", order = 0)]
  public class CharactersPool : ScriptableObject{
    public List<CharacterPoolElement> AllCharacters = new();

    public List<CharacterData> GetRandomChars(int count){
      List<CharacterData> allChars = new();
      foreach (var charElem in AllCharacters){
        for (int i = 0; i < charElem.Count; i++){
          allChars.Add(charElem.CardData);
        }
      }

      allChars = allChars.OrderBy(r => Random.value).ToList();
      return allChars.GetRange(0, count);
    }
  }
}