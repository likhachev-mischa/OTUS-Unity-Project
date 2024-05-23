using System;
using System.Numerics;
using Game.Components;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Utils
{
    [Serializable]
    public struct SerializableFloat3
    {
        public float x, y, z;

        public SerializableFloat3(float3 value)
        {
            x = value.x;
            y = value.y;
            z = value.z;
        }
    }

    public struct SerializableFloat4
    {
        public float x, y, z, w;

        public SerializableFloat4(float4 value)
        {
            x = value.x;
            y = value.y;
            z = value.z;
            w = value.w;
        }
    }

    [Serializable]
    public struct TransformData
    {
        public SerializableFloat3 Position;
        public SerializableFloat4 Rotation;

        public TransformData(LocalTransform localTransform)
        {
            this.Position = new SerializableFloat3(localTransform.Position);
            this.Rotation = new SerializableFloat4(localTransform.Rotation.value);
        }
    }

    [Serializable]
    public struct EntitySpawnData
    {
        public SpawnerID SpawnerID;
        public TransformData TransformData;
        public Health Health;

        public EntitySpawnData(LocalTransform transformData, Health health, SpawnerID spawnerID)
        {
            this.TransformData = new TransformData(transformData);
            this.Health = health;
            this.SpawnerID = spawnerID;
        }
    }
}