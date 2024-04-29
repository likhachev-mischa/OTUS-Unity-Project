using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Logic
{
    public enum Cameras
    {
        CHARACTER,
        MAIN
    }

    [Serializable]
    public sealed class CamerasRegistry
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private CinemachineCamera characterCamera;

        [SerializeField]
        private CinemachineCamera otherCamera;

        public CinemachineCamera GetCinemachineCamera(Cameras camera)
        {
            return camera switch
            {
                Cameras.MAIN => otherCamera,
                Cameras.CHARACTER => characterCamera,
                _ => throw new Exception($"Camera with {camera} index was not found!")
            };
        }

        public Camera GetMainCamera()
        {
            return mainCamera;
        }
    }
}