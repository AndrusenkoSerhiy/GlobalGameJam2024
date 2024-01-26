using UI;
using UnityEngine;

namespace Uobjects {
  public class UobjectsManager : MonoBehaviour {
    public UobjMonitorUI UobjMonitorUI;


    public void AddUIInteract(Uobject uobj) {
      UobjMonitorUI.AddUobjUI(uobj);
    }

    public void DestroyUIInteract() {
      UobjMonitorUI.DestroyAllUobjUI();
    }
  }
}