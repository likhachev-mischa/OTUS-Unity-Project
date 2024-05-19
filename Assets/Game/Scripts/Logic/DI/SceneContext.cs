using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DI
{
    public sealed class SceneContext : Context
    {
        [SerializeField]
        private GameInstallerContainer[] sceneInstallers;

        private List<GameInstaller> gameInstallers = new();
        private List<GameObjectInstaller> gameObjectInstallers = new();

        public void RegisterInstaller(GameObjectInstaller installer)
        {
            ExtractListeners(installer);
            ExtractServices(installer);
            ExtractInjectors(installer);

            gameObjectInstallers.Add(installer);
        }

        public void RegisterScene(ServiceLocator serviceLocator, GameManager gameManager)
        {
            this.serviceLocator = serviceLocator;
            this.gameManager = gameManager;

            foreach (GameInstallerContainer gameInstallerContainer in sceneInstallers)
            {
                IEnumerable<GameInstaller> installers = gameInstallerContainer.ProvideInstallers();
                foreach (GameInstaller gameInstaller in installers)
                {
                    gameInstallers.Add(gameInstaller);
                }
            }

            foreach (GameInstaller installer in gameInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }
        }

        public void Inject()
        {
            foreach (GameInstaller installer in gameInstallers)
            {
                ExtractInjectors(installer);
            }

            InjectGameObjectsOnScene();
        }

        public void Unload()
        {
            foreach (GameInstaller installer in gameInstallers)
            {
                RemoveInstaller(installer);
            }

            foreach (GameObjectInstaller gameObjectInstaller in gameObjectInstallers)
            {
                RemoveInstaller(gameObjectInstaller);
            }
        }

        private void RemoveInstaller(object installer)
        {
            if (installer is IGameListenerProvider listenerProvider)
            {
                gameManager.RemoveListeners(listenerProvider.ProvideListeners());
            }

            if (installer is IServiceProvider serviceProvider)
            {
                IEnumerable<(Type, object)> services = serviceProvider.ProvideServices();
                foreach ((Type type, object service) temp in services)
                {
                    serviceLocator.RemoveService(temp.type);
                }
            }
        }
    }
}