public class ProgressSaverButton : ButtonClickHandler
{
    private ISaver _saver;

    protected override void OnClick() =>
        _saver.Save();
}