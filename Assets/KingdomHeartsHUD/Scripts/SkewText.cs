using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class SkewText : MonoBehaviour
{
    Vector4 skewX;
    Vector4 skewY;

    private TMP_Text m_TextComponent;
    private TMP_Text CachedTextComponent
    {
        get
        {
            if(m_TextComponent == null)
            {
                m_TextComponent = gameObject.GetComponent<TMP_Text>();
            }
            return m_TextComponent;
        }
    }

    public float xSkew = 2;

    Vector3[] vertices;
    Matrix4x4 matrix;

    private void SetSkew()
    {
        CachedTextComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        int characterCount = textInfo.characterCount;

        if (characterCount == 0) return;

        float boundsMinX = CachedTextComponent.bounds.min.x;
        float boundsMaxX = CachedTextComponent.bounds.max.x;

        for (int i = 0; i < characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
            {
                continue;
            }

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            vertices = textInfo.meshInfo[materialIndex].vertices;

            vertices[vertexIndex + 0].x += xSkew;
            vertices[vertexIndex + 3].x += xSkew;

            // Upload the mesh with the revised information
            m_TextComponent.UpdateVertexData();
        }
    }

    private void OnEnable()
    {
        SetSkew();
    }

    private void OnValidate()
    {
        SetSkew();
    }

    private void Update()
    {
        //if (CachedTextComponent.havePropertiesChanged)
        //{
            SetSkew();
        //}
    }
}
