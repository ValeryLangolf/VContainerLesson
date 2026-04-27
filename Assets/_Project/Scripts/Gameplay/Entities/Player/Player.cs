using UnityEngine;
using VContainer;

public class Player : MonoBehaviour, IPlayer
{
    private IInputReader _inputReader;

    public Transform Transform => transform;

    [Inject]
    public void Construct(IInputReader inputReader) =>
        _inputReader = inputReader;

    private void OnEnable() =>
        _inputReader.InteractionPressed += OnInteractionPressed;

    private void OnDisable() =>
        _inputReader.InteractionPressed -= OnInteractionPressed;

    private void OnInteractionPressed() =>
        Debug.Log("Нажата кнопка взаимодействия");
}