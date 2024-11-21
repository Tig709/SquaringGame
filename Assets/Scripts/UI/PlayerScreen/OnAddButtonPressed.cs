using Events.GameEvents.Typed;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerScreen
{
    /// <summary>
    /// Script for the add button of the player screen. Retrieves button and passes it on click
    /// </summary>
    public class OnAddButtonPressed : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onNextInListObjectFound;
        [SerializeField] private ButtonGameEvent onAddButtonPressed;

        private Button _button;
        private void Awake()
        {
            onNextInListObjectFound.AddListener(RetrieveObjectToInvoke);
        }

        public void OnAddButtonClicked()
        {
            onAddButtonPressed.Invoke(_button);
        }

        /// <summary>
        /// Retrieves object to invoke, also checks in children
        /// </summary>
        /// <param name="object">the object to invoke</param>
        private void RetrieveObjectToInvoke(GameObject @object)
        {
            if (@object.GetComponent<Button>() == null)
            {
                _button = @object.GetComponentInChildren<Button>();
            }
            _button = @object.GetComponent<Button>();
        }
    }
}
