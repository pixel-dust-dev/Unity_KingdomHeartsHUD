using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public StatsManager statsManager;

    public void HP_TakeDamage(int damage)
    {
        statsManager.healthManager.TakeDamage(damage);
    }

    public void HP_DamageAll()
    {
        statsManager.healthManager.DamageAll();
    }

    public void HP_Heal(int healAmount)
    {
        statsManager.healthManager.Heal(healAmount);
    }

    public void HP_HealAll()
    {
        statsManager.healthManager.HealAll();
    }

    public void HP_ChangeMax(float value)
    {
        statsManager.healthManager.SetMaxHealth((int)value);
    }

    public void HP_ChangeCurr(float value)
    {
        statsManager.healthManager.SetCurrHealth((int)value);
    }
}
