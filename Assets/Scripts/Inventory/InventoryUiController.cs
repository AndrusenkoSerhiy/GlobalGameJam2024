using FarrokhGames.Inventory;
using Inventory.Inventory;
using UnityEngine;

namespace Inventory
{
    internal class InventoryUiController : MonoBehaviour
    {
        public GameObject inventoryRoot;

        public InventoryRenderer playerRenderer;
        public InventoryRenderer lootRenderer;

        public static InventoryUiController Instance;

        private InventoryDefinition _playerInv;

        private void Start()
        {
            if (Instance != null)
                return;

            Hide();

            _playerInv = gameObject.GetComponent<InventoryDefinition>();
            _playerInv.Init();

            playerRenderer.SetInventory(_playerInv.Inventory, InventoryRenderMode.Grid);

            Instance = this;
        }

        public InventoryManager GetPlayerInventory() => _playerInv.Inventory;

        public void Show(InventoryManager uobjInv)
        {
            lootRenderer.SetInventory(uobjInv, InventoryRenderMode.Grid);

            if (!inventoryRoot.activeInHierarchy)
                inventoryRoot.SetActive(true);
        }

        public void Hide()
        {
            if (inventoryRoot == null)
                inventoryRoot = GameObject.Find("InventoryRoot").gameObject;

            if (inventoryRoot.activeInHierarchy)
                inventoryRoot.SetActive(false);
        }

        public void OnCloseClick() => Hide();
    }
}