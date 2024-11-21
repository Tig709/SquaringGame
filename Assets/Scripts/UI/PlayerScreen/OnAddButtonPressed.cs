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
        /// <param name="retrievedObject">the object to invoke</param>
        private void RetrieveObjectToInvoke(GameObject retrievedObject)
        {
            if (retrievedObject.GetComponent<Button>() == null)
                _button = retrievedObject.GetComponentInChildren<Button>();

            else
                _button = retrievedObject.GetComponent<Button>();
        }
    }
}
