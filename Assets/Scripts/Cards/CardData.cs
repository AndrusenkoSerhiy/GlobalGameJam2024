using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "GAME/Create CardData", fileName = "CardData", order = 0)]
  public class CardData : ScriptableObject {
    public string NameText => names[Random.Range(0, names.Count)];
    public List<Tag> TagsList = new();
    [SerializeField] private List<Sprite> sprites = new();
    [SerializeField] private List<string> names = new();
    public Sprite FaceSprite => sprites[Random.Range(0, sprites.Count)];
  }
}