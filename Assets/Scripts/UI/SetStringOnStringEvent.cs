using Events.GameEvents.Typed;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SetStringOnStringEvent : MonoBehaviour
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