using UnityEngine;
using Uobjects;

namespace UI {
  public class UobjMonitorUI : MonoBehaviour {
    public UobjUI UobjUIPrefab;
    public Camera Camera;
    public Canvas Canvas;

    public void AddUobjUI(Uobject uobj) {
      var newUobjUI = Instantiate(UobjUIPrefab, transform);
      newUobjUI.Init(uobj, Camera, Canvas);
    }

    public void DestroyAllUobjUI() {
      foreach (Transform child in transform) {
        Destroy(child.gameObject);
      }
    }
  }
}