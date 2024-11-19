using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTextsOnEvent : MonoBehaviour
{
    [SerializeField] private StringListGameEvent eventToSetTextsOn;
    [SerializeField] private List<TextMeshProUGUI> targetTexts;

    private void Awake()
    {
        eventToSetTextsOn.AddListener(SetTexts);
    }

    private void SetTexts(List<string> textsToSet)
    {
        if (targetTexts.Count != textsToSet.Count)
            return;

        for (int i = 0; i < targetTexts.Count; i++)
            targetTexts[i].text = textsToSet[i];
    }
}
