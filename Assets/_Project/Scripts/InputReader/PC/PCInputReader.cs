using System;
using UnityEngine;

public partial class PCInputReader : IInputReader
{
    private readonly IPCInputData _data;

    public PCInputReader(IPCInputData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public event Action InteractionPressed;

    // Надо как-то обновлять, если не монобех
    public void Tick()
    {
        if (Input.GetKeyDown(_data.InteractionKey))
            InteractionPressed?.Invoke();
    }
}