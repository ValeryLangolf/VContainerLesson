using System;

public class EnemyFactory : IEnemyFactory
{
    private readonly Enemy _prefab;

    public EnemyFactory(Enemy prefab)
    {
        _prefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));
    }

    public IEnemy Create() =>
        UnityEngine.Object.Instantiate(_prefab);
}