using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private SpawnerBots _spawnerBots;
    [SerializeField] private Pool _pool;
    [SerializeField] private ResourceDistributor _resourceDistributor;
    [SerializeField] private RemoverResource _removerResource;
    [SerializeField] private FlagCreator _flagCreator;

    public override void InstallBindings()
    {
        Container.Bind<SpawnerBots>().FromInstance(_spawnerBots).AsSingle();
        Container.Bind<Pool>().FromInstance(_pool).AsSingle();
        Container.Bind<ResourceDistributor>().FromInstance(_resourceDistributor).AsSingle();
        Container.Bind<RemoverResource>().FromInstance(_removerResource).AsSingle();
        Container.Bind<FlagCreator>().FromInstance(_flagCreator).AsSingle();
    }
}