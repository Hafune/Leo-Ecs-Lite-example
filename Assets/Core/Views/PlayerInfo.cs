using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using TMPro;
using UnityEngine;

namespace Core
{
    public class PlayerInfo : MonoConstruct
    {
        [SerializeField] private TextMeshProUGUI _text;

        private EcsPackedEntityWithWorld _packedEntity;
        private Container _context;
        private EcsWorld _world;

        protected override void Construct(Container context) => _context = context;

        private void Start()
        {
            _world = _context.Resolve<EcsWorld>();
            var entity = _world.NewEntity();
            _packedEntity = _world.PackEntityWithWorld(entity);

            var pool = _world.GetPool<PlayerInfoComponent>();
            ref var component = ref pool.Add(entity);
            component.onChange += OnChange;
        }

        private void OnChange(Vector2 position) => _text.text = position.ToString();

        private void OnDestroy()
        {
            if (_packedEntity.Unpack(out _, out int entity))
                _world.DelEntity(entity);
        }
    }
}