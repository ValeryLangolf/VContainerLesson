public class Weapon
{
    private readonly int _bulletCount;

    public Weapon()
    {
        _bulletCount = UnityEngine.Random.Range(0, 30);
    }

    public int BulletCount => _bulletCount;
}