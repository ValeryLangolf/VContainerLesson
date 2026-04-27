using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public event Action<IEnemy> Deactivated;

    public Transform Transform => transform;

    public void Deactivate() =>
        Deactivated?.Invoke(this);
}