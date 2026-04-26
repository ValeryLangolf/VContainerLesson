using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private IMusic _music;

    // надо как-то внедрить _music

    private void Start()
    {
        //_music.Play(_clip);
    }
}