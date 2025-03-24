using NaughtyAttributes;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [Foldout("OTHERS")]
    [SerializeField] private bounce3D _bounce;
    [Foldout("OTHERS")]
    [SerializeField] private Transform _transformDialogueSpawnPoint;
    [Foldout("OTHERS")]
    [SerializeField] private textController _textController;

    private Dialogue _currentDialogue;
    
    
    
    public void CallDialogue(Dialogue dialogue)
    {
        _textController.CallDialogue(dialogue);
    }
    public bounce3D GetBounce()
    {
        return _bounce;
    }
}
