using System;
using VContainer;

public class ProgressSaverButton : ButtonClickHandler, IInjectable
{
    private ISaver _saver;

    [Inject]
    public void Construct(ISaver saver)
    {
        _saver = saver ?? throw new ArgumentNullException(nameof(saver));
    }

    protected override void OnClick() =>
        _saver.Save();
}