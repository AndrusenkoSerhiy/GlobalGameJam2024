using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI{
  public class NavigationButton: MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{
    public int index;
    public void OnPointerClick(PointerEventData eventData){
      GameManager.GameManager.Instance.HouseManager.UpdateRoom(index);
    }

    public void OnPointerEnter(PointerEventData eventData){
      transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData){
      transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.Linear);
    }
  }
}