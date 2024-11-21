using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PlayerScreen
{
    /// <summary>
    /// Set next non-active object in list active
    /// </summary>
    public class SetNextInListActive : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onNextInListObjectFound;
        [SerializeField] private List<GameObject> objectsToSetActive = new List<GameObject>();

        private GameObject GetNextActive()
        {
            foreach (GameObject objectToSetActive in objectsToSetActive)
            {
                if (objectToSetActive.activeInHierarchy)
                    continue;

                else
                {
                    onNextInListObjectFound.Invoke(objectToSetActive);
                    return objectToSetActive;
                }
            }
            return null;
        }

        public void SetNextActive()
        {
            GetNextActive().SetActive(true);
        }
    }
}