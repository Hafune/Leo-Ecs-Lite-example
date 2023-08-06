using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class UnitControllerSystem : IEcsRunSystem
    {
        private EcsFilterInject<
            Inc<
                TransformComponent
            >,
            Exc<Player1UniqueTag>> _filter;

        private EcsPoolInject<TransformComponent> _transformPool;

        private readonly Joystick _joystick;

        public UnitControllerSystem(Container context)
        {
            _joystick = context.Resolve<Joystick>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _transformPool.Value.Get(entity).transform.position -=
                    (Vector3)_joystick.Direction * Time.deltaTime;
            }
        }
    }
}