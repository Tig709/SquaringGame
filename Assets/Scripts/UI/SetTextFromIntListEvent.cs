using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SetTextFromIntListEvent : MonoBehaviour
    {
        [SerializeField] private IntListGameEvent eventToShowOn;
        [SerializeField] private TextMeshProUGUI targetText;

        private void Awake()
        {
            eventToShowOn.AddListener(SetText);
        }


        private void SetText(List<int> listOfInts)
        {
            if (listOfInts.Count == 2)
                targetText.text = $"Is this card outside or in between {listOfInts[0]} and {listOfInts[1]}?";

            if (listOfInts.Count == 1)
                targetText.text = $"Is this card higher or lower then {listOfInts[0]}?";
        }
    }
}