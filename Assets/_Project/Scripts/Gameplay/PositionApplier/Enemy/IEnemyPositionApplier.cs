public interface IEnemyPositionApplier
{
    public int CountAvailable { get; }

    public bool TryApply(IEnemy entity);
}