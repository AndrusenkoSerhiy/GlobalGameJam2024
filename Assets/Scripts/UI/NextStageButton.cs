using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextStageButton : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{

    public void OnPointerClick(PointerEventData eventData){
        GameManager.GameManager.Instance.NextLocation();
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData){
        transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData){
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.Linear);
    }
}
