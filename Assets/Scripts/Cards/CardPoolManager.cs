using UnityEngine;
using Cards; // Assuming your CardData class is in the Cards namespace
using System.Collections.Generic;

public class CardPoolManager : MonoBehaviour
{
    public CardsPool cardsPool; // Reference to the CardsPool
    public List<CardData> cardList = new List<CardData>(); // List of CardData objects
}
