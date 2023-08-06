using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UIUpdateInfoSystem : IEcsRunSystem
    {
        private EcsFilterInject<
            Inc<
                EventInfoChanged,
                Player1UniqueTag,
                TransformComponent
            >> _playerFilter;

        private EcsFilterInject<
            Inc<
                PlayerInfoComponent
            >> _uiFilter;

        private EcsPoolInject<TransformComponent> _transformPool;
        private EcsPoolInject<PlayerInfoComponent> _playerInfoPool;
        private EcsPoolInject<EventInfoChanged> _eventPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            foreach (var uiEntity in _uiFilter.Value)
            {
                var position = _transformPool.Value.Get(playerEntity).transform.position;
                _playerInfoPool.Value.Get(uiEntity).onChange?.Invoke(position);
            }
        }
    }
}