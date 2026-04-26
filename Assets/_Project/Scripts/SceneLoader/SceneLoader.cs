using UnityEngine;

public class SceneLoader : ISceneLoader
{
    public void Load(string sceneName)
    {
        Debug.Log($"Загружена сцена '{sceneName}'");
    }
}