using UnityEngine;
using TMPro;

public class LetterVisibilityByLetterXPosition : MonoBehaviour
{
    [SerializeField] private Transform parapluie;

    private TextMeshProUGUI _textMesh;
    private Matrix4x4 _localToWorldMatrix;

    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _textMesh.ForceMeshUpdate();
        _localToWorldMatrix = transform.localToWorldMatrix;

        TMP_TextInfo textInfo = _textMesh.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible || charInfo.character == ' ') continue;

            Vector3 charBottomLeft = charInfo.bottomLeft;

            Vector3 charPosition = _localToWorldMatrix.MultiplyPoint3x4(charBottomLeft);
            bool shouldBeVisible = charPosition.x < parapluie.position.x;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

            byte alpha = shouldBeVisible ? (byte)255 : (byte)0;

            for (int j = 0; j < 4; j++)
            {
                vertexColors[vertexIndex + j].a = alpha;
            }
        }

        _textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}