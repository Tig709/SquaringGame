using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

namespace Cards
{
    /// <summary>
    /// Cardstack: Retrieves its children in a stack, which will be the cardstack. Holds functions for cardstack
    /// </summary>
    public class CardStack : MonoBehaviour
    {
        [SerializeField] private StringListGameEvent onCardTaken;

        private Stack<Card> _cards = new();

        private void Start()
        {
            foreach (Card card in gameObject.GetComponentsInChildren<Card>())
                _cards.Push(card);

            //TODO has to go through event
            OnTakeCard();
        }

        private void OnTakeCard()
        {
            if (_cards.Count == 0 || _cards.Peek() == null)
                return;

            List<string> listOfTexts = new();

            foreach (TextMeshProUGUI textMeshProUGUI in _cards.Peek().GetComponentsInChildren<TextMeshProUGUI>())
                listOfTexts.Add(textMeshProUGUI.text);

            onCardTaken.Invoke(listOfTexts);
        }

        //TODO has to go through event
        private void ShuffleStack()
        {
            _cards.Shuffle();
        }
    }
}