using System;

public interface IInputReader
{
    public event Action InteractionPressed;
}

// где-то будет проверяться платформа:
#if UNITY_ANDROID || UNITY_IOS
    // Мобильный код
#else 
    // ПК Windows
#endif