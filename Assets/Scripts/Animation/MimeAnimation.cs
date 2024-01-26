using UnityEngine;

namespace DefaultNamespace{
  public class MimeAnimation : MonoBehaviour{
    public Animator Animator;

    public enum MimeAnimEnum{
      Idle = 1,
      Exploring = 30,
      Joke1 = 35,
      Joke2 = 40,
      Joke3 = 45,
      Grabbing = 50,
      Lose = 55,
      Win = 60,
    }

    public MimeAnimEnum mimeAnim;

    private void Awake(){
      PlayMimeAnimation(MimeAnimEnum.Idle);
    }

    public void PlayMimeAnimation(MimeAnimEnum mimeAnimEnum){
      Animator.Play("Default", 0, (int)mimeAnimEnum/(float)60);
      Animator.speed = 0;
    }

    /*private void Update(){
      if (Input.GetKeyDown(KeyCode.PageUp)){
        PlayMimeAnimation(MimeAnimEnum.Joke1);
      }
      if (Input.GetKeyDown(KeyCode.PageDown)){
        PlayMimeAnimation(MimeAnimEnum.Win);
      }
    }*/
  }
}