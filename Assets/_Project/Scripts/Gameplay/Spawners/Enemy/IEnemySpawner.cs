using System.Collections.Generic;

public interface IEnemySpawner
{
    public IReadOnlyList<IEnemy> Spawn(int count);
}