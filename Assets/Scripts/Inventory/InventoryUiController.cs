using FarrokhGames.Inventory;
using Inventory.Inventory;
using UnityEngine;

namespace Inventory
{
    internal class InventoryUiController : MonoBehaviour
    {
        public RectTransform InventoryRoot;
        
        public InventoryRenderer PlayerRenderer;
        public InventoryRenderer LootRenderer;

        public static InventoryUiController Instance;
        
        private InventoryDefinition _playerInv;

        private void Start()
        {
            if (Instance != null)
                return;
            
            Hide();

            _playerInv = gameObject.GetComponent<InventoryDefinition>();
            _playerInv.Init();
            
            PlayerRenderer.SetInventory(_playerInv.Inventory, InventoryRenderMode.Grid);

            Instance = this;
        }

        public void Show(InventoryManager uobjInv)
        {
            LootRenderer.SetInventory(uobjInv, InventoryRenderMode.Grid);
            
            if (!InventoryRoot.gameObject.activeInHierarchy)
                InventoryRoot.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (InventoryRoot.gameObject.activeInHierarchy)
                InventoryRoot.gameObject.SetActive(false);
        }

        public void OnCloseClick() => Hide();
    }
}
