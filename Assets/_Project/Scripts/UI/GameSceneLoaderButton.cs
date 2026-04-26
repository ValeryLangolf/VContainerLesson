public class GameSceneLoaderButton : ButtonClickHandler
{
    private ISceneLoader _sceneLoader;

    protected override void OnClick()
    {
        _sceneLoader.Load(Constants.GameSceneName);
    }
}