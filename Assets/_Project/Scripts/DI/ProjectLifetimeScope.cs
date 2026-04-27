using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private Music _musicService;
    [SerializeField] private string _savingPath = "saves/";

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ProjectEntryPoint>(Lifetime.Singleton);
        builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);

        builder.RegisterComponent(_savingPath);
        builder.Register<ISaver, JsonSaver>(Lifetime.Singleton);
        builder.RegisterComponent<IMusic>(_musicService);
    }
}