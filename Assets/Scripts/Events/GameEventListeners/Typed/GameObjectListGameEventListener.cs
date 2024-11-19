using Events.GameEvents.Typed;
using System.Collections.Generic;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// GameObjectList-typed <see cref="List<UnityEngine.GameObject>"/> to use when there is the need for passing through GameObjectList values.
    /// </summary>
    public class GameObjectListGameEventListener : GameEventListenerBase<List<UnityEngine.GameObject>, GameObjectListGameEvent, GameObjectListEvent>
    {
    }
}