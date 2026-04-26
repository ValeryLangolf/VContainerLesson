using UnityEngine;

public class PlayerPrefsSaver : ISaver
{
    public void Load()
    {
        Debug.Log("Загружены данные из Player Prefs");
    }

    public void Save()
    {
        Debug.Log("Сохранены данные в Player Prefs");
    }
}