using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : Context
    {
        [SerializeField]
        private GameInstallerContainer[] projectInstallerContainers;

        private SceneContext sceneContext;
        private readonly List<GameInstaller> projectInstallers = new();

        private ObjectResolver objectResolver;


        public void RegisterProject()
        {
            Initialize();

            foreach (GameInstallerContainer gameInstallerContainer in projectInstallerContainers)
            {
                IEnumerable<GameInstaller> installers = gameInstallerContainer.ProvideInstallers();
                foreach (GameInstaller gameInstaller in installers)
                {
                    projectInstallers.Add(gameInstaller);
                }
            }

            foreach (GameInstaller installer in projectInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }
        }

        public void StartProject()
        {
            foreach (GameInstaller installer in projectInstallers)
            {
                ExtractInjectors(installer);
            }

            InjectGameObjectsOnScene();
        }

        public void RegisterScene()
        {
            sceneContext = FindObjectOfType<SceneContext>();
            serviceLocator.BindService(typeof(Context), sceneContext);
            sceneContext.RegisterScene(serviceLocator, gameManager);
            objectResolver = new ObjectResolver(serviceLocator, gameManager, sceneContext);
            serviceLocator.BindService(typeof(IObjectResolver), objectResolver);
        }

        public void StartScene()
        {
            sceneContext.Inject();
            gameManager.Initialize();
            gameManager.LateLoad();
        }

        public void UnloadScene()
        {
            objectResolver.Dispose();
            serviceLocator.RemoveService<IObjectResolver>();
            sceneContext.Unload();
            serviceLocator.RemoveService<Context>();
        }
    }
}