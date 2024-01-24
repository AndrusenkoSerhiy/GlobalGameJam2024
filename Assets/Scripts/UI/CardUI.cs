using Cards;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
  public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler{
    public CardData CardData;
    public Image FaceImage;
    public TMP_Text TextLabel;
    public Canvas Canvas;
    public GraphicRaycaster GraphicRaycaster;
    private Tweener tween;

    public void Init(CardData cardData){
      CardData = cardData;
      FaceImage.sprite = cardData.FaceSprite;
      TextLabel.text = cardData.NameText;
      Canvas.overrideSorting = true;
      Canvas.sortingOrder = 0;
    }

    public void OnPointerClick(PointerEventData eventData) {
      GraphicRaycaster.enabled = false;
      GameManager.GameManager.Instance.PlayCard(CardData);
      Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData) {
      DOTween.Kill(tween);
      Canvas.sortingOrder = 1;
      tween = transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData) {
      DOTween.Kill(tween);
      Canvas.sortingOrder = 0;
      tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.Linear);
      Debug.Log("DeScale anim");
    }
  }
}
