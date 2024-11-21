using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

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
