using System.Collections.Generic;
using UnityEngine;
using Uobjects;

namespace DefaultNamespace {
  public class MimeAnimation : MonoBehaviour {
    public Animator Animator;
    private bool isInteracting;

    public enum MimeAnimEnum {
      Idle = 1,
      Exploring = 30,
      Joke1 = 35,
      Joke2 = 40,
      Joke3 = 45,
      Grabbing = 50,
      Lose = 55,
      Win = 60,
    }

    private Vector3 startPos;
    private Vector3 startRot;

    public MimeAnimEnum mimeAnim;

    private void Awake() {
      isInteracting = false;
      PlayMimeAnimation(MimeAnimEnum.Idle);
      startPos = transform.position;
      startRot = transform.rotation.eulerAngles;
      prevJoke = -1;
    }

    public void Return() {
      isInteracting = false;
      if (GameManager.GameManager.Instance.GameStage == GameManager.GameManager.GameStageE.Game)
        PlayMimeAnimation(MimeAnimEnum.Idle);
      transform.position = startPos;
      transform.rotation = Quaternion.Euler(startRot);
    }

    public void SetInteract(Uobject uobj) {
      if (isInteracting) return;
      var rand = Random.Range(0, 2);
      if (rand == 0)
        PlayMimeAnimation(MimeAnimEnum.Grabbing);
      else PlayMimeAnimation(MimeAnimEnum.Exploring);
      isInteracting = true;
      var pos = uobj.transform.position;
      pos.y = transform.position.y;
      var dir = pos - transform.position;
      Vector3 newPosition = pos - dir.normalized * 2f;
      // Set the position of A to the new position
      //transform.position = newPosition;
      //transform.position = uobj.transform.position;

      Quaternion targetRotation = Quaternion.LookRotation(dir);
      targetRotation *= Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f); // Preserve Y-axis rotation
      transform.rotation = targetRotation;
      //transform.LookAt(dir);
    }

    private int prevJoke = -1;
    private List<int> jokeAnims = new List<int>() { 0, 1, 2 };

    public void PlayJoke() {
      var listAnims = new List<int>(jokeAnims);
      listAnims.Remove(prevJoke);
      var rand = listAnims[Random.Range(0, 2)];
      prevJoke = rand;
      if (rand == 0)
        PlayMimeAnimation(MimeAnimEnum.Joke1);
      if (rand == 1)
        PlayMimeAnimation(MimeAnimEnum.Joke2);
      else
        PlayMimeAnimation(MimeAnimEnum.Joke3);
    }

    public void PlayMimeAnimation(MimeAnimEnum mimeAnimEnum) {
      Animator.Play("Default", 0, (int)mimeAnimEnum / (float)60);
      Animator.speed = 0;
    }
  }
}