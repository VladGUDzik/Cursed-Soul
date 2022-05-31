using System.Collections.Generic;
using System.Linq;
using Core.CoreComponents;
using UnityEngine;

namespace Core
{
    public class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();

        public void LogicUpdate()
        {
            foreach (var component in _coreComponents)
            {
                component.LogicUpdate();
            }
        }
    
        public void AddComponent(CoreComponent component)
        {
            if (!_coreComponents.Contains(component))
            {
                _coreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = _coreComponents.OfType<T>().FirstOrDefault();
            
            if(comp==null)
                Debug.LogWarning($"{typeof(T)} not found on {transform.gameObject}");

            return comp;
        }

        public T GetCoreComponent<T>(ref T value) where T:CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
