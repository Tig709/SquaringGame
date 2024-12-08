using Events.GameEvents;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Makes string empty after x seconds when event invoked
    /// </summary>
    public class SetEmptyStringOnCountdown : MonoBehaviour
    {
        [SerializeField] private GameEvent eventToStartTimer;

        [SerializeField] private TextMeshProUGUI thisText;

        [SerializeField] private int time;

        private void Awake()
        {
            eventToStartTimer.AddListener(StartCountdown);
        }

        private void StartCountdown()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(time);
            thisText.text = string.Empty;
        }
    }
}
