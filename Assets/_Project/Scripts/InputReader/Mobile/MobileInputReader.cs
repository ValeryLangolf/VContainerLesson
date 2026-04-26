using System;
using UnityEngine;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    public event Action InteractionPressed;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
                InteractionPressed?.Invoke();
        }
    }
}