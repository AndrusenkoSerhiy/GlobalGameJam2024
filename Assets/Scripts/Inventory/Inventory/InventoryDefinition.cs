using FarrokhGames.Inventory;
using Loot;
using UnityEngine;

namespace Inventory.Inventory
{
    [RequireComponent(typeof(InventoryRenderer))]
    public class InventoryDefinition : MonoBehaviour
    {
        [SerializeField] private int _width = 6;
        [SerializeField] private int _height = 7;
        
        [Header("FillUp")]
        [SerializeField] private ItemManager _itemManager;
        [SerializeField] private LootPreset _lootPreset;

        void Start()
        {
            var provider = new InventoryProvider(InventoryRenderMode.Grid, -1);

            var inventory = new InventoryManager(provider, _width, _height);

            if (_lootPreset != null)
            {
                var loot = _lootPreset.GetTags();

                foreach (var t in loot)
                    inventory.TryAdd(_itemManager.GetRandomItemWithTag(t).CreateInstance());
            }

            LinkTo(inventory);
        }

        private void LinkTo(InventoryManager inventory)
        {
            GetComponent<InventoryRenderer>().SetInventory(inventory, InventoryRenderMode.Grid);
        }
    }
}
