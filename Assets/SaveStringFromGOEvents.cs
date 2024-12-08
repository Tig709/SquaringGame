using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SaveStringFromGOEvents : MonoBehaviour
    {
        [SerializeField] private List<GameObjectGameEvent> eventsToSaveOn;

        private GameObject _savedObject;

        public GameObject SavedObject => _savedObject;

        private void Awake()
        {
            foreach (GameObjectGameEvent eventToSaveOn in eventsToSaveOn)
                eventToSaveOn.AddListener(SaveGameObject);
        }

        private void SaveGameObject(GameObject gameObject)
        {
            _savedObject = gameObject;
        }
    }
}
