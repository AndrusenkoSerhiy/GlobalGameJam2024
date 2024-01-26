using UnityEngine;
using UnityEditor;
using System.Linq;
using Cards; // Make sure this matches the namespace of your CardData and CardsPool classes

[CustomEditor(typeof(CardPoolManager))]
public class CardPoolManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        CardPoolManager manager = (CardPoolManager)target;

        if (GUILayout.Button("Add Cards to Pool"))
        {
            AddCardsToPool(manager);
        }
    }

    private void AddCardsToPool(CardPoolManager manager)
    {
        if (manager.cardsPool == null)
        {
            Debug.LogError("CardsPool reference is not set.");
            return;
        }

        foreach (CardData card in manager.cardList)
        {
            // Assuming CardPoolElement has a constructor that takes CardData and count
            var newElement = new CardPoolElement { CardData = card, Count = 1 };

            // Add the new element to the CardsPool
            manager.cardsPool.AllCards.Add(newElement);
        }

        // Clear the list after adding
        manager.cardList.Clear();

        // Mark the CardsPool object as dirty so it saves the changes
        EditorUtility.SetDirty(manager.cardsPool);
    }
}
