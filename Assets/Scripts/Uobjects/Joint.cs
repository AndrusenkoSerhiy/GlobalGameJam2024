using System.Collections.Generic;
using UnityEngine;

namespace Uobjects{
  public class Joint : MonoBehaviour{
    public List<Uobject> _Uobjects = new();

    public void Init(){
      foreach (var uobj in _Uobjects){
        uobj.gameObject.SetActive(false);
      }

      //enable random uobject
      var rnd = Random.Range(0, _Uobjects.Count);
      var activeUobj = _Uobjects[rnd];
      activeUobj.gameObject.SetActive(true);
      
      GameManager.GameManager.Instance.UobjectsManager.AddUIInteract(activeUobj);
    }
  }
}