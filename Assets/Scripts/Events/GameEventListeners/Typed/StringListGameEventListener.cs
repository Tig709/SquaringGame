using Events.GameEvents.Typed;
using System;
using System.Collections.Generic;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// GameObjectList-typed <see cref="List<String>"/> to use when there is the need for passing through StringList values.
    /// </summary>
    public class StringListGameEventListener : GameEventListenerBase<List<String>, StringListGameEvent, StringListEvent>
    {
    }
}