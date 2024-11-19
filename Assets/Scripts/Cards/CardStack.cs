using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    [SerializeField] private StringListGameEvent onCardTaken;
    private Stack<Card> _cards = new();

    void Start()
    {
        foreach (Card card in gameObject.GetComponentsInChildren<Card>())
            _cards.Push(card);

        OnTakeCard();
    }

    private void OnTakeCard()
    {
        if (_cards.Peek() == null)
            return;

        List<string> listOfTexts = new();

        foreach (TextMeshProUGUI textMeshProUGUI in _cards.Peek().GetComponentsInChildren<TextMeshProUGUI>())
        {
            listOfTexts.Add(textMeshProUGUI.text);
        }

        onCardTaken.Invoke(listOfTexts);
    }
}
