using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour{
  protected CardData CardData;
  public Image FaceImage;
  public TMP_Text TextLabel;
  public Canvas Canvas;

  public void Init(CardData cardData){
    cardData = CardData;
    FaceImage.sprite = cardData.FaceSprite;
    TextLabel.text = cardData.NameText;
    Canvas.sortingOrder = 0;
  }

}
