using Events.GameEvents;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// switches text color on events
    /// </summary>
    public class ChangeTextColorThroughEvent : MonoBehaviour
    {
        [SerializeField] private GameEvent event1;
        [SerializeField] private GameEvent event2;

        [SerializeField] private Color color1;
        [SerializeField] private Color color2;

        [SerializeField] private TextMeshProUGUI targetText;

        private void Awake()
        {
            event1.AddListener(SetTextColor1);
            event2.AddListener(SetTextColor2);
        }

        private void SetTextColor1()
        {
            targetText.color = color1;
        }

        private void SetTextColor2()
        {
            targetText.color = color2;
        }
    }
}
