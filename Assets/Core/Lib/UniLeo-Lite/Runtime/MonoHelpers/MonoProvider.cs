using UnityEngine;
using Leopotam.EcsLite;
using Lib;

namespace Voody.UniLeo.Lite
{
    public abstract class MonoProvider<T> : BaseMonoProvider where T : struct
    {
        private enum ValueType
        {
            Unknown,
            Auto,
            Simple,
        }

        [SerializeField] public T value;
        private EcsPool<T> _pool;
        private IEcsAutoReset<T> _iValue;
        private ValueType valueType = ValueType.Unknown;

        public override void Attach(int entity, EcsWorld world, bool addIfNotExist)
        {
            if (valueType == ValueType.Unknown)
            {
                _pool = world.GetPool<T>();

                if (value is IResetInProvider and IEcsAutoReset<T> iValue)
                {
                    _iValue = iValue;
                    valueType = ValueType.Auto;
                }
                else
                {
                    valueType = ValueType.Simple;
                }
            }

            if (valueType == ValueType.Auto)
                _iValue.AutoReset(ref value);

            if (addIfNotExist)
                _pool.GetOrInitialize(entity) = value;
            else
                _pool.Add(entity) = value;
        }
    }
}