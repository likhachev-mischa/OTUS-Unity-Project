using Game.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Utils
{
    public static class BakingUtils
    {
        public static BlobAssetReference<Curve> CreateCurveComponent(AnimationCurve authoringCurve,
            float curvePrecision)
        {
            var builder = new BlobBuilder(Allocator.Temp);
            ref Curve curve = ref builder.ConstructRoot<Curve>();
            int count = (int)math.ceil(1 / curvePrecision) + 1;

            BlobBuilderArray<float> arrayBuilder = builder.Allocate(ref curve.Array, count + 1);
            arrayBuilder[0] = authoringCurve.Evaluate(curvePrecision * 0.5f);
            arrayBuilder[count] = 1;

            for (var i = 1; i < count; ++i)
            {
                arrayBuilder[i] = authoringCurve.Evaluate(curvePrecision * i);
            }

            BlobAssetReference<Curve> result = builder.CreateBlobAssetReference<Curve>(Allocator.Persistent);

            builder.Dispose();
            return result;
        }

        public static BlobAssetReference<T> CreateInitialComponent<T>(T authoringComponent) where T : unmanaged
        {
            var builder = new BlobBuilder(Allocator.Temp);
            ref T initialComponent = ref builder.ConstructRoot<T>();
            initialComponent = authoringComponent;
            BlobAssetReference<T> blobReference = builder.CreateBlobAssetReference<T>(Allocator.Persistent);
            builder.Dispose();
            return blobReference;
        }
    }
}