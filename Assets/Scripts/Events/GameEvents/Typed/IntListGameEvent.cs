using System.Collections.Generic;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// IntList-typed <see cref="List<Int>"/> to use when there is the need for passing through IntList values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/IntList Event", order = 0)]
    public class IntListGameEvent : GameEventBase<List<int>>
    {
    }
}