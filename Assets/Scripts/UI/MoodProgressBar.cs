using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI{
  public class MoodProgressBar : MonoBehaviour{
    public Image uiImage;
    public Actor target;
    public Slider Slider;
    public Vector3 Offset;

    public void Init(Actor actor, CharacterData chardata){
      target = actor;
      Slider.minValue = chardata.MinMood;
      Slider.maxValue = chardata.MaxMood;
    }
    void Update()
    {
      Slider.value = target.CurMood;
      if (target != null && uiImage != null)
      {
        // Convert 3D object's position to screen space using UI camera
        RectTransform canvasRectTransform = uiImage.canvas.GetComponent<RectTransform>();
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position + Offset);

        // Set the UI image's anchored position
        uiImage.rectTransform.anchoredPosition = screenPosition - canvasRectTransform.sizeDelta / 2f;
      }
    }
  }
}