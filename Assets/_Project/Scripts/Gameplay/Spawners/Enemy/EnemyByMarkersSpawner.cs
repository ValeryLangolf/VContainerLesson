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

    public IReadOnlyList<IEnemy> Spawn(int count)
    {
        List<IEnemy> enemies = new();

        for(int i = 0; i < count; i++)
        {
            if (_positionApplier.HasPosition == false)
                break;

            enemies.Add(Spawn());
        }

        return enemies;
    }

    private IEnemy Spawn()
    {
        IEnemy enemy = _pool.Give();
        _positionApplier.Apply(enemy);

        return enemy;
    }
}