using UnityEngine;

namespace UI
{
    /// <summary>
    /// Set active objects non-active, set non-active objects active
    /// </summary>
    public class SetReverseActive : MonoBehaviour
    {
        public void SetObjectReverseActive(GameObject obj)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
    }
}
