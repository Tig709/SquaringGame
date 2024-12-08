using Events.GameEvents.Typed;
using System.Collections.Generic;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// IntList-typed <see cref="List<int>"/> to use when there is the need for passing through IntList values.
    /// </summary>
    public class IntListGameEventListener : GameEventListenerBase<List<int>, IntListGameEvent, IntListEvent>
    {
    }
}