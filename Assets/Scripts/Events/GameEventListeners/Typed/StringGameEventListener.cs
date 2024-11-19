using Events.GameEvents.Typed;
using System;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// String-typed <see cref="GameEventListener"/> to use when there is the need for passing through String values.
    /// </summary>
    public class StringGameEventListener : GameEventListenerBase<String, StringGameEvent, StringEvent>
    {
    }
}