using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI{
  public class MoodMonitor : MonoBehaviour{
    public MoodProgressBar ProgressBar;
    private List<MoodProgressBar> allPB = new();
    public Camera Camera;
    public Canvas Canvas;

    public void ClearAll() {
      allPB.ForEach(c=>Destroy(c.gameObject));
      allPB.Clear();
    }
    
    public void AddMoodBar(Actor actor, CharacterData chardata){
      var progressbar = Instantiate(ProgressBar, transform);
      allPB.Add(progressbar);
      progressbar.Init(actor, chardata,Camera,Canvas);
    }
  }
}