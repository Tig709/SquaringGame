using Events.GameEvents;
using TMPro;
using UI.PlayerScreen;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Sets score text on an event. Gets names and scoretext from playerinfoholder
    /// </summary>
    public class SetScoreText : MonoBehaviour
    {
        [SerializeField] private GameEvent eventToSetTextOn;
        [SerializeField] private TextMeshProUGUI targetText;
        private string _textString;

        private void Awake()
        {
            eventToSetTextOn.AddListener(SetText);
        }

        private void Start()
        {
            SetText();
        }

        private void SetText()
        {
            _textString = "Scores:\n";
            for (int i = 0; i < PlayerInfoHolder.Instance.PlayerNames.Count; i++)
                _textString += $"{PlayerInfoHolder.Instance.PlayerNames[i]} : {PlayerInfoHolder.Instance.PlayerScores[i]}\n";

            targetText.text = _textString;
        }
    }
}