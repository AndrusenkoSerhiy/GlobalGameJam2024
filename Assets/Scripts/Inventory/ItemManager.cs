using System.Collections.Generic;
using Inventory.Inventory;
using Tags;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "_ItemManager", menuName = "Inventory/ItemManager")]
    public class ItemManager : ScriptableObject
    {
        public List<ItemDefinition> Items;
        
        public ItemDefinition GetRandomItemWithTag(Tag tag)
        {
            var list = GetItemsWithTag(tag);
            return list[Random.Range(0, list.Count)];
        }
        
        public List<ItemDefinition> GetItemsWithTag(Tag tag)
        {
            var result = new List<ItemDefinition>();
            
            foreach (var item in Items)
                if (item.Tags.Contains(tag))
                    result.Add(item);

            return result;
        }
    }
}
