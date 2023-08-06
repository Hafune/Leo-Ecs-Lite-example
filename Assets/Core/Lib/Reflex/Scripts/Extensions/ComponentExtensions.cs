using System;
using System.Collections.Generic;
using Lib;
using Reflex.Scripts.Enums;
using Reflex.Scripts.Utilities;
using UnityEngine;

namespace Reflex.Scripts.Extensions
{
    internal static class ComponentExtensions
    {
        internal static MonoConstruct[] GetInjectables<T>(this T component, MonoInjectionMode injectionMode)
            where T : Component
        {
            switch (injectionMode)
            {
                case MonoInjectionMode.Single: return new[] { component.GetComponent<MonoConstruct>() };
                case MonoInjectionMode.Object: return component.GetComponents<MonoConstruct>();
                case MonoInjectionMode.Recursive: return component.GetComponentsInChildren<MonoConstruct>(true);
                default: throw new ArgumentOutOfRangeException(nameof(injectionMode), injectionMode, null);
            }
        }
    }
}