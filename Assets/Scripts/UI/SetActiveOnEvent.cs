using Events.GameEvents;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Scripts used to set gameobject active / non-active on event.
    /// </summary>
    public class SetActiveOnEvent : MonoBehaviour
    {
        [SerializeField] private GameEvent eventToShowObjectOn;
        [SerializeField] private GameObject objectToSetActive;
        [SerializeField] private bool setOn;

        private void Awake()
        {
            eventToShowObjectOn.AddListener(SetActive);
        }

        private void SetActive()
        {
            objectToSetActive.SetActive(setOn);
        }
    }
}
