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
        }

        //TODO has to go through event
        private void ShuffleStack()
        {
            _cards.Shuffle();
        }

        //TODO check if cardstack empty
        private void LayCardsDownFromStack(GameObject tile)
        {
            Card thisCard = _cards.Pop();
            thisCard.transform.SetParent(tile.transform, true);
            thisCard.transform.SetLocalPositionAndRotation(new Vector3(0, 0.0001f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        }
    }
}