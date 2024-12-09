using Events.GameEvents;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Sets text on one of the events. You can add a delay in editor
    /// </summary>
    public class SetStringOnEvents : MonoBehaviour
    {

        [SerializeField] private List<GameEvent> eventsToSetStringOn = new List<GameEvent>();
        [SerializeField] private TextMeshProUGUI targetText;
        [SerializeField] private string newText;
        [SerializeField] private float delay;

        private void Awake()
        {
            foreach (GameEvent eventToSetStringOn in eventsToSetStringOn)
                eventToSetStringOn.AddListener(SetText);
        }

        private void SetText()
        {
            StartCoroutine(SetTextAfterSeconds());
        }

        private IEnumerator SetTextAfterSeconds()
        {
            yield return new WaitForSeconds(delay);
            targetText.text = newText;
        }
    }
}
