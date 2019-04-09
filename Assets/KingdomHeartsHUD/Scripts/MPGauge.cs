using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPGauge : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image innerEmpty;
    [SerializeField]
    private Image innerFill;

    public void OnMaxManaChanged(ManaManager manaManager)
    {
        this.OnMaxManaChanged(manaManager.MaxMana);
    }
    public void OnMaxManaChanged(int newValue)
    {
        background.rectTransform.sizeDelta = new Vector2(newValue + 55, background.rectTransform.sizeDelta.y);
        innerEmpty.rectTransform.sizeDelta = new Vector2(newValue, innerEmpty.rectTransform.sizeDelta.y);
        innerFill.rectTransform.sizeDelta = new Vector2(newValue, innerFill.rectTransform.sizeDelta.y);
    }

    public void OnCurrManaChanged(ManaManager manaManager)
    {
        this.OnCurrManaChanged(manaManager.ManaPercentage);
    }
    public void OnCurrManaChanged(float newValue)
    {
        innerFill.fillAmount = newValue;
    }
}
