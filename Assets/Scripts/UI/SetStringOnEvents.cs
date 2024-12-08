using Events.GameEvents;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SetStringOnEvents : MonoBehaviour
    {

        [SerializeField] private List<GameEvent> eventsToSetStringOn = new List<GameEvent>();
        [SerializeField] private TextMeshProUGUI targetText;
        [SerializeField] private string newText;

        private void Awake()
        {
            foreach (GameEvent eventToSetStringOn in eventsToSetStringOn)
                eventToSetStringOn.AddListener(SetText);
        }

        private void SetText()
        {
            targetText.text = newText;
        }
    }
}
