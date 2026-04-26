using System;
using UnityEngine;

public class JsonSaver : ISaver
{
    private readonly string _path;

    public JsonSaver(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new InvalidOperationException($"{nameof(path)} = '{path}' - указан некорректный путь");

        _path = path;
    }

    public void Load()
    {
        Debug.Log($"Загружены данные из Json по пути '{_path}'");
    }

    public void Save()
    {
        Debug.Log($"Сохранены данные в Json по пути '{_path}'");
    }
}