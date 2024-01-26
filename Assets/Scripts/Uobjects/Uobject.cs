using Audio;
using Inventory;
using Inventory.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Uobjects {
  public class Uobject : MonoBehaviour {
    public Uobject SpawnUobject(Vector3 position, Quaternion rotation) {
      var uobj = Instantiate(this, position, rotation);
      return uobj;
    }

    public float interactionTime = 5f; // Time required for interaction in seconds
    private float currentTime = 0f;
    public bool isInteracting;
    public bool isOpened;

    private AudioEmitter _interactSfx;

    private InventoryDefinition _inventoryDef;

    public Image progressBar;

    public void SetImage(Image img) {
      progressBar = img;
    }

    public void InitInventory() {
      _inventoryDef = gameObject.GetComponent<InventoryDefinition>();
      _inventoryDef.Init();
    }

    void Update() {
      if (isInteracting) {
        GameManager.GameManager.Instance.MimePlayer.SetInteract(this);
        if (GameManager.GameManager.Instance.CheckLoseCondition()) {
          ResetInteraction();
          GameManager.GameManager.Instance.UobjectsManager.UobjMonitorUI.DestroyAllUobjUI();
          return;
        }

        currentTime += Time.deltaTime;

        // Update progress bar
        float progress = currentTime / interactionTime;
        progressBar.fillAmount = progress;

        if (currentTime >= interactionTime) {
          // Interaction complete, open the object or perform desired action
          OpenObject();
          ResetInteraction();
        }
      }
    }

    void OpenObject() {
      // Implement the logic to open the object
      isOpened = true;
      ResetInteraction();

      InventoryUiController.Instance.Show(_inventoryDef.Inventory);
    }

    void ResetInteraction() {
      GameManager.GameManager.Instance.MimePlayer.Return();
      currentTime = 0f;
      isInteracting = false;

      // Reset progress bar
      progressBar.fillAmount = 1f;

      if (_interactSfx != null)
        _interactSfx.Stop();
    }

    public void StartInteraction() {
      isInteracting = true;

      _interactSfx = AudioController.Instance.Interact();
    }

    public void StopInteract() {
      GameManager.GameManager.Instance.MimePlayer.Return();
      isInteracting = false;
      //ResetInteraction();
    }
  }
}