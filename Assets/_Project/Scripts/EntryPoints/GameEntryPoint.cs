using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    private readonly IObjectResolver _objectResolver;
    private readonly IMusic _music;
    private readonly Player _player;
    private readonly Transform _playerSpawnPoint;
    private readonly AudioClip _clip;

    public GameEntryPoint(
        IObjectResolver objectResolver,
        IMusic music,
        Player player,
        PlayerSpawnPoint spawnPoint,
        AudioClip clip)
    {
        _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
        _music = music ?? throw new ArgumentNullException(nameof(music));
        _player = player != null ? player : throw new ArgumentNullException(nameof(player));
        _clip = clip != null ? clip : throw new ArgumentNullException(nameof(clip));

        if (spawnPoint == null)
            throw new ArgumentNullException(nameof(spawnPoint));

        _playerSpawnPoint = spawnPoint.transform;
    }

    public void Start()
    {
        Player player = _objectResolver.Instantiate(_player);

        player.transform.SetPositionAndRotation(
            _playerSpawnPoint.position,
            _playerSpawnPoint.rotation);

        _music.Play(_clip);
    }
}