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
        [SerializeField] private GameObjectGameEvent emptyTileFound;

        private Stack<Card> _cards = new();

        private void Awake()
        {
            emptyTileFound.AddListener(LayCardsDownFromStack);
        }

        private void Start()
        {
            foreach (Card card in gameObject.GetComponentsInChildren<Card>())
                _cards.Push(card);

            //TODO has to go through event
            OnTakeCard();
        }

        //TODO make on peekcard and make new on take function for players to take
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

        private void LayCardsDownFromStack(GameObject tile)
        {
            Card thisCard = _cards.Pop();
            thisCard.transform.SetParent(tile.transform, true);
            thisCard.transform.SetLocalPositionAndRotation(new Vector3(0, 0.0001f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        }
    }
}