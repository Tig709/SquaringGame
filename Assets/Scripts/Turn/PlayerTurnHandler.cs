using UnityEngine;
using UnityEngine.Events;

namespace Turn
{
    public class PlayerTurnHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCardsToPlayReached = new();
        //event to do the Reset and add played cards
        [SerializeField] private int cardsToPlay;
        private int _playedCards;

        //Also invoke on wrong card
        private void ResetPlayedCards()
        {
            _playedCards = 0;
        }

        //invoke on card played correct
        private void AddAPlayedCard()
        {
            _playedCards++;
            AreCardsToPlayReached();
        }

        private void AreCardsToPlayReached()
        {
            if (cardsToPlay != _playedCards)
                return;

            ResetPlayedCards();
            onCardsToPlayReached.Invoke();
        }
    }
}