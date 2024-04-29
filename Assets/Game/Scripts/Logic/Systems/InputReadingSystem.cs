using Game.Components;
using Game.Logic;
using Unity.Cinemachine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class InputReadingSystem : SystemBase
    {
        private InputAction moveAction;
        private InputAction mouseMoveAction;
        private InputAction attackAction;

        private float distance;
        private Camera mainCamera;

        protected override void OnCreate()
        {
            Entity controller = EntityManager.CreateEntity();
            EntityManager.AddComponent<MoveDirectionController>(controller);
            EntityManager.AddComponent<MousePositionController>(controller);

            moveAction = InputSystem.actions.FindAction("Move");
            mouseMoveAction = InputSystem.actions.FindAction("CharacterLook");
            attackAction = InputSystem.actions.FindAction("Attack");


            mouseMoveAction.performed += OnMouseMoved;
            attackAction.performed += OnAttackPressed;

            RequireForUpdate<ContextComponent>();
        }

        private void OnMouseMoved(InputAction.CallbackContext obj)
        {
            RefRW<MousePositionController> mouseController = SystemAPI.GetSingletonRW<MousePositionController>();
            var mousePosition = obj.ReadValue<Vector2>();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            float3 rayPoint = ray.GetPoint(distance);
            mouseController.ValueRW.Value = new float2(rayPoint.x, rayPoint.z);
        }

        private void OnAttackPressed(InputAction.CallbackContext obj)
        {
            Entity actionPressedEntity = EntityManager.CreateEntity();
            EntityManager.AddComponent<AttackActionPerformedEvent>(actionPressedEntity);
        }


        protected override void OnStartRunning()
        {
            EntityQuery contextQuery = SystemAPI.QueryBuilder().WithAll<ContextComponent>().Build();
            var context = contextQuery.GetSingleton<ContextComponent>();

            var camerasRegistry = context.Context.GetService<CamerasRegistry>();
            distance = camerasRegistry.GetCinemachineCamera(Cameras.CHARACTER).GetComponent<CinemachineOrbitalFollow>()
                .Radius;

            mainCamera = camerasRegistry.GetMainCamera();
        }

        protected override void OnUpdate()
        {
            RefRW<MoveDirectionController> moveController = SystemAPI.GetSingletonRW<MoveDirectionController>();
            var value = moveAction.ReadValue<Vector2>();
            moveController.ValueRW.Value = value.normalized;
        }

        protected override void OnDestroy()
        {
            attackAction.performed -= OnAttackPressed;
            moveAction.performed -= OnMouseMoved;
        }
    }
}