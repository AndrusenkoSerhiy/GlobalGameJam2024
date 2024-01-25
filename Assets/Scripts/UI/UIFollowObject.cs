using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Uobjects;

namespace UI{
  public class UIFollowObject : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler, IPointerUpHandler{
    
    public Uobject target;  // Reference to the 3D object
    public Image uiImage;
    public Image uiImageTest;
    [SerializeField] private Vector3 _offset;
    private Tweener tween;
    
    private void Awake(){
      target.GetComponent<Uobject>().SetImage(uiImageTest);
    }
    void Update()
    {
      if (target != null)
      {
        // Convert 3D object's position to screen space
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(target.transform.position + _offset);

        // Set the UI image's position to the converted screen space position
        uiImage.transform.position = targetPosition;
      }
    }
    
    public void OnPointerClick(PointerEventData eventData){
      Debug.LogError("click");
    }

    public void OnPointerEnter(PointerEventData eventData){
      DOTween.Kill(tween);
      tween = transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData){
      DOTween.Kill(tween);
      tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.Linear);
    }

    public void OnPointerDown(PointerEventData eventData){
      if (target == null || target.isInteracting || target.isOpened) return;
      
      target.StartInteraction();
    }

    public void OnPointerUp(PointerEventData eventData){
      target.StopInteract();
    }
  }
}