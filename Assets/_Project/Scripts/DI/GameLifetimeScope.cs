using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PCInputConfig _inputConfig;
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private AudioClip _gameMucic;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameEntryPoint>();

        builder.RegisterComponent(_playerSpawnPoint);
        builder.RegisterComponent(_playerPrefab);
        builder.RegisterComponent(_gameMucic);

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