using FarrokhGames.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Inventory
{
    public class InventorySelection : MonoBehaviour
    {
        Text _text;

        void Start()
        {
            _text = GetComponentInChildren<Text>();
            _text.text = string.Empty;

            var allControllers = GameObject.FindObjectsOfType<InventoryController>();

            foreach (var controller in allControllers)
            {
                controller.onItemHovered += HandleItemHover;
            }
        }

        private void HandleItemHover(IInventoryItem item)
        {
            if (item != null)
            {
                _text.text = (item as ItemDefinition).Name;
            }
            else
            {
                _text.text = string.Empty;
            }
        }
    }
}