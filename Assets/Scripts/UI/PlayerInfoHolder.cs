using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Holds names for players, carries over through scenes
    /// </summary>
    public class PlayerInfoHolder : MonoBehaviour
    {
        [SerializeField] private StringListGameEvent eventToSaveNames;

        private List<string> _playerNames = new();
        public List<string> PlayerNames => _playerNames;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            eventToSaveNames.AddListener(saveNames);
        }

        private void saveNames(List<string> names)
        {
            _playerNames = names;
        }
    }
}
