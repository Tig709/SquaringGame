using Events;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Cards
{
    public class InteractableCard : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onCardClickedEventHL;
        [SerializeField] private GameObjectGameEvent onCardClickedEventIO;
        [SerializeField] private GameObjectEvent onCardTurned;

        //TODO make event for clickable now
        private bool _clickableNow = true; // used if card outside of stack and outside of non clickable moment for example when text is on screen
        private bool _turnedAround; // used to turn card around so that a card can not be turned multiple times
        private int _neighbourCount; //used to make cards only clickable when neigbouring

        public bool TurnedAround
        {
            get => _turnedAround;
            set => _turnedAround = value;
        }
        public int NeighbourCount
        {
            get => _neighbourCount;
            set => _neighbourCount = value;
        }

        private void OnMouseDown()
        {
            if (!_clickableNow || _turnedAround || _neighbourCount != 0)
                return;

            OnCardClicked();
        }

        public void OnCardClicked()
        {
            if (_neighbourCount == 1)
                onCardClickedEventHL.Invoke(gameObject);
            else if (_neighbourCount >= 2)
                onCardClickedEventIO.Invoke(gameObject);
        }

        private void TurnAround()
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            _turnedAround = true;
            onCardTurned.Invoke(gameObject);
        }
    }
}
