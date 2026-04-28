using System.Collections.Generic;

public interface IEnemySpawner
{
    public bool TrySpawn(int count, out List<IEnemy> enemies);
}