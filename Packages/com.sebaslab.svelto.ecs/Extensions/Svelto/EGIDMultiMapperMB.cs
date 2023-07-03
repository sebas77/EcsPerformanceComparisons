using System;
using System.Runtime.CompilerServices;
using Svelto.DataStructures;
using Svelto.ECS.Hybrid;

namespace Svelto.ECS
{
    /// <summary>
    /// to retrieve an EGIDMultiMapper use entitiesDB.QueryMappedEntities<T>(groups);
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct EGIDMultiMapperMB<T> where T : struct, IEntityViewComponent
    {
        internal EGIDMultiMapperMB(FasterDictionary<ExclusiveGroupStruct,
                SveltoDictionary<uint, T, ManagedStrategy<SveltoDictionaryNode<uint>>, ManagedStrategy<T>, ManagedStrategy<int>>> dictionary)
        {
            _dic = dictionary;
        }

        public int count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _dic.count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Entity(EGID entity)
        {
#if DEBUG && !PROFILE_SVELTO
            if (Exists(entity) == false)
                throw new Exception("EGIDMultiMapper: Entity not found");
#endif
            ref var sveltoDictionary = ref _dic.GetValueByRef(entity.groupID);
            return ref sveltoDictionary.GetValueByRef(entity.entityID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Exists(EGID entity)
        {
            return _dic.TryFindIndex(entity.groupID, out var index)
                 && _dic.GetDirectValueByRef(index).ContainsKey(entity.entityID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEntity(EGID entity, out T component)
        {
            component = default;
            return _dic.TryFindIndex(entity.groupID, out var index)
                 && _dic.GetDirectValueByRef(index).TryGetValue(entity.entityID, out component);
        }

        readonly FasterDictionary<ExclusiveGroupStruct,
            SveltoDictionary<uint, T, ManagedStrategy<SveltoDictionaryNode<uint>>, ManagedStrategy<T>, ManagedStrategy<int>>> _dic;
    }
}