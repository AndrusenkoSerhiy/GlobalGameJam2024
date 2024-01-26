using UnityEngine;

namespace DefaultNamespace{
  public class PeopleAnimation : MonoBehaviour{
    public Animator Animator;
    
    public enum PeopleAnimEnum{
      Idle = 1,
      NotFunnyAtAll = 5,
      Well = 10,
      Lol = 15,
      AHAHAhahH = 20,
      MnahahanahhahHAHA = 25,
    }
    
    private void Awake(){
      PlayPeopleAnimation(PeopleAnimEnum.Idle);
    }
    
    public void PlayPeopleAnimation(PeopleAnimEnum peopleAnimEnum){
      Animator.Play("Default", 0, (int)peopleAnimEnum/(float)25);
      Animator.speed = 0;
    }
    
    /*private void Update(){
      if (Input.GetKeyDown(KeyCode.PageUp)){
        PlayPeopleAnimation(PeopleAnimEnum.Lol);
      }
      if (Input.GetKeyDown(KeyCode.PageDown)){
        PlayPeopleAnimation(PeopleAnimEnum.MnahahanahhahHAHA);
      }
    }*/
  }
}