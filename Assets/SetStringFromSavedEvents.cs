using Cards;
using Events.GameEvents;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SetStringFromSavedEvents : MonoBehaviour
    {
        [SerializeField] private SaveStringFromGOEvents SaveStringFromGOEvents;
        [SerializeField] private SaveStringsFromGOListEvent SaveStringsFromGOListEvent;
        [SerializeField] private TextMeshProUGUI targetText;

        private List<string> retrievedTextParts;

        [SerializeField] private GameEvent eventToShowOn;//TODO activation events

        private void Awake()
        {
            eventToShowOn.AddListener(SetText);
        }

        private void SetText()
        {
            retrievedTextParts.Add(SaveStringFromGOEvents.SavedObject.GetComponent<Card>().Value.ToString());
            foreach (GameObject gameObject in SaveStringsFromGOListEvent.SavedObjects)
                retrievedTextParts.Add(gameObject.GetComponent<Card>().Value.ToString());

            if (retrievedTextParts.Count == 3)
                targetText.text = $"Is {retrievedTextParts[0]} outside or in between {retrievedTextParts[1]} and {retrievedTextParts[2]}?";//TODO event should be invoked in other place because now it doesnt send 2 nearest neigbours

            if (retrievedTextParts.Count == 2)
                targetText.text = $"Is {retrievedTextParts[0]} higher or lower then {retrievedTextParts[1]}?";
        }
    }
}