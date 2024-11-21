using Events.GameEvents.Typed;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerScreen
{
    /// <summary>
    /// Script which submits text fields.
    /// </summary>
    public class NameInputFieldScript : MonoBehaviour
    {
        [SerializeField] private ButtonGameEvent OnAddEvent;
        [SerializeField] private ButtonGameEvent OnEditEvent;
        [SerializeField] private GameObject content;
        [SerializeField] private TMP_InputField inputField;

        private Button _button;

        private void Awake()
        {
            OnAddEvent.AddListener(SaveButtonClicked);
            OnEditEvent.AddListener(EditButtonClicked);
        }

        /// <summary>
        /// Sets inputfield screen on when save button clicked, retrieves button
        /// </summary>
        /// <param name="button">The button which should be retrieved</param>
        private void SaveButtonClicked(Button button)
        {
            content.SetActive(true);
            _button = button;
        }

        /// <summary>
        /// Sets inputfield screen on when edit button clicked, retrieves button and sets input fieldtext to text the button already has.
        /// </summary>
        /// <param name="button">The button which should be retrieved</param>
        private void EditButtonClicked(Button button)
        {
            content.SetActive(true);
            _button = button;
            inputField.text = _button.GetComponentInChildren<TextMeshProUGUI>().text;
        }

        /// <summary>
        /// Reset inputfield screen, sets next name 
        /// </summary>
        public void OnConfirmButtonClicked()
        {
            _button.GetComponentInChildren<TextMeshProUGUI>().text = inputField.text;
            inputField.text = "";
            content.SetActive(false);
        }
    }
}
