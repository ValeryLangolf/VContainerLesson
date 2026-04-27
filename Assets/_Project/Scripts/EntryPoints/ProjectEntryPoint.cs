using System;
using VContainer.Unity;

public class ProjectEntryPoint : IStartable
{
    private readonly ISaver _saver;

    public ProjectEntryPoint(ISaver saver)
    {
        _saver = saver ?? throw new ArgumentNullException(nameof(saver));
    }

    public void Start() =>
        _saver.Load();
}