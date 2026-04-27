using UnityEngine;

[CreateAssetMenu(
    fileName = nameof(PCInputConfig), 
    menuName = Constants.AssetMenuPath + nameof(PCInputConfig))]
public class PCInputConfig : ScriptableObject, IPCInputData
{
    [SerializeField] private KeyCode _interactionButton;

    public KeyCode InteractionKey => _interactionButton;
}