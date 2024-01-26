using System;
using System.Collections.Generic;
using DefaultNamespace;
using Tags;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character{
  public class Actor : MonoBehaviour{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private bool randMood;
    [SerializeField] public int CurMood;
    [SerializeField] private PeopleAnimation _peopleAnimation;
    private float timer = 0f;

    public void Init(CharacterData charData){
      characterData = charData;
      InitMood();
    }

    public void InitMood() {
      timer = 0f;
      CurMood = randMood ? Random.Range(characterData.MinMood, characterData.MaxMood) : 0;
      ChangeAnimation();
      RandomRotation();
    }

    void Update() {

      if (CurMood > 0) {
        timer += Time.deltaTime;
        if (timer >= 20f) {
          CurMood--;
          timer = 0f;
        }
      }
      else {
        timer = 0f;
      }
    }


    private void RandomRotation(){
      float randomAngle = Random.Range(-20f, 20f);
      transform.rotation *= Quaternion.Euler(0f, randomAngle, 0f);
    }

    public bool CheckTags(List<Tag> targetTags){
      int matched = 0;
      int dismatched = 0;
      foreach (var tag in targetTags){
        if (characterData.PreferedTags.Contains(tag)) matched++;
        if (characterData.HatedTags.Contains(tag)) dismatched++;
      }
      
      ChangeMood(matched - dismatched);
      return matched > dismatched;
    }

    public void ChangeMood(int count){
      if (count == 0) return;
      CurMood = Mathf.Clamp(CurMood + count, characterData.MinMood, characterData.MaxMood);
      ChangeAnimation();
    }

    public void ChangeAnimation(){
      if (CurMood == 0){
        _peopleAnimation.PlayPeopleAnimation(PeopleAnimation.PeopleAnimEnum.Idle);
        Debug.Log(characterData.name + " in default mood");
        return;
      }

      if (CurMood == characterData.MaxMood){
        var rnd = Random.Range(0, 2);
        _peopleAnimation.PlayPeopleAnimation(rnd==0 ? PeopleAnimation.PeopleAnimEnum.MnahahanahhahHAHA : PeopleAnimation.PeopleAnimEnum.AHAHAhahH );
        Debug.Log(characterData.name + " in razyob mood");
        return;
      }

      if (CurMood == characterData.MinMood){
        _peopleAnimation.PlayPeopleAnimation(PeopleAnimation.PeopleAnimEnum.NotFunnyAtAll);
        Debug.Log(characterData.name + " in shit mood");
        return;
      }

      if (CurMood > 0){
        _peopleAnimation.PlayPeopleAnimation(PeopleAnimation.PeopleAnimEnum.Lol);
        Debug.Log(characterData.name + " in good mood");
        return;
      }

      if (CurMood < 0){
        _peopleAnimation.PlayPeopleAnimation(PeopleAnimation.PeopleAnimEnum.Well);
        Debug.Log(characterData.name + " in bad mood");
      }
    }
  }
}