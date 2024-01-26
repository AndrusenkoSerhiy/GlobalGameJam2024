using System.Collections;
using System.Collections.Generic;
using Location;
using UnityEngine;
using UnityEngine.UI;

public class LocationTimerMonitor : MonoBehaviour{
  public bool Work = false;

  public float MaxValue;
  public float CurValue;

  [Header("Components")] public Slider Slider;

  // Start is called before the first frame update
  public void Init(LocationData locationData){
    MaxValue = locationData.TimeForCompletion;
    CurValue = MaxValue;
    Slider.minValue = 0;
    Slider.maxValue = MaxValue;
    Slider.value = MaxValue;
    Work = true;
  }

  // Update is called once per frame
  void Update(){
    if (!Work) return;
    if (GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Game){
      if (CurValue > 0f){
        CurValue -= Time.deltaTime;
        Slider.value = CurValue;
      }

      if (CurValue <= 0f){
        GameManager.GameManager.Instance.CompleteStage();
        Work = false;
      }
    }
  }
}