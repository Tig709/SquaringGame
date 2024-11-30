using Events.GameEvents;
using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Cards
{
    /// <summary>
    /// Discard stacks: contains all functions for a discard stack. Counterpart of cardstack
    /// </summary>
    public class DiscardStack : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onCardRetrieved;
        [SerializeField] private GameEvent otherStackEmptyEvent;
        [SerializeField] private GameObjectListGameEvent sendStackEvent;

        private Stack<Card> _cards = new();
        private void Awake()
        {
            onCardRetrieved.AddListener(PushCardIn);
            otherStackEmptyEvent.AddListener(PopStackOut);
        }
        private void PushCardIn(GameObject card)
        {
            _cards.Push(card.GetComponent<Card>());
            card.transform.SetParent(transform, true);
            card.transform.SetLocalPositionAndRotation(new Vector3(0, 0.0001f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        }

        private void PopStackOut()
        {
            List<GameObject> cardList = _cards.ToGameObjectList();
            while (_cards.Count > 0)
                _cards.Pop();

            sendStackEvent.Invoke(cardList);
        }
    }
}
