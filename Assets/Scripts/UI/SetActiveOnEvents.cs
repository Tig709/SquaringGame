using Events.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SetActiveOnEvents : MonoBehaviour
    {
        [SerializeField] private List<GameEvent> eventsToShowObjectOn;
        [SerializeField] private GameObject objectToSetActive;
        [SerializeField] private bool setOn;

        private void Awake()
        {
            foreach (GameEvent eventToShowObjectOn in eventsToShowObjectOn)
                eventToShowObjectOn.AddListener(SetActive);
        }

        private void SetActive()
        {
            objectToSetActive.SetActive(setOn);
        }
    }
}
