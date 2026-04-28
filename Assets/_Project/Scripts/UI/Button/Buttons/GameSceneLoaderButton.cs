using System;
using VContainer;

public class GameSceneLoaderButton : ButtonClickHandler, IInjectable
{
    private ISceneLoader _sceneLoader;

    [Inject]
    public void Construct(ISceneLoader sceneLoader) =>
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));

    protected override void OnClick() =>
        _sceneLoader.Load(Constants.GameSceneName);
}