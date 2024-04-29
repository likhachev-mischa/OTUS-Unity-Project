using DI;
using Game.Components;
using Unity.Cinemachine;

namespace Game.Logic
{
    public sealed class CameraCharacterTracker
    {
        [Inject]
        private void Construct(CamerasRegistry camerasRegistry, VisualTransform characterVisuals)
        {
            CinemachineCamera camera = camerasRegistry.GetCinemachineCamera(Cameras.CHARACTER);
            camera.Follow = characterVisuals.Value;
            camera.gameObject.SetActive(true);
        }
    }

}