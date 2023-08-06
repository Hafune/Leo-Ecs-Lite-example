﻿using UnityEngine;
using Reflex.Scripts.Core;
using Reflex.Scripts.Events;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly] // https://docs.unity3d.com/ScriptReference/Scripting.AlwaysLinkAssemblyAttribute.html

namespace Reflex.Injectors
{
    internal static class UnityInjector
    {
#if UNITY_EDITOR
        private static bool _projectContextIsReady;

        internal static void InitIfNotExist()
        {
            if (_projectContextIsReady)
                return;

            //ProjectContext prefab guid
            var path = AssetDatabase.GUIDToAssetPath("6441163d2f294f04383dd08a4458fef6");
            var contextPrefab = AssetDatabase.LoadAssetAtPath<ProjectContext>(path);
            Object.Instantiate(contextPrefab);
            _projectContextIsReady = true;
        }
#endif

        internal static void BeforeAwakeOfFirstSceneOnly(ProjectContext projectContext)
        {
            var projectContainer = CreateProjectContainer(projectContext);
            UnityStaticEvents.OnSceneEarlyAwake += scene =>
            {
                var sceneContainer = CreateSceneContainer(scene, projectContainer);
                SceneInjector.Inject(scene, sceneContainer);
            };

#if UNITY_EDITOR
            _projectContextIsReady = true;
#endif
        }

        private static Container CreateProjectContainer(ProjectContext projectContext)
        {
            var container = ContainerTree.Root = new Container("ProjectContainer");

            Application.quitting += () =>
            {
                ContainerTree.Root = null;
                container.Dispose();
            };

            projectContext.InstallBindings(container);

            return container;
        }

        private static Container CreateSceneContainer(Scene scene, Container projectContainer)
        {
            var container = projectContainer.Scope(scene.name);

            var subscription = scene.OnUnload(() => { container.Dispose(); });

            // If app is being closed, all containers will be disposed by depth first search starting from project container root, see UnityInjector.cs
            Application.quitting += () => { subscription.Dispose(); };

            if (scene.TryFindAtRootObjects<SceneContext>(out var sceneContext))
            {
                sceneContext.InstallBindings(container);
            }

            return container;
        }
    }
}