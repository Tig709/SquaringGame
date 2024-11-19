using System.Collections.Generic;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// GameObjectList-typed <see cref="List<GameObject>"/> to use when there is the need for passing through GameObjectList values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/GameObjectList Event", order = 0)]
    public class GameObjectListGameEvent : GameEventBase<List<GameObject>>
    {
    }
}