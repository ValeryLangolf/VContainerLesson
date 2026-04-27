using System;

public interface IEnemy : IEntity
{
    public event Action<IEnemy> Deactivated;
}