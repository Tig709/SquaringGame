using Events.GameEvents.Typed;
using System.Collections.Generic;
using UnityEngine;

public class SaveStringsFromGOListEvent : MonoBehaviour
{
    [SerializeField] private GameObjectListGameEvent eventToSaveOn;

    private List<GameObject> _savedObjects = new();

    public List<GameObject> SavedObjects => _savedObjects;

    private void Awake()
    {
        eventToSaveOn.AddListener(SaveList);
    }

    private void SaveList(List<GameObject> listOfObjects)
    {
        _savedObjects = listOfObjects;
    }
}
