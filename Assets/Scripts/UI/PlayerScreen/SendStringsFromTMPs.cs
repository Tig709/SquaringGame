using Events.GameEvents.Typed;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.PlayerScreen
{
    /// <summary>
    /// Fires editor assigned string list through event
    /// </summary>
    public class SendStringsFromTMPs : MonoBehaviour
    {
        [SerializeField] private StringListGameEvent eventToFire;
        [SerializeField] private List<TextMeshProUGUI> textsToSend = new List<TextMeshProUGUI>();
        private List<string> _stringList = new List<string>();

        public void FireStringListEvent()
        {
            eventToFire.Invoke(TextsToStrings());
        }

        private List<string> TextsToStrings()
        {
            _stringList.Clear();
            foreach (TextMeshProUGUI tmp in textsToSend)
            {
                if (tmp.gameObject.activeInHierarchy)
                    _stringList.Add(tmp.text);
            }
            return _stringList;
        }
    }
}
