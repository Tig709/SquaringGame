using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PlayerScreen
{
    /// <summary>
    /// Holds names for players, carries over through scenes
    /// </summary>
    public class PlayerInfoHolder : MonoBehaviour
    {
        // Singleton Instance
        public static PlayerInfoHolder Instance { get; private set; }

        [SerializeField] private StringListGameEvent eventToSaveNames;

        private List<string> _playerNames = new();
        public List<string> PlayerNames => _playerNames;

        private void Awake()
        {
            // Singleton logic
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);

            eventToSaveNames.AddListener(saveNames);
        }

        private void saveNames(List<string> names)
        {
            _playerNames = names;
        }
    }
}
