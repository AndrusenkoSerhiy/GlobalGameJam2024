using System;
using Audio;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI{
  public class CurtainsMonitor : MonoBehaviour{
    public RectTransform MainCurtain;
    public RectTransform LeftCurtain;
    public RectTransform RightCurtain;
    public TMP_Text SummaryLabel;
    public GameObject PlayButton;
    public GameObject NextButton;
    public GameObject LoseLabel;
    public GameObject NextLabel;
    public GameObject StartLabel;
    public NavigationButton PreviousRoom;
    public NavigationButton NextRoom;

    private void Awake(){
      PreviousRoom.gameObject.SetActive(false);
    }
    void SwitchLabels(){
      StartLabel.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.PreGame);
      LoseLabel.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Lose);
      NextLabel.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Win);
      SummaryLabel.gameObject.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Win ||
                                        GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Lose);
    }

    public void ShowCurtains(bool forceHiding = true, Action onCompleteAll = null){
      AudioController.Instance.Curtains();
      
      SwitchLabels();
      
      //right
      float xR = RightCurtain.anchoredPosition.x;
      var endXR = 60f;
      DOTween.To(() => xR, dx => xR = dx, endXR, 2f).SetEase(Ease.Linear).OnUpdate(() => {
        var pos = RightCurtain.anchoredPosition;
        pos.x = xR;
        RightCurtain.anchoredPosition = pos;
      }).OnComplete(() => {
        PlayButton.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.PreGame ||
                             GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Lose);
        NextButton.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Win);
      });
      
      //left
      float xL = LeftCurtain.anchoredPosition.x;
      var endXL = 335f;
      DOTween.To(() => xL, dx => xL = dx, endXL, 1.5f).SetEase(Ease.Linear).OnUpdate(() => {
        var pos = LeftCurtain.anchoredPosition;
        pos.x = xL;
        LeftCurtain.anchoredPosition = pos;
      });
      
      //main
      float yM = MainCurtain.anchoredPosition.y;
      var endYM = yM - 1200f;
      DOTween.To(() => yM, dy => yM = dy, endYM, 1.5f).SetEase(Ease.Linear).OnUpdate(() => {
        var pos = MainCurtain.anchoredPosition;
        pos.y = yM;
        MainCurtain.anchoredPosition = pos;
      }).SetDelay(0.5f).OnComplete(() => {
        if(onCompleteAll != null) onCompleteAll.Invoke();
        if(forceHiding) HideCurtains();
      });
      
    }
    
    public void HideCurtains(){
      GameManager.GameManager.Instance.RoomInfoMonitor.SetInfo(GameManager.GameManager.Instance.HouseManager.CurRoom);
      PreviousRoom.gameObject.SetActive(GameManager.GameManager.Instance.HouseManager.CanPevRoom());
      NextRoom.gameObject.SetActive(GameManager.GameManager.Instance.HouseManager.CanNextRoom());
      PreviousRoom.isClicked = false;
      NextRoom.isClicked = false;
      
      AudioController.Instance.Curtains();
      
      float xR = RightCurtain.anchoredPosition.x;
      var endXR = 400f;
      DOTween.To(() => xR, dx => xR = dx, endXR, 1.5f).SetEase(Ease.Linear).OnUpdate(() => {
        var pos = RightCurtain.anchoredPosition;
        pos.x = xR;
        RightCurtain.anchoredPosition = pos;
      }).SetDelay(0.5f);
      
      float xL = LeftCurtain.anchoredPosition.x;
      var endXL = 0f;
      DOTween.To(() => xL, dx => xL = dx, endXL, 1.5f).SetEase(Ease.Linear).OnUpdate(() => {
        var pos = LeftCurtain.anchoredPosition;
        pos.x = xL;
        LeftCurtain.anchoredPosition = pos;
      }).SetDelay(0.5f);
      
      //main
      float yM = MainCurtain.anchoredPosition.y;
      var endYM = yM + 1200f;
      DOTween.To(() => yM, dy => yM = dy, endYM, 2f).SetEase(Ease.Linear).OnUpdate(() => {
        var pos = MainCurtain.anchoredPosition;
        pos.y = yM;
        MainCurtain.anchoredPosition = pos;
      }).OnComplete(() => {
        PlayButton.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.PreGame ||
                             GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Lose);
        NextButton.SetActive(GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Win);
        SwitchLabels();
      });
    }
  }
}