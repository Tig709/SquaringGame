using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class DiscardStack : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent retrievedCards;

        private Stack<Card> _cards = new();
        private void Awake()
        {
            retrievedCards.AddListener(PushCardIn);
        }
        private void PushCardIn(GameObject card)
        {
            _cards.Push(card.GetComponent<Card>());
            card.transform.SetParent(transform, true);
            card.transform.SetLocalPositionAndRotation(new Vector3(0, 0.0001f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        }

        private void PopCardOut()
        {
            //pop
            //set transform right
        }
    }
}
