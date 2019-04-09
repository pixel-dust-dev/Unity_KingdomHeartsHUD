using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class StatsManager : MonoBehaviour
{
    public HealthManager healthManager;
    public ManaManager manaManager;
    public DriveManager driveManager;

    private void OnValidate()
    {
        healthManager.Validate();
        manaManager.Validate();
        driveManager.Validate();
    }
    private void Update()
    {
        healthManager.Update();
        manaManager.Update();
        driveManager.Update();
    }
}

[System.Serializable]
public struct HealthManager
{
    public bool DEBUG_Take_Damage;
    public bool DEBUG_Heal_Damage;
    public int damage;

    [Header("Health Points")]
    [SerializeField]
    private int currHealthPoints;
    [SerializeField]
    private int maxHealthPoints;
    [SerializeField]
    private HealthUpdateEvent OnMaxHealthUpdate;
    [SerializeField]
    private HealthUpdateEvent OnCurrHealthUpdate;
    [SerializeField]
    private ReceiveDamageEvent OnReceiveDamage;

    public int MaxHealth { get { return maxHealthPoints; } }
    public int CurrHealth { get { return currHealthPoints; } }
    public float HealthPercentage { get { return (float)CurrHealth / (float)MaxHealth; } }

    [System.Serializable]
    public class HealthUpdateEvent : UnityEvent<HealthManager> { }
    [System.Serializable]
    public class ReceiveDamageEvent : UnityEvent<int> { }

    public void Update()
    {
        Validate();

        if(Application.isPlaying)
        {
            if (DEBUG_Take_Damage)
            {
                DEBUG_Take_Damage = false;
                TakeDamage(damage);
            }
            if (DEBUG_Heal_Damage)
            {
                DEBUG_Heal_Damage = false;
                Heal(damage);
            }
        }

    }

    public void TakeDamage(int value)
    {
        OnReceiveDamage.Invoke(value);
        currHealthPoints -= value;
        Validate();
    }
    public void DamageAll()
    {
        OnReceiveDamage.Invoke(currHealthPoints);
        currHealthPoints = 0;
        Validate();
    }
    public void Heal(int value)
    {
        currHealthPoints += value;
        Validate();
    }
    public void HealAll()
    {
        currHealthPoints = maxHealthPoints;
        Validate();
    }
    public void SetMaxHealth(int value)
    {
        maxHealthPoints = value;
        Validate();
    }
    public void SetCurrHealth(int value)
    {
        currHealthPoints = value;
        Validate();
    }
    public void Validate()
    {
        if (maxHealthPoints < 5) { maxHealthPoints = 5; }
        if (currHealthPoints > maxHealthPoints) { currHealthPoints = maxHealthPoints; }
        if (currHealthPoints < 0) { currHealthPoints = 0; }

        OnMaxHealthUpdate.Invoke(this);
        OnCurrHealthUpdate.Invoke(this);
    }
}

[System.Serializable]
public struct ManaManager
{
    [Header("Mana Points")]
    [SerializeField]
    private int currManaPoints;
    [SerializeField]
    private int maxManaPoints;
    [SerializeField]
    private ManaUpdateEvent OnMaxManaUpdate;
    [SerializeField]
    private ManaUpdateEvent OnCurrManaUpdate;

    public int MaxMana { get { return maxManaPoints; } }
    public int CurrMana { get { return currManaPoints; } }
    public float ManaPercentage { get { return (float)CurrMana / (float)MaxMana; } }

    [System.Serializable]
    public class ManaUpdateEvent : UnityEvent<ManaManager> { }

    public void Update()
    {
        Validate();
    }

    public void Validate()
    {
        OnMaxManaUpdate.Invoke(this);
        OnCurrManaUpdate.Invoke(this);

        if (currManaPoints > maxManaPoints) { currManaPoints = maxManaPoints; }
        if (currManaPoints < 0) { currManaPoints = 0; }
    }
}

[System.Serializable]
public struct DriveManager
{
    [Header("Drive/Form")]
    [SerializeField]
    private int maxCharge;
    [SerializeField]
    private int currCharge;
    [SerializeField]
    private float maxDrive;
    [SerializeField]
    private float currDrive;
    public enum DriveMode { Charging, Form, Summon }
    [SerializeField]
    private DriveMode driveMode;
    [SerializeField]
    private DriveUpdateEvent OnDriveChargeUpdate;

    public float MaxDrive
    {
        get { return maxDrive; }
        set { maxDrive = value; Validate(); }
    }
    public float CurrDrive
    {
        get { return currDrive; }
        set { currDrive = value; Validate(); }
    }
    public int MaxCharge
    {
        get { return maxCharge; }
        set { maxCharge = value; Validate(); }
    }
    public int CurrCharge
    {
        get { return currCharge; }
        set { currCharge = value; Validate(); }
    }
    public DriveMode CurrDriveMode
    {
        get { return driveMode; }
        set { driveMode = value; Validate(); }
    }

    [System.Serializable]
    public class DriveUpdateEvent : UnityEvent<DriveManager> { }

    public void Validate()
    {
        if (currDrive < 0) { currDrive = 0; }
        if (maxDrive < 1) { maxDrive = 1; }
        if (maxCharge < 0) { maxCharge = 0; }

        if (CurrDrive > (MaxCharge + 1) * MaxDrive)
        {
            currDrive = (MaxCharge + 1) * MaxDrive;
        }

        currCharge = (int)Mathf.Clamp(currDrive / maxDrive, 0, maxCharge);

        OnDriveChargeUpdate.Invoke(this);
    }

    public void Update()
    {
        Validate();
        if(CurrDriveMode == DriveMode.Charging)
        {

        }
        if (CurrDriveMode == DriveMode.Summon)
        {
            CurrDrive -= Time.deltaTime*10;
            if(currDrive <= 0)
            {
                CurrDriveMode = DriveMode.Charging;
            }
        }
        if (CurrDriveMode == DriveMode.Form)
        {
            CurrDrive -= Time.deltaTime * 10;
            if (currDrive <= 0)
            {
                CurrDriveMode = DriveMode.Charging;
            }
        }
    }
}