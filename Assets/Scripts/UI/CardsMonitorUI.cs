using Cards;
using UI;
using UnityEngine;

public class CardsMonitorUI : MonoBehaviour{
    public CardUI CardPrefab;
    
    public void AddCard(CardData cardData) {
        var newCard = Instantiate(CardPrefab, transform);
        newCard.Init(cardData);
    }
}
