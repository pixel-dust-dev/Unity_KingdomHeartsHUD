using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthGauge : MonoBehaviour
{
    [SerializeField]
    private Image innerCircularSection;
    [SerializeField]
    private Image innerLinearSection;

    [SerializeField]
    private float damageTime = 2;
    [SerializeField]
    private Gradient damageGradient;
    [SerializeField]
    private Image damageCircularSection;
    [SerializeField]
    private Image damageLinearSection;
    private float timer = 0;

    [SerializeField]
    private Image outerCircularSection;
    [SerializeField]
    private Image outerLinearSection;

    [SerializeField]
    private Image linearCap;
    [SerializeField]
    private RectTransform rotator;

    private float circlePercentage;

    [Range(0,1)]
    public float fillPercentage;

    public int barLength = 300;

    [SerializeField]
    private float ringDiameter = 200;
    float TotalRinglength
    {
        get
        {
            return TotalRingCircumference * 0.75f;
        }
    }
    float TotalRingCircumference
    {
        get
        {
            return Mathf.PI * ringDiameter;
        }
    }

    private void Update()
    {
        if (innerCircularSection == null || innerLinearSection == null) { return; }
        UpdateCircularSection();
        UpdateLinearSection();

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
            damageCircularSection.color = damageGradient.Evaluate(1 - (timer / damageTime));
            damageLinearSection.color = damageGradient.Evaluate(1 - (timer / damageTime));
        }
        else
        {
            damageCircularSection.color = damageGradient.Evaluate(1);
            damageLinearSection.color = damageGradient.Evaluate(1);
        }
    }

    private void UpdateCircularSection()
    {
        outerCircularSection.fillAmount = Mathf.Lerp(0, 0.75f, (barLength / TotalRinglength));

        circlePercentage = Mathf.Clamp01(outerCircularSection.fillAmount * TotalRingCircumference / barLength);
        
        innerCircularSection.fillAmount = Mathf.Lerp(0f, outerCircularSection.fillAmount, (fillPercentage/circlePercentage));
        damageCircularSection.fillAmount = Mathf.Clamp(damageCircularSection.fillAmount, 0, outerCircularSection.fillAmount);

        //Set End Cap rotation
        float requiredRotation = Mathf.Lerp(0,-360, outerCircularSection.fillAmount);
        rotator.eulerAngles = new Vector3(0,0, requiredRotation);
        if(barLength - TotalRinglength > 0)
        {
            rotator.gameObject.SetActive(false);
        }
        else
        {
            rotator.gameObject.SetActive(true);
        }
    }
    private void UpdateLinearSection()
    {
        outerLinearSection.rectTransform.sizeDelta = new Vector2(barLength - TotalRinglength, innerLinearSection.rectTransform.sizeDelta.y);
        innerLinearSection.rectTransform.sizeDelta = new Vector2(barLength - TotalRinglength, outerLinearSection.rectTransform.sizeDelta.y);
        damageLinearSection.rectTransform.sizeDelta = innerLinearSection.rectTransform.sizeDelta;

        innerLinearSection.fillAmount = Mathf.Lerp(0, 1, ((fillPercentage - circlePercentage) / (1 - circlePercentage)));

        linearCap.enabled = outerLinearSection.rectTransform.sizeDelta.x > 0 ? true : false;
    }

    public void OnMaxHealthChanged(HealthManager healthManager)
    {
        this.OnMaxHealthChanged(healthManager.MaxHealth);
    }
    public void OnMaxHealthChanged(int newValue)
    {
        this.barLength = newValue;
    }

    public void OnCurrHealthChanged(HealthManager healthManager)
    {
        this.OnCurrHealthChanged(healthManager.HealthPercentage);
    }
    public void OnCurrHealthChanged(float newValue)
    {
        this.fillPercentage = newValue;
    }

    public void OnDamageReceieved(int damage)
    {
        this.damageLinearSection.fillAmount = this.innerLinearSection.fillAmount;
        this.damageCircularSection.fillAmount = this.innerCircularSection.fillAmount;

        this.timer = damageTime;
    }
}