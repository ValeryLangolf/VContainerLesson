using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private PCInputConfig _inputConfig;
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private AudioClip _gameMucic;
    [SerializeField] private List<EnemySpawnPoint> _spawnPoints;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameEntryPoint>();

        builder.RegisterComponent(_playerSpawnPoint);
        builder.RegisterComponent<IEnemyFactory>(_enemyFactory);
        builder.RegisterInstance(_playerPrefab);
        builder.RegisterInstance(_gameMucic);
        builder.RegisterInstance(_spawnPoints).As<IReadOnlyList<EnemySpawnPoint>>();

        builder.Register<IEnemyPool, EnemyPool>(Lifetime.Scoped);
        builder.Register<Weapon>(Lifetime.Transient);
        builder.Register<IEnemyPositionApplier, EnemyPositionApplier>(Lifetime.Scoped);
        builder.Register<IEnemySpawner, EnemyByMarkersSpawner>(Lifetime.Scoped);

        if (Application.isMobilePlatform)
        {
            builder
                .RegisterComponentOnNewGameObject<MobileInputReader>(
                    Lifetime.Singleton, 
                    nameof(MobileInputReader))
                .As<IInputReader>();
        }
        else
        {
            builder.RegisterEntryPoint<PCInputReader>(Lifetime.Singleton).As<IInputReader>();
            builder.RegisterComponent<IPCInputData>(_inputConfig);
        }
    }
}