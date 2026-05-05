using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyFactory : MonoBehaviour, IEnemyFactory, IInjectable
{
    [SerializeField] private Enemy _prefab;

    private IObjectResolver _resolver;

    [Inject]
    public void Construct(IObjectResolver resolver) =>
        _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));

    public IEnemy Create() =>
        _resolver.Instantiate(_prefab);
}