using System;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void Load(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
            throw new ArgumentException("Имя сцены не может быть null, пустым или состоять только из пробелов.", nameof(sceneName));

        if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
            throw new ArgumentException($"Сцена '{sceneName}' не найдена в Build Settings. Добавьте её в список сцен сборки.");

        SceneManager.LoadScene(sceneName);
    }
}