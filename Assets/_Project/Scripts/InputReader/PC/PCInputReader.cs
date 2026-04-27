using System;
using UnityEngine;
using VContainer.Unity;

public partial class PCInputReader : IInputReader, ITickable
{
    private readonly IPCInputData _data;

    public PCInputReader(IPCInputData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public event Action InteractionPressed;

    public void Tick()
    {
        if (Input.GetKeyDown(_data.InteractionKey))
            InteractionPressed?.Invoke();
    }
}