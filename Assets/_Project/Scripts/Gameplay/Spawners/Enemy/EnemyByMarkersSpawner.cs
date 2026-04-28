using System;
using System.Collections.Generic;

public class EnemyByMarkersSpawner : IEnemySpawner
{
    private readonly IEnemyPool _pool;
    private readonly IEnemyPositionApplier _positionApplier;

    public EnemyByMarkersSpawner(IEnemyPool pool, IEnemyPositionApplier positionApplier)
    {
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        _positionApplier = positionApplier ?? throw new ArgumentNullException(nameof(positionApplier));
    }

    public bool TrySpawn(int count, out List<IEnemy> enemies)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Значение должно быть положительным");

        enemies = new();

        if (_positionApplier.CountAvailable < count)
            return false;

        for (int i = 0; i < count; i++)
            enemies.Add(Spawn());

        return true;
    }

    private IEnemy Spawn()
    {
        IEnemy enemy = _pool.Give();
        _positionApplier.TryApply(enemy);

        return enemy;
    }
}