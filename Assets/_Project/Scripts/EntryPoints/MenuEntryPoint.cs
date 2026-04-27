using UnityEngine;
using VContainer.Unity;

public class MenuEntryPoint : IStartable
{
    private readonly AudioClip _clip;
    private readonly IMusic _music;

    public MenuEntryPoint(AudioClip clip, IMusic music)
    {
        _clip = clip;
        _music = music;
    }

    public void Start() =>
        _music.Play(_clip);
}
