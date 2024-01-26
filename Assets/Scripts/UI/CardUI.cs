using System;
using Audio;
using Cards;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI {
  public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public CardData CardData;
    public bool IsPlaying;
    public bool CanBePlayed = true;
    [Header("Components")] public RectTransform RectTransform;
    public RectTransform BGRectTransform;
    public Image FaceImage;
    public TMP_Text TextLabel;
    public Canvas Canvas;
    public GraphicRaycaster GraphicRaycaster;
    public GameObject Explosion;
    public GameObject Poison;
    public GameObject textbg;
    private Tweener tween;

    public void Init(CardData cardData) {
      CardData = cardData;
      FaceImage.sprite = cardData.FaceSprite;
      TextLabel.text = cardData.NameText;
      textbg.transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-3, 3) * 3);
    }


    public void SetSortingOn() {
      Canvas.overrideSorting = true;
      Canvas.sortingOrder = 0;
    }

    public void SetCustomSorting() {
      Canvas.overrideSorting = true;
      Canvas.sortingOrder = 5;
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (!CanBePlayed) return;
      IsPlaying = true;

      AudioController.Instance.Joke();
      GameManager.GameManager.Instance.MimePlayer.PlayJoke();
      transform.SetParent(transform.parent.parent);
      GraphicRaycaster.enabled = false;
      SetCustomSorting();
      PlayCardAnim();
    }

    public void PlayCardAnim() {
      float curPos = RectTransform.anchoredPosition.y;
      var endPos = curPos;
      endPos += 250f;
      DOTween.To(() => curPos, x => curPos = x, endPos, 1f).SetEase(Ease.InOutBack).OnUpdate(() => {
        var pos = RectTransform.anchoredPosition;
        pos.y = curPos;
        RectTransform.anchoredPosition = pos;
      }).OnComplete(CheckResult);
    }

    void CheckResult() {
      bool result = GameManager.GameManager.Instance.PlayCard(CardData);
      if (result) PlayMatchAnimation();
      else PlayMismatchAnimation();
    }

    public void PlayMismatchAnimation() {
      AudioController.Instance.CardFail();

      Poison.SetActive(true); //x
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

    public void PlayMatchAnimation() {
      AudioController.Instance.CardSuccess();

      Explosion.SetActive(true);
      Sequence seq = DOTween.Sequence();
      seq.Append(BGRectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.6f, RotateMode.FastBeyond360));
      seq.Append(BGRectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.6f, RotateMode.FastBeyond360));
      seq.Append(BGRectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.3f, RotateMode.FastBeyond360));
      seq.Append(BGRectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.3f, RotateMode.FastBeyond360));
      seq.Append(BGRectTransform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Append(BGRectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.FastBeyond360));
      seq.Play().OnComplete(() => Destroy(gameObject));
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (!CanBePlayed) return;
      //DOTween.Kill(tween);
      Canvas.sortingOrder = 1;
      tween = transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (!CanBePlayed) return;
      //DOTween.Kill(tween);
      Canvas.sortingOrder = 0;
      tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.Linear);
    }
  }
}