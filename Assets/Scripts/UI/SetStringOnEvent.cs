using Events.GameEvents.Typed;
using TMPro;
using UnityEngine;

namespace ui
{
    public class SetStringOnEvent : MonoBehaviour
    {
        [SerializeField] private StringGameEvent eventToSetOn;
        [SerializeField] private TextMeshProUGUI textToSet;
        [SerializeField] private string textToAddBehind;

        private void Awake()
        {
            eventToSetOn.AddListener(SetString);
        }

        private void SetString(string text)
        {
            textToSet.text = text + textToAddBehind;
        }
    }
}