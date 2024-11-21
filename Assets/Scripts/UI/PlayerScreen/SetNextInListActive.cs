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
            foreach (GameObject objectsToSetActive in objectsToSetActive)
            {
                if (objectsToSetActive.activeInHierarchy)
                    continue;

                else
                {
                    onNextInListObjectFound.Invoke(objectsToSetActive);
                    return objectsToSetActive;
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