using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// GameObjectList-typed <see cref="List<Strong>"/> to use when there is the need for passing through StringList values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/StringList Event", order = 0)]
    public class StringListGameEvent : GameEventBase<List<String>>
    {
    }
}