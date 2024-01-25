using UnityEngine;
using UnityEngine.UI;

namespace Uobjects{
  public class Uobject : MonoBehaviour{
    public Uobject SpawnUobject(Vector3 position, Quaternion rotation){
      var uobj = Instantiate(this, position, rotation);
      return uobj;
    }
    
    
    public float interactionTime = 5f;  // Time required for interaction in seconds
    private float currentTime = 0f;
    public bool isInteracting;
    public bool isOpened;

    public Image progressBar;

    public void SetImage(Image img){
      progressBar = img;
    }
    void Update(){
      if (isOpened) return;
      if (isInteracting)
      {
        currentTime += Time.deltaTime;

        // Update progress bar
        float progress = currentTime / interactionTime;
        progressBar.fillAmount = progress;

        if (currentTime >= interactionTime)
        {
          // Interaction complete, open the object or perform desired action
          OpenObject();
          ResetInteraction();
        }
      }
    }

    void OpenObject()
    {
      // Implement the logic to open the object
      isOpened = true;
      Debug.LogError("Object opened!");
    }

    void ResetInteraction()
    {
      currentTime = 0f;
      isInteracting = false;

      // Reset progress bar
      progressBar.fillAmount = 1f;
    }

    public void StartInteraction()
    {
      Debug.LogError("start interaction");
      isInteracting = true;
    }

    public void StopInteract(){
      Debug.LogError("stop interact");
      isInteracting = false;
      ResetInteraction();
    }
  }
}