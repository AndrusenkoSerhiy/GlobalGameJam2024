using System.Collections.Generic;
using System.Linq;
using Audio;
using Cards;
using DG.Tweening;
using UI;
using UnityEngine;

public class CardsMonitorUI : MonoBehaviour{
    public CardUI CardPrefab;
    private float delay = 0f;
    private float coolDown = 1f;
    public float curCoolDown = 0f;
    public List<CardUI> currentCards = new();

    public void Clear(){
        currentCards.ForEach(c=>Destroy(c.gameObject));
        currentCards.Clear();
    }
    
    void Update(){
        currentCards.RemoveAll(c => c == null);
        if (delay > 0){
            delay = Mathf.Clamp(delay - Time.deltaTime, 0f, Mathf.Infinity);
        }

        if (currentCards.Any(c => c != null && c.IsPlaying) && curCoolDown <= 0f){
            curCoolDown += coolDown;
        }

        if (curCoolDown > 0f){
            curCoolDown = Mathf.Clamp(curCoolDown - Time.deltaTime, 0f, Mathf.Infinity);
        }
            
        if (curCoolDown > 0f){
            currentCards.ForEach(c => c.CanBePlayed = false);
        }
        else{
            currentCards.ForEach(c => c.CanBePlayed = true);
        }
    }
    
    public void AddCard(CardData cardData) {
        AudioController.Instance.CardFlip();
        
        var newCard = Instantiate(CardPrefab, transform.parent);
        newCard.transform.position = transform.position;
        var pos = newCard.RectTransform.anchoredPosition;
        pos.y -= 250f;
        newCard.RectTransform.anchoredPosition = pos;
        newCard.Init(cardData);
        newCard.SetCustomSorting();

        float curPos = newCard.RectTransform.anchoredPosition.y;
        var endPos = curPos;
        endPos += 250f;
        DOTween.To(() => curPos, x => curPos = x, endPos, 0.5f).SetEase(Ease.InOutBack).OnUpdate(() => {
            var pos = newCard.RectTransform.anchoredPosition;
            pos.y = curPos;
            newCard.RectTransform.anchoredPosition = pos;
        }).OnComplete(() => {
            newCard.transform.SetParent(transform);
            newCard.SetSortingOn();
        }).SetDelay(delay);
        delay += 0.5f;
        currentCards.Add(newCard);
    }
}