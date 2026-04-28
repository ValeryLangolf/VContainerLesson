using System;
using UnityEngine;
using VContainer.Unity;

public class MenuEntryPoint : IStartable
{
    private readonly AudioClip _clip;
    private readonly IMusic _music;

    public MenuEntryPoint(AudioClip clip, IMusic music)
    {
        _clip = clip != null ? clip : throw new ArgumentNullException(nameof(clip));
        _music = music ?? throw new ArgumentNullException(nameof(music));
    }

    public void Start() =>
        _music.Play(_clip);
}