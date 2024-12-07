using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Set number inbetween 2 strings or on the end or start of a string. Activates on an Event using intgame event, for scoring UI etc.
    /// </summary>
    public class SetNumberInStrings : MonoBehaviour
    {
        [SerializeField] private IntGameEvent onValueChanged;
        [SerializeField] private List<string> basicText;

        private TextMeshProUGUI _textMeshProComponent;

        private void Awake()
        {
            _textMeshProComponent = GetComponent<TextMeshProUGUI>();
            onValueChanged.AddListener(SetTextFromEvent);
        }

        private void SetTextFromEvent(int numberToAddToString)
        {
            if (basicText.Count == 0 || basicText.Count >= 3)
                return;

            if (basicText.Count == 1)
                _textMeshProComponent.text = basicText[0] + numberToAddToString.ToString();

            if (basicText.Count == 2)
                _textMeshProComponent.text = basicText[0] + numberToAddToString.ToString() + basicText[1];
        }
    }
}
