using FarrokhGames.Inventory;
using UnityEngine;

namespace Inventory.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
    public class ItemDefinition : ScriptableObject, IInventoryItem
    {
        [Header("View")] 
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite = null;
        
        [SerializeField] private InventoryShape _shape = null;

        [Header("Data")] 
        [SerializeField] private int _price = 0;
        
        [SerializeField] private ItemType _type = ItemType.Utility;
        
        [SerializeField, HideInInspector] private Vector2Int _position = Vector2Int.zero;
        
        public string Name => _name;
        
        public ItemType Type => _type;
        
        public Sprite sprite => _sprite;

        public int width => _shape.width;
        
        public int height => _shape.height;

        public int price => _price;
        
        public Vector2Int position
        {
            get => _position;
            set => _position = value;
        }
        
        public bool IsPartOfShape(Vector2Int localPosition)
        {
            return _shape.IsPartOfShape(localPosition);
        }
        
        public bool canDrop => false;

        /// <summary>
        /// Creates a copy if this scriptable object
        /// </summary>
        public IInventoryItem CreateInstance()
        {
            var clone = ScriptableObject.Instantiate(this);
            clone.name = clone.name.Substring(0, clone.name.Length - 7); // Remove (Clone) from name
            return clone;
        }
    }
}