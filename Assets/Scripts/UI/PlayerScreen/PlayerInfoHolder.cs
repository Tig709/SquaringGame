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
        private List<int> _playerScores = new();
        public List<string> PlayerNames => _playerNames;
        public List<int> PlayerScores => _playerScores;

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
            for (int i = 0; i < names.Count; i++)
                _playerScores.Add(0);
        }
    }
}
