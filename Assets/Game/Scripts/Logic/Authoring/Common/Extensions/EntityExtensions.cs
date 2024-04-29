using Unity.Entities;

namespace Game.Utils
{
    public static class EntityExtensions
    {
        public static bool TryGetComponent<T>(this Entity entity, out T component)
            where T : unmanaged, IComponentData
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            if (!entityManager.HasComponent<T>(entity))
            {
                component = default;
                return false;
            }

            entityManager.CompleteDependencyBeforeRO<T>();

            component = entityManager.GetComponentData<T>(entity);
            return true;
        }

        public static T GetComponent<T>(this Entity entity) where T : unmanaged, IComponentData
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.CompleteDependencyBeforeRO<T>();

            return entityManager.GetComponentData<T>(entity);
        }


        public static void SetComponent<T>(this Entity entity, T component) where T : unmanaged, IComponentData
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityManager.CompleteDependencyBeforeRW<T>();
            entityManager.SetComponentData(entity, component);
        }
    }
}