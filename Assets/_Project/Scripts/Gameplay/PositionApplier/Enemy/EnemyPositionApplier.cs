using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionApplier : IEnemyPositionApplier
{
    private readonly List<EnemySpawnPoint> _spawnPoints;

    public EnemyPositionApplier(IReadOnlyList<EnemySpawnPoint> spawnPoints)
    {
        if (spawnPoints == null)
            throw new ArgumentNullException(nameof(spawnPoints));

        _spawnPoints = new(spawnPoints);
    }

    public bool HasPosition => _spawnPoints.Count > 0;

    public void Apply(IEnemy enemy) 
    {
        Transform target = GetTarget();
        enemy.Transform.SetPositionAndRotation(target.position, target.rotation);
    }

    private Transform GetTarget()
    {
        int index = UnityEngine.Random.Range(0, _spawnPoints.Count);
        Transform target = _spawnPoints[index].transform;
        _spawnPoints.RemoveAt(index);

        return target;
    }
}