using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHBarManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    #region Elements
    [Header("Elements")]
    [SerializeField]
    private HealthGauge healthGauge;
    [SerializeField]
    private Portrait portrait;
    [SerializeField]
    private DriveGauge driveGauge;
    [SerializeField]
    private MPGauge manaGauge;
    #endregion

    public AudioClip[] damageSounds;
    public AudioClip healthUp;

    #region HEALTH
    [SerializeField]
    private int maxHealth = 600;
    [SerializeField]
    private int currentHealth = 600;
    public float HealthPercentage
    {
        get
        {
            return currentHealth > 0 ? (float)currentHealth / (float)maxHealth : 0;
        }
    }

    public void ChangeMaxHealth(int value)
    {
        this.maxHealth = value;
        this.healthGauge.OnMaxHealthChanged(this.maxHealth);
        this.ChangeCurrHealth(this.currentHealth);
    }

    public void ChangeCurrHealth(int value)
    {
        this.currentHealth = Mathf.Clamp(value, 0, this.maxHealth);
        this.healthGauge.OnCurrHealthChanged(this.HealthPercentage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) { currentHealth = 0; }

        this.healthGauge.OnCurrHealthChanged(HealthPercentage);
        this.portrait.DisplayDamagedFace();
        this.animator.SetTrigger("Damage");

        if(damage <= 50)
        {
            audioSource.PlayOneShot(damageSounds[0]);
        }
        else if(damage <= 100)
        {
            audioSource.PlayOneShot(damageSounds[1]);
        }
        else
        {
            audioSource.PlayOneShot(damageSounds[2]);
        }
        int rand = Random.Range(0, damageSounds.Length);
    }

    public AudioSource audioSource;

    public void HealAll()
    {
        currentHealth = maxHealth;

        this.healthGauge.OnCurrHealthChanged(HealthPercentage);

        audioSource.PlayOneShot(healthUp);
    }
    #endregion

    #region MP
    [SerializeField]
    private int maxMP = 50;
    [SerializeField]
    private int currentMP = 50;
    public float MPPercentage
    {
        get
        {
            return currentMP > 0 ? (float)currentMP / (float)maxMP : 0;
        }
    }
    #endregion

    #region DRIVE
    [SerializeField]
    private int maxDrive = 50;
    [SerializeField]
    private int currentDrive = 50;
    public float DrivePercentage
    {
        get
        {
            return currentMP > 0 ? (float)currentMP / (float)maxMP : 0;
        }
    }
    private enum DriveMode { Normal, Summon, Form }
    private DriveMode driveMode = DriveMode.Normal;
    #endregion

}