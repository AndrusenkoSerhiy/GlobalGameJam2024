using System;
using System.Collections.Generic;
using System.Linq;
using Tags;
using UnityEngine;

namespace Loot
{
    [CreateAssetMenu(fileName = "LootPreset", menuName = "Inventory/LootPreset")]
    public class LootPreset : ScriptableObject
    {
        public int capacity;

        public List<LootItemPreset> variants;

        public List<Tag> GetTags()
        {
            var loot = new List<Tag>();
            
            float totalProbability = variants.Aggregate(0f, (current, lootPreset) => current + lootPreset.probability);

            for (int i = 0; i < capacity; i++)
            {
                float randomValue = UnityEngine.Random.Range(0f, totalProbability);
                float cumulativeProbability = 0f;

                foreach (var lootPreset in variants)
                {
                    cumulativeProbability += lootPreset.probability;

                    if (randomValue <= cumulativeProbability)
                    {
                        loot.Add(lootPreset.tag);
                        break;
                    }
                }
            }

            return loot;
        }
    }

    [Serializable]
    public struct LootItemPreset
    {
        public Tag tag;
        [Range(0, 100)] public int probability;
    }
}
