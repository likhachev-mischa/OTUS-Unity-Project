using System.Linq;
using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : Context
    {
        [SerializeField]
        private GameInstallerContainer projectInstallerContainer;

        private SceneContext sceneContext;
        private GameInstaller[] projectInstallers;

        private ObjectResolver objectResolver;


        public void RegisterProject()
        {
            Initialize();


            projectInstallers = projectInstallerContainer.ProvideInstallers().ToArray();

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
            gameManager.StartGame();
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