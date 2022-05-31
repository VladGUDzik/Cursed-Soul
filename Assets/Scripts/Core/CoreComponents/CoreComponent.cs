using Interfaces;
using UnityEngine;

namespace Core.CoreComponents
{
    public class CoreComponent : MonoBehaviour,ILogicUpdate
    {
        protected Core Core;

        protected virtual void Awake()
        {
            Core = transform.parent.GetComponent<Core>();

            if (Core == null)
                Debug.LogError("There is no Core on the parent");
            Core.AddComponent(this);
        }

        public virtual void LogicUpdate()
        {
        }
    }
}
