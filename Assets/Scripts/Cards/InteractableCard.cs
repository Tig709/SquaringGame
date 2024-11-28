using Events;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Cards
{
    public class InteractableCard : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onCardClickedEvent;
        [SerializeField] private GameObjectEvent onCardTurned;

        //TODO make event for clickable now
        private bool _clickableNow = true; // used if card outside of stack and outside of non clickable moment for example when text is on screen
        private bool _turnedAround; // used to turn card around so that a card can not be turned multiple times
        private bool _hasNeighbour; //used to make cards only clickable when neigbouring

        public bool TurnedAround
        {
            get => _turnedAround;
            set => _turnedAround = value;
        }
        public bool HasNeighbour
        {
            get => _hasNeighbour;
            set => _hasNeighbour = value;
        }
        private void OnMouseDown()
        {
            if (!_clickableNow || _turnedAround || !_hasNeighbour)
                return;

            OnCardClicked();
            TurnAround();
        }

        //TODO handles on card clicked
        private void OnCardClicked()
        {
            onCardClickedEvent.Invoke(gameObject);
            //set buttons for higher / lower active
            //Handle input from those
        }

        //TODO Handles turn around logic.
        private void TurnAround()
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            _turnedAround = true;
            onCardTurned.Invoke(gameObject);
        }
    }
}
