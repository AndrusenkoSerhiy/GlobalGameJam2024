using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Uobjects;

namespace UI{
  public class UobjUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler, IPointerUpHandler{
    
    public Uobject target;  // Reference to the 3D object
    public Image uiImage;
    public Image uiImageTest;
    [SerializeField] private Vector3 _offset;
    private Tweener tween;

    public void Init(Uobject uobj){
      target = uobj;
      target.SetImage(uiImageTest);
    }
    
    void Update()
    {
      if (target != null && uiImage != null)
      {
        // Convert 3D object's position to screen space using UI camera
        RectTransform canvasRectTransform = uiImage.canvas.GetComponent<RectTransform>();
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);

        // Set the UI image's anchored position
        uiImage.rectTransform.anchoredPosition = screenPosition - canvasRectTransform.sizeDelta / 2f;
      }
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
      if (target == null) return;
      
      target.StartInteraction();
    }

    public void OnPointerUp(PointerEventData eventData){
      target.StopInteract();
    }
  }
}