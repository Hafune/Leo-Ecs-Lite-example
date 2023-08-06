using System;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    [Serializable]
    public struct TransformComponent : IEcsAutoReset<TransformComponent>
    {
        public Transform transform;
        public ConvertToEntity convertToEntity;

        public void AutoReset(ref TransformComponent c)
        {
            if (!c.transform)
                return;

            c.convertToEntity.RemoveConnectionInfo();
            c.convertToEntity = null;
        }
    }
}