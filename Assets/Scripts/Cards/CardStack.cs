using Events.GameEvents;
using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Cards
{
    /// <summary>
    /// Cardstack: Retrieves its children in a stack, which will be the cardstack. Holds functions for cardstack
    /// </summary>
    public class CardStack : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent popCardEvent;
        [SerializeField] private GameEvent stackReadyEvent;
        [SerializeField] private GameEvent thisStackEmptyEvent;
        [SerializeField] private GameObjectListGameEvent pushCardsEvent;
        [SerializeField] private List<Card> cards;
        private Stack<Card> _cards = new();

        private void Awake()
        {
            popCardEvent.AddListener(PopCardOut);
            pushCardsEvent.AddListener(PushStackIn);
        }

        private void Start()
        {
            foreach (Card card in cards)
                _cards.Push(card);

            ShuffleStack();
        }

        private void ShuffleStack()
        {
            _cards.Shuffle();
        }

        private void PopCardOut(GameObject newParent)
        {
            if (_cards.Count <= 0)
            {
                thisStackEmptyEvent.Invoke();
                return;
            }

            Card thisCard = _cards.Pop();
            thisCard.transform.SetParent(newParent.transform, true);
            thisCard.transform.SetLocalPositionAndRotation(new Vector3(0, 0.0001f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        }

        private void PushStackIn(List<GameObject> cards)
        {
            foreach (GameObject card in cards)
            {
                _cards.Push(card.GetComponent<Card>());
                card.transform.SetParent(transform, true);
                card.transform.SetLocalPositionAndRotation(new Vector3(0, 0.0001f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
            }
            ShuffleStack();
            stackReadyEvent.Invoke();
        }
    }
}