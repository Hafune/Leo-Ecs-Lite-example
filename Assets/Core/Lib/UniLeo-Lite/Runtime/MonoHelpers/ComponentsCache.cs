using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace Voody.UniLeo.Lite
{
    public class ComponentsCache : MonoBehaviour
    {
        private List<Action<int, EcsWorld, bool>> _cache = new();

        public void ApplyCache(int entity, EcsWorld world)
        {
            MakeCache();

            foreach (var action in _cache)
                action.Invoke(entity, world, false);
        }

        private void MakeCache()
        {
            if (_cache.Count != 0)
                return;

            var list = gameObject.GetComponents<Component>();

            for (int i = 0, count = list.Length; i < count; i++)
            {
                var component = list[i];

                if (component is not BaseMonoProvider entityComponent)
                    continue;

                _cache.Add(entityComponent.Attach);
            }
        }
    }
}