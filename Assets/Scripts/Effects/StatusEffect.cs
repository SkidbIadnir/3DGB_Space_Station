using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// [CreateAssetMenu(fileName = "SO_Status", menuName = "Effects/SO_Status")]
// public class SO_Status : ScriptableObject
// {
//     [Header("General")]
//     public string description;

//     [Header("Stats")]
//     public FYU_Stats_Dictionary stats;

//     [Header("Type")]
//     public StatusType status;
//     public CoreType type;

//     [Header("Settings")]
//     public bool fixedValue = true;
//     public bool percentageValue = false;
//     public bool repeatable = false;

//     [Header("Clock")]
//     public MyTime duration;
//     public MyTime repeatTime;
// }

public enum StatusType {
    Buff,
    Debuff
};

// Implement Whitemane

[Serializable]
public class StatusEffect
{
    [field: Header("General")]
    public string _name;
    public string _description;
    public Sprite _sprite;

    [field: Header("Status")]
    public StatType valueType;
    [SerializeField] int value;

    [Header("Type")]
    public StatusType status;

    [Header("Settings")]
    [SerializeField] bool fixedValue = true;
    [SerializeField] bool percentageValue = false;
    public bool repeatable = false;
    public bool infinite = false;

    // [SerializeField] MyTime duration;
    // [SerializeField] MyTime repeatTime;
    [field: Header("Clock")]
    public Clock duration;
    public Clock repeatTime;

    int CalculatePercentageValue(Stats to) {//(Stats from, Stats to) {
        return (int)(((float)to.total[valueType] / (float)value) * 100f);
    }

    public int Calculate(Stats to) {
        if (fixedValue) {
            if (StatusType.Debuff == status) return -1 * value;
            else return value;
        }
        if (percentageValue) return CalculatePercentageValue(to);

        if (StatType.HP == valueType && StatusType.Debuff == status)
            return -1 * (int)(((float)(to.total[StatType.Attack] + value)) / ((float)(to.total[StatType.Defense] + 100) / 100f));
        else {
            int t = (int)(((float)(to.total[valueType] + value)) * 1.25f);
            if (StatusType.Debuff == status) return -1 * t;
            else return t;
        }
    }

    public int Calculate(Stats from, Stats to) {
        if (fixedValue) {
            if (StatusType.Debuff == status) return -1 * value;
            else return value;
        }
        if (percentageValue) return CalculatePercentageValue(to);

        if (StatType.HP == valueType && StatusType.Debuff == status)
            return -1 * (int)(((float)(from.total[StatType.Attack] + value)) / ((float)(to.total[StatType.Defense] + 100) / 100f));
        else {
            int t = (int)(((float)(to.total[valueType] + value)) * 1.25f);
            if (StatusType.Debuff == status) return -1 * t;
            else return t;
        }
    }
}