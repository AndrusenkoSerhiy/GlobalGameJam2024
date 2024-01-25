using System;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Joint = Uobjects.Joint;

namespace Rooms{
  [Serializable]
  public class Room : MonoBehaviour{
    [SerializeField] private int _roomIndex;
    
    public int RoomIndex{
      get => _roomIndex;
      set => _roomIndex = value;
    }

    public List<Transform> SpawnPoints = new();
    public List<Actor> ActorsInRoom = new();
    public List<Joint> Joints = new();
    
    public void InitJoints(){
      foreach (var joint in Joints){
        joint.Init();
      }
    }

    public void Clear(){
      //clear actor
      ActorsInRoom.ForEach(e => Destroy(e.gameObject));
      ActorsInRoom.Clear();
      //clear ui interact
      GameManager.GameManager.Instance.UobjectsManager.UobjMonitorUI.DestroyAllUobjUI();
    }
  }
}