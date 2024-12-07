using Events.GameEvents;
using Events.GameEvents.Typed;
using UnityEngine;
using UnityEngine.Events;

namespace Turn
{
    /// <summary>
    /// Holds functions for a player's turn
    /// </summary>
    public class PlayerTurnHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCardsToPlayReached = new();
        [SerializeField] private GameEvent onWrongAnswerEvent;
        [SerializeField] private GameEvent onRightAnswerEvent;
        [SerializeField] private IntGameEvent onPlayedCardsChanged;
        [SerializeField] private int cardsToPlay;
        private int _playedCards;

        private void Awake()
        {
            onWrongAnswerEvent.AddListener(ResetPlayedCards);
            onRightAnswerEvent.AddListener(AddAPlayedCard);
        }

        private void ResetPlayedCards()
        {
            _playedCards = 0;
            onPlayedCardsChanged.Invoke(_playedCards);
        }

        private void AddAPlayedCard()
        {
            _playedCards++;
            onPlayedCardsChanged.Invoke(_playedCards);
            AreCardsToPlayReached();
        }

        /// <summary>
        /// Checks if player reached cards he needs to play in a turn
        /// </summary>
        private void AreCardsToPlayReached()
        {
            if (cardsToPlay != _playedCards)
                return;

            ResetPlayedCards();
            onCardsToPlayReached.Invoke();
        }
    }
}