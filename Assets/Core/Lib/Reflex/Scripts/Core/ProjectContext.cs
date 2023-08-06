using Reflex.Injectors;
using UnityEngine;

namespace Reflex.Scripts.Core
{
    [DefaultExecutionOrder(-10000)]
    public class ProjectContext : AContext
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            UnityInjector.BeforeAwakeOfFirstSceneOnly(this);
        }

        public override void InstallBindings(Container container)
        {
            base.InstallBindings(container);
            Debug.Log($"{GetType().Name} Bindings Installed");
        }
    }
}