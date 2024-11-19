using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTextColorOnEvent : MonoBehaviour
{
    [SerializeField] private StringListGameEvent eventToSetTextsOn;
    [SerializeField] private List<TextMeshProUGUI> targetTexts;
    [SerializeField] private int indexOfColorElement;

    [SerializeField] private List<string> stringsPerColors;
    [SerializeField] private List<Color> colorsPerStringList;

    private void Awake()
    {
        eventToSetTextsOn.AddListener(SetTexts);
    }

    private void SetTexts(List<string> textsToSet)
    {
        for (int i = 0; i < stringsPerColors.Count; i++)
        {
            if (textsToSet[indexOfColorElement] == stringsPerColors[i])
            {
                foreach (TextMeshProUGUI text in targetTexts)
                {
                    text.color = colorsPerStringList[i];
                }
            }
        }
    }
}
