using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class PlayerControllerSystem : IEcsRunSystem
    {
        private EcsFilterInject<
            Inc<
                TransformComponent,
                Player1UniqueTag
            >> _filter;

        private EcsFilterInject<
            Inc<
                EventInfoChanged
            >> _eventFilter;

        private EcsPoolInject<TransformComponent> _transformPool;
        private EcsPoolInject<EventInfoChanged> _eventPool;

        private readonly Joystick _joystick;

        public PlayerControllerSystem(Container context)
        {
            _joystick = context.Resolve<Joystick>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _eventFilter.Value)
                _eventPool.Value.Del(entity);

            foreach (var entity in _filter.Value)
            {
                var direction = _joystick.Direction;

                if (direction == Vector2.zero)
                    continue;

                _transformPool.Value.Get(entity).transform.position +=
                    (Vector3)direction * Time.deltaTime;

                _eventPool.Value.Add(entity);
            }
        }
    }
}