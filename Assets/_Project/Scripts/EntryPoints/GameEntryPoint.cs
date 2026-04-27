using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    private readonly IObjectResolver _objectResolver;
    private readonly Player _player;
    private readonly Transform _playerSpawnPoint;

    public GameEntryPoint(
        IObjectResolver objectResolver,
        Player player,
        PlayerSpawnPoint spawnPoint)
    {
        _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
        _player = player;
        _playerSpawnPoint = spawnPoint.transform;
    }

    public void Start()
    {
        Player player = _objectResolver.Instantiate(_player);

        player.transform.SetPositionAndRotation(
            _playerSpawnPoint.position,
            _playerSpawnPoint.rotation);
    }
}