using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Component = UnityEngine.Component;
using Container = Reflex.Container;

namespace Voody.UniLeo.Lite
{
    public class ConvertToEntity : MonoConstruct
    {
        [Description("В момент его вызова RawEntity уже не валидна")]
        public Action<ConvertToEntity> EntityWasDeleted;

        private List<Action<int, EcsWorld, bool>> _cache = new();
        private EcsPool<EventRemoveEntity> _eventRemovePool;
        private EcsPackedEntityWithWorld _packedEntity;
        private EcsWorld _world;
        private int _startLayer;
        private int _rawEntity = -1;
        private bool _isStarted;
        private Container _context;

        public int RawEntity => _rawEntity;
        public Container Context => _context;
        public EcsPackedEntityWithWorld PackedEntity => _packedEntity;

        protected override void Construct(Container context) => _context = context;

        private void Awake()
        {
            _startLayer = gameObject.layer;
            _world = _context.Resolve<EcsWorld>();
            _eventRemovePool = _world.GetPool<EventRemoveEntity>();
            MakeCache();
        }

        private void Start()
        {
            _isStarted = true;
            OnEnable();
        }

        private void OnEnable()
        {
            if (!_isStarted)
                return;

            gameObject.layer = _startLayer;
            ConnectToWorld();
        }

        private void OnDisable() => DisconnectFromWorld();

        public int ManualConnection()
        {
            _isStarted = true;
            ConnectToWorld();

            return _rawEntity;
        }

        public void RemoveConnectionInfo()
        {
            if (_rawEntity == -1)
                return;

            EntityWasDeleted?.Invoke(this);
            _rawEntity = -1;

            gameObject.SetActive(false);
        }

        private void DisconnectFromWorld()
        {
            if (_rawEntity == -1)
                return;

            _eventRemovePool.AddIfNotExist(_rawEntity);
            RemoveConnectionInfo();
        }

        private void ApplyCache(int entity, EcsWorld world)
        {
            for (int i = 0, count = _cache.Count; i < count; i++)
                _cache[i].Invoke(entity, world, false);
        }

        private void MakeCache()
        {
            var list = gameObject.GetComponents<Component>();

            for (int i = 0, count = list.Length; i < count; i++)
            {
                var component = list[i];

                if (component is not BaseMonoProvider entityComponent)
                    continue;

                _cache.Add(entityComponent.Attach);

                Destroy(component);
            }
        }

        private void ConnectToWorld()
        {
            if (_rawEntity != -1)
                return;

            _rawEntity = _world.NewEntity();
            _packedEntity = _world.PackEntityWithWorld(_rawEntity);

            ApplyCache(_rawEntity, _world);
        }
    }
}