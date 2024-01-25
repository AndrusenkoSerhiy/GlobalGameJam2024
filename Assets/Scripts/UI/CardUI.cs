using Cards;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI{
  public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{
    public CardData CardData;

    [Header("Components")] public RectTransform RectTransform;
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

    public void OnPointerClick(PointerEventData eventData){
      transform.SetParent(transform.parent.parent);
      GraphicRaycaster.enabled = false;
      Canvas.overrideSorting = true;
      Canvas.sortingOrder = 5;
      PlayCardAnim();
      //GameManager.GameManager.Instance.PlayCard(CardData);
      //Destroy(gameObject);
    }

    public void PlayCardAnim(){
      float curPos = RectTransform.anchoredPosition.y;
      var endPos = curPos;
      endPos += 250f;
      DOTween.To(() => curPos, x => curPos = x, endPos, 1f).SetEase(Ease.InOutBack).OnUpdate(() => {
        var pos = RectTransform.anchoredPosition;
        pos.y = curPos;
        RectTransform.anchoredPosition = pos;
      }).OnComplete(CheckResult);
    }

    void CheckResult(){
      var rand = Random.Range(0, 2);
      if(rand == 0) PlayMatchAnimation();
      else PlayMismatchAnimation();
    }

    public void PlayMismatchAnimation(){
      //x
      float x = RectTransform.anchoredPosition.x;
      var endX = x;
      var endX_Start = x - 220f;
      //y
      float y = RectTransform.anchoredPosition.y;
      var endY = y - 550f;
      DOTween.To(() => y, dy => y = dy, endY, 1f).SetEase(Ease.InOutBack).OnUpdate(() => {
        var pos = RectTransform.anchoredPosition;
        pos.y = y;
        pos.x = x;
        RectTransform.anchoredPosition = pos;
      }).OnComplete(() => Destroy(gameObject));
      DOTween.To(() => x, dx => x = dx, endX_Start, 0.4f).SetEase(Ease.InBack);
      DOTween.To(() => x, dx => x = dx, endX, 0.6f).SetEase(Ease.InBack).SetDelay(0.4f);

      //Rotation
      RectTransform.DORotate(new Vector3(0f, 0f, 90f), 1f).SetEase(Ease.Linear);
    }

    public void PlayMatchAnimation(){
      Sequence seq = DOTween.Sequence();
      seq.Append(RectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.6f, RotateMode.FastBeyond360)).SetDelay(0.5f);
      seq.Append(RectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.6f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.3f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.3f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Append(RectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Play().OnComplete(() => Destroy(gameObject));
    }

    public void Clear(){
      Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData){
      DOTween.Kill(tween);
      Canvas.sortingOrder = 1;
      tween = transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData){
      DOTween.Kill(tween);
      Canvas.sortingOrder = 0;
      tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.Linear);
    }
  }
}