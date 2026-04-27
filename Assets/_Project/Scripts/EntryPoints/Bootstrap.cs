using System;
using VContainer.Unity;

public class Bootstrap : IStartable
{
    private readonly ISceneLoader _sceneLoader;

    public Bootstrap(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
    }

    public void Start() =>
        _sceneLoader.Load(Constants.MenuSceneName);
}