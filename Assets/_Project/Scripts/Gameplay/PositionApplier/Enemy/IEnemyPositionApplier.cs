public interface IEnemyPositionApplier
{
    public bool HasPosition { get; }

    public void Apply(IEnemy entity);
}