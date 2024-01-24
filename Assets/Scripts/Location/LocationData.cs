using Cards;
using Character;
using UnityEngine;

namespace Location{
  [CreateAssetMenu(menuName = "GAME/Create Location", fileName = "Location", order = 0)]
  public class LocationData : ScriptableObject{
    public float TimeForCompletion = 100f;
    public CardsPool CardsPool;
    public CharactersPool CharactersPool;
  }
}