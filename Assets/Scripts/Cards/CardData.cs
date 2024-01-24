using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Cards{
  [CreateAssetMenu(menuName = "GAME/Create CardData", fileName = "CardData", order = 0)]
  public class CardData : ScriptableObject{
    public string NameText;
    public List<Tag> TagsList = new();
    public Sprite FaceSprite;
  }
}