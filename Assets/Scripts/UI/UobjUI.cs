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
    
    private Tweener tween;
    private Camera _camera;
    private Canvas _canvas;

    public void Init(Uobject uobj, Camera camera, Canvas canvas){
      target = uobj;
      target.SetImage(uiImageTest);
      target.InitInventory();
      _camera = camera;
      _canvas = canvas;
    }
    
    void Update()
    {
      if (target != null && uiImage != null)
      {
        var screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, screenPosition, _camera, out localPos);
        uiImage.rectTransform.anchoredPosition = localPos;
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