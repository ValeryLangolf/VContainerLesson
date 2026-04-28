using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPositionApplier : IEnemyPositionApplier
{
    private readonly List<Transform> _spawnPoints;

    public EnemyPositionApplier(IReadOnlyList<EnemySpawnPoint> spawnPoints)
    {
        if (spawnPoints == null)
            throw new ArgumentNullException(nameof(spawnPoints));

        _spawnPoints = spawnPoints
            .Where(point => point != null && point.transform != null)
            .Select(point => point.transform)
            .ToList();
    }

    public int CountAvailable => _spawnPoints.Count;

    public bool TryApply(IEnemy enemy)
    {
        if (enemy == null)
            throw new ArgumentNullException(nameof(enemy));

        if (TryGetTarget(out Transform target))
        {
            enemy.Transform.SetPositionAndRotation(target.position, target.rotation);

            return true;
        }

        return false;
    }

    private bool TryGetTarget(out Transform target)
    {
        target = null;

        if (CountAvailable == 0)
            return false;

        int index = UnityEngine.Random.Range(minInclusive: 0, _spawnPoints.Count);
        target = _spawnPoints[index];
        _spawnPoints.RemoveAt(index);

        return true;
    }
}