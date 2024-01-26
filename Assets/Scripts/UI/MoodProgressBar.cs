using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class MoodProgressBar : MonoBehaviour {
    public Image uiImage;
    public Actor target;
    public Slider Slider;
    public Vector3 Offset;
    public Camera _cam;
    public Canvas _canvas;

    public void Init(Actor actor, CharacterData chardata, Camera camera, Canvas canvas) {
      target = actor;
      Slider.minValue = chardata.MinMood;
      Slider.maxValue = chardata.MaxMood;
      _cam = camera;
      _canvas = canvas;
    }

    void Update() {
      Slider.value = target.CurMood;
      if (target != null && uiImage != null) {
        var screenPosition = Camera.main.WorldToScreenPoint(target.transform.position + Offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, screenPosition, _cam, out localPos);
        uiImage.rectTransform.anchoredPosition = localPos;
      }
    }
  }
}