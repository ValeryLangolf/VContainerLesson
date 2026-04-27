using System;
using System.Collections.Generic;

public class EnemyPool : IEnemyPool
{
    private readonly IEnemyFactory _factory;
    private readonly Stack<IEnemy> _stack = new();

    public EnemyPool(IEnemyFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public IEnemy Give()
    {
        IEnemy enemy;

        if (_stack.Count > 0)
            enemy = _stack.Pop();
        else
            enemy = _factory.Create();

        enemy.Deactivated += Return;

        return enemy;
    }

    private void Return(IEnemy enemy)
    {
        if (enemy == null)
            throw new ArgumentNullException(nameof(enemy));

        enemy.Deactivated -= Return;

        _stack.Push(enemy);
    }
}