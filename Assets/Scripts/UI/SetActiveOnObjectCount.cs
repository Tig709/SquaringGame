using Events.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Set object active if x of other objects are active
    /// </summary>
    public class SetActiveOnObjectCount : MonoBehaviour
    {
        [SerializeField] private List<GameEvent> eventsToCheckOn;
        [SerializeField] private List<GameObject> objectsToCheck;
        [SerializeField] private int amountWhichShouldBeActive;
        [SerializeField] private GameObject gameObjectToSet;
        [SerializeField] private GameObject alternateObject;
        [SerializeField] private bool hasAlternateObject;

        private int _activeCount;

        private void Awake()
        {
            foreach (GameEvent eventToCheckOn in eventsToCheckOn)
                eventToCheckOn.AddListener(SetActiveIfEnoughActive);
        }

        private bool AreEnoughActive()
        {
            _activeCount = 0;
            foreach (GameObject objectToCheck in objectsToCheck)
            {
                if (objectToCheck.activeInHierarchy)
                {
                    _activeCount++;
                }
            }

            if (_activeCount >= amountWhichShouldBeActive)
                return true;

            return false;
        }

        private void SetActiveIfEnoughActive()
        {
            gameObjectToSet.SetActive(AreEnoughActive());
            if (hasAlternateObject) 
                alternateObject.SetActive(!AreEnoughActive());
        }
    }
}
