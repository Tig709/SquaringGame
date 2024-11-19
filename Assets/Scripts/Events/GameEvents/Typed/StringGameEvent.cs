using System;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// String-typed <see cref="String"/> to use when there is the need for passing through String values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/String Event", order = 0)]
    public class StringGameEvent : GameEventBase<String>
    {
    }
}