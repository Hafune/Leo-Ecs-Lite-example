using Core.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core
{
    public class EcsEngine : MonoConstruct
    {
        private readonly EcsWorld _world = new();

        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        private Container _context;

        protected override void Construct(Container context)
        {
            _context = context;
            _context.BindInstanceAs(_world);
        }

        private void Awake()
        {
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);

            _updateSystems
                .Add(new PlayerControllerSystem(_context))
                .Add(new UnitControllerSystem(_context))
                .Add(new UIUpdateInfoSystem());

            _updateSystems.Inject();
            _fixedUpdateSystems.Inject();

            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        private void Update() => _updateSystems.Run();

        private void FixedUpdate() => _fixedUpdateSystems.Run();

        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _fixedUpdateSystems.Destroy();
        }
    }
}