using UnityEngine;

public class Player : MonoBehaviour
{
    private IInputReader _inputReader;

    // Надо как-то получить IInputReader

    private void OnEnable()
    {
        _inputReader.InteractionPressed += OnInteractionPressed;
    }

    private void OnDisable()
    {
        _inputReader.InteractionPressed -= OnInteractionPressed;
    }

    private void OnInteractionPressed()
    {
        Debug.Log("Нажата кнопка взаимодействия");
    }
}