using System;
using UnityEngine;

namespace Rooms{
  [Serializable]
  public class Room : MonoBehaviour{
    [SerializeField] private int _roomIndex;
    
    public int RoomIndex{
      get => _roomIndex;
      set => _roomIndex = value;
    }
  }
}