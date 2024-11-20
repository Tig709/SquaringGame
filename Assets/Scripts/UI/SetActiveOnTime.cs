using Events.GameEvents;
using System.Collections;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Scripts used to set gameobject active / non-active after time. Timer starts on event or immediately.
    /// </summary>
    public class SetActiveOnTime : MonoBehaviour
    {
        [SerializeField] private GameEvent eventToStartTimer;
        [SerializeField] private GameObject objectToSet;

        [SerializeField] private int time;

        [SerializeField] private bool setOn;
        [SerializeField] private bool startTimerOnEvent;

        private void Awake()
        {
            if (startTimerOnEvent)
                eventToStartTimer.AddListener(StartCountdown);
        }

        private void OnEnable()
        {
            if (!startTimerOnEvent)
                StartCountdown();
        }

        private void StartCountdown()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(time);
            objectToSet.SetActive(setOn);
        }
    }
}
