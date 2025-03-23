using UnityEngine;
using UnityEditor;

public class ShortCut
{
    // % représente Ctrl (ou Cmd sur Mac) et & représente Alt.
    // Ici, le raccourci est Ctrl+Alt+T (Cmd+Alt+T sur Mac).
    [MenuItem("GameObject/Toggle Active %t")]
    private static void ToggleActive()
    {
        if (Selection.activeGameObject != null)
        {
            bool currentState = Selection.activeGameObject.activeSelf;
            Selection.activeGameObject.SetActive(!currentState);
        }
    }
}