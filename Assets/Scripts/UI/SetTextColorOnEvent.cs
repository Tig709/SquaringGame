using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// This script is used to set text color from another text, by determining what color specific strings of a text should be. 
    /// </summary>
    public class SetTextColorOnEvent : MonoBehaviour
    {
        [SerializeField] private StringListGameEvent eventToSetTextsOn;

        [SerializeField] private List<TextMeshProUGUI> targetTexts;
        [SerializeField] private List<string> stringsPerColors;
        [SerializeField] private List<Color> colorsPerStringList;

        [SerializeField] private int indexOfColorElement;

        private void Awake()
        {
            eventToSetTextsOn.AddListener(SetTexts);
        }

        /// <summary>
        /// Set textscolor from string.
        /// </summary>
        /// <param name="retrievedTexts">Texts which determine color</param>
        private void SetTexts(List<string> retrievedTexts)
        {
            for (int i = 0; i < stringsPerColors.Count; i++)
            {
                if (retrievedTexts[indexOfColorElement] == stringsPerColors[i])
                {
                    foreach (TextMeshProUGUI text in targetTexts)
                        text.color = colorsPerStringList[i];
                }
            }
        }
    }
}
