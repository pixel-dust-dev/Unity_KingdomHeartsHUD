using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DriveGauge : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image innerEmpty;
    [SerializeField]
    private Image innerFill;
    [SerializeField]
    private Image innerFillCharge;
    [SerializeField]
    private Image innerFillSummon;
    [SerializeField]
    private TMP_Text driveNumber;
    [SerializeField]
    private GameObject maxSprite;

    public void OnDriveChanged(DriveManager driveManager)
    {
        if(driveManager.CurrDriveMode == DriveManager.DriveMode.Charging)
        {
            innerFillCharge.enabled = true;
            innerFillSummon.enabled = false;
        }
        else if (driveManager.CurrDriveMode == DriveManager.DriveMode.Summon)
        {
            innerFillCharge.enabled = false;
            innerFillSummon.enabled = true;
        }
        OnDriveChanged(driveManager.MaxDrive, driveManager.CurrDrive, driveManager.MaxCharge, driveManager.CurrCharge);
    }
    public void OnDriveChanged(float newMaxValue, float newCurrentValue, int maxCharges, int currentCharges)
    {
        innerFill.fillAmount = (float)(newCurrentValue - (currentCharges * newMaxValue)) / newMaxValue;
        driveNumber.text = currentCharges.ToString();

        if (newCurrentValue >= (maxCharges + 1) * newMaxValue)
        {
            maxSprite.SetActive(true);
        }
        else
        {
            maxSprite.SetActive(false);
        }
    }
}