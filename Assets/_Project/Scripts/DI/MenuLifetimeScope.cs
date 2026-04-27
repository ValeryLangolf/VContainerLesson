using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    [SerializeField] private AudioClip _menuClip;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<MenuEntryPoint>();
        builder.RegisterComponent(_menuClip);
    }
}