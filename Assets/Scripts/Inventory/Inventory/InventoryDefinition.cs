using FarrokhGames.Inventory;
using Loot;
using UnityEngine;

namespace Inventory.Inventory
{
    public class InventoryDefinition : MonoBehaviour
    {
        [SerializeField] private int _width = 6;
        [SerializeField] private int _height = 7;
        
        [Header("FillUp")]
        [SerializeField] private ItemManager _itemManager;
        [SerializeField] private LootPreset _lootPreset;

        public InventoryManager Inventory;

        public void Init()
        {
            var provider = new InventoryProvider(InventoryRenderMode.Grid, -1);

            Inventory = new InventoryManager(provider, _width, _height);

            if (_lootPreset != null)
                GenerateLoot(Inventory);
        }

        private void GenerateLoot(InventoryManager inventory)
        {
            var loot = _lootPreset.GetTags();

            foreach (var t in loot)
                inventory.TryAdd(_itemManager.GetRandomItemWithTag(t).CreateInstance());
        }
    }
}