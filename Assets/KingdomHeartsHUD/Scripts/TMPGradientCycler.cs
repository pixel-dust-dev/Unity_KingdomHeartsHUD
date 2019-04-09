using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class TMPGradientCycler : MonoBehaviour
{
    [SerializeField]
    private Color[] colors;

    private TMP_Text m_TextComponent;
    private TMP_Text CachedTextComponent
    {
        get
        {
            if (m_TextComponent == null)
            {
                m_TextComponent = gameObject.GetComponent<TMP_Text>();
            }
            return m_TextComponent;
        }
    }

    private Color CurrentColor
    {
        get
        {
            return colors[currentIndex];
        }
    }
    private Color NextColor
    {
        get
        {
            return colors[NextIndex];
        }
    }

    [SerializeField]
    private float changeTime = 0.8f;
    private float timer = 0;

    private int currentIndex = 0;
    private int nextIndex;
    private int NextIndex
    {
        get
        {
            nextIndex = currentIndex + 1;
            if(nextIndex >= colors.Length)
            {
                nextIndex = 0;
            }
            return nextIndex;
        }
    }
    private void Update()
    {
        if(colors.Length > 1)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    currentIndex++;
                    if(currentIndex >= colors.Length)
                    {
                        currentIndex = 0;
                    }
                    timer = changeTime;
                }
            }
            CachedTextComponent.color = Color.Lerp(CurrentColor, NextColor, 1-(timer/changeTime));
        }
    }

    private void Start()
    {
        timer = changeTime;
    }
}