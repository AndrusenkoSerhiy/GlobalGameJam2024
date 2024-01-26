using Character;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI{
  public class MoodMonitor : MonoBehaviour{
    public MoodProgressBar ProgressBar;

    public void AddMoodBar(Actor actor, CharacterData chardata){
      var progressbar = Instantiate(ProgressBar, transform);
      progressbar.Init(actor, chardata);
    }
  }
}