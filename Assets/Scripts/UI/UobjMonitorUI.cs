using UnityEngine;
using Uobjects;

namespace UI{
  public class UobjMonitorUI : MonoBehaviour{
    public UobjUI UobjUIPrefab;
    
    public void AddUobjUI(Uobject uobj) {
      var newUobjUI = Instantiate(UobjUIPrefab, transform);
      newUobjUI.Init(uobj);
    }

    public void DestroyAllUobjUI(){
      foreach (Transform child in transform) {
        Destroy(child.gameObject);
      }
    }
  }
}