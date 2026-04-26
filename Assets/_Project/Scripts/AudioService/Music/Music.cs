using System;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public void Play(AudioClip clip)
    {
        if (clip == null)
            throw new ArgumentNullException();

        _source.clip = clip;
        _source.Play();
    }
}