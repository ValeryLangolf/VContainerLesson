using System;
using UnityEngine;
using VContainer;

public class Enemy : MonoBehaviour, IEnemy
{
    private Weapon _weapon;
    private int _bulletCount;

    public event Action<IEnemy> Deactivated;

    public Transform Transform => transform;

    [Inject]
    public void Construct(Weapon weapon)
    {
        _weapon = weapon;
        _bulletCount = _weapon.BulletCount;
        Debug.Log(_bulletCount);
    }

    public void Deactivate() =>
        Deactivated?.Invoke(this);
}