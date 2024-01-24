using System.Collections.Generic;
using Cards;
using Character;
using Rooms;
using UnityEngine;

namespace Location{
  [CreateAssetMenu(menuName = "GAME/Create Location", fileName = "Location", order = 0)]
  public class LocationData : ScriptableObject{
    public float TimeForCompletion = 100f;
    public CardsPool CardsPool;
    public CharactersPool CharactersPool;
    public int HouseRooms = 0;
    public List<Room> AvailableRooms = new();
  }
}