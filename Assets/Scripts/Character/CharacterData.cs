using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Character{
  [CreateAssetMenu(menuName = "GAME/Create CharacterData", fileName = "CharacterData", order = 0)]
  public class CharacterData : ScriptableObject{
    public int MaxMood;
    public int MinMood;
    public List<Tag> PreferedTags = new();
    public List<Tag> HatedTags = new();
    public Actor Actor;
  }
}