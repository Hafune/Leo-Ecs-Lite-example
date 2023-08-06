using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Lib
{
    public static class MyLeoEcsExtensions
    {
        public static bool TryGet<T>(this EcsPoolInject<T> poolInject, int entity, out T component) where T : struct
        {
            bool hasComponent = poolInject.Value.Has(entity);
            component = hasComponent ? poolInject.Value.Get(entity) : default;

            return hasComponent;
        }

        public static ref T GetOrInitialize<T>(this EcsPoolInject<T> poolInject, int entity) where T : struct =>
            ref poolInject.Value.Has(entity)
                ? ref poolInject.Value.Get(entity)
                : ref poolInject.Value.Add(entity);

        public static ref T GetOrInitialize<T>(this EcsPool<T> poolInject, int entity) where T : struct =>
            ref poolInject.Has(entity)
                ? ref poolInject.Get(entity)
                : ref poolInject.Add(entity);

        public static void DelIfExist<T>(this EcsPoolInject<T> poolInject, int entity) where T : struct 
        {
            if (!poolInject.Value.Has(entity))
                return;

            poolInject.Value.Del(entity);
        }

        public static void DelIfExist<T>(this EcsPool<T> pool, int entity) where T : struct 
        {
            if (!pool.Has(entity))
                return;

            pool.Del(entity);
        }

        public static void AddIfNotExist<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity))
                return;

            pool.Add(entity);
        }

        public static EcsPackedEntity PackEntity(this EcsWorldInject worldInject, int i) =>
            worldInject.Value.PackEntity(i);

        public static bool HasEntity(this EcsFilter filter, int entity) => filter.GetSparseIndex()[entity] != 0;

        public static int GetFirst(this EcsFilter filter) => filter.GetRawEntities()[0];

        public static bool UnpackEntity<T>(this EcsFilterInject<T> filter, EcsPackedEntityWithWorld packedEntity,
            out int entity)
            where T : struct, IEcsInclude
        {
            entity = -1;

            if (!packedEntity.Unpack(out _, out int targetEntity))
                return false;

            if (!filter.Value.HasEntity(targetEntity))
                return false;

            entity = targetEntity;
            return true;
        }

        public static bool UnpackEntity<T, E>(this EcsFilterInject<T, E> filter, EcsPackedEntityWithWorld packedEntity,
            out int entity)
            where T : struct, IEcsInclude
            where E : struct, IEcsExclude
        {
            entity = -1;

            if (!packedEntity.Unpack(out _, out int targetEntity))
                return false;

            if (!filter.Value.HasEntity(targetEntity))
                return false;

            entity = targetEntity;
            return true;
        }

        public struct Inc<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEcsInclude
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
            where T8 : struct
            where T9 : struct {
            public EcsPool<T1> Inc1;
            public EcsPool<T2> Inc2;
            public EcsPool<T3> Inc3;
            public EcsPool<T4> Inc4;
            public EcsPool<T5> Inc5;
            public EcsPool<T6> Inc6;
            public EcsPool<T7> Inc7;
            public EcsPool<T8> Inc8;
            public EcsPool<T9> Inc9;
        
            public EcsWorld.Mask Fill (EcsWorld world) {
                Inc1 = world.GetPool<T1> ();
                Inc2 = world.GetPool<T2> ();
                Inc3 = world.GetPool<T3> ();
                Inc4 = world.GetPool<T4> ();
                Inc5 = world.GetPool<T5> ();
                Inc6 = world.GetPool<T6> ();
                Inc7 = world.GetPool<T7> ();
                Inc8 = world.GetPool<T8> ();
                Inc9 = world.GetPool<T9> ();
                return world
                    .Filter<T1>()
                    .Inc<T2>()
                    .Inc<T3>()
                    .Inc<T4>()
                    .Inc<T5>()
                    .Inc<T6>()
                    .Inc<T7>()
                    .Inc<T8>()
                    .Inc<T9>();
            }
        }
        
        public struct Inc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IEcsInclude
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
            where T8 : struct
            where T9 : struct
            where T10 : struct {
            public EcsPool<T1> Inc1;
            public EcsPool<T2> Inc2;
            public EcsPool<T3> Inc3;
            public EcsPool<T4> Inc4;
            public EcsPool<T5> Inc5;
            public EcsPool<T6> Inc6;
            public EcsPool<T7> Inc7;
            public EcsPool<T8> Inc8;
            public EcsPool<T9> Inc9;
            public EcsPool<T10> Inc10;
        
            public EcsWorld.Mask Fill (EcsWorld world) {
                Inc1 = world.GetPool<T1> ();
                Inc2 = world.GetPool<T2> ();
                Inc3 = world.GetPool<T3> ();
                Inc4 = world.GetPool<T4> ();
                Inc5 = world.GetPool<T5> ();
                Inc6 = world.GetPool<T6> ();
                Inc7 = world.GetPool<T7> ();
                Inc8 = world.GetPool<T8> ();
                Inc9 = world.GetPool<T9> ();
                Inc10 = world.GetPool<T10> ();
                return world
                    .Filter<T1> ()
                    .Inc<T2> ()
                    .Inc<T3> ()
                    .Inc<T4> ()
                    .Inc<T5> ()
                    .Inc<T6> ()
                    .Inc<T7> ()
                    .Inc<T8> ()
                    .Inc<T9> ()
                    .Inc<T10> ();
            }
        }
    }
}