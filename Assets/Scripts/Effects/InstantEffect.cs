using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "SO_InstantEffect", menuName = "Effects/SO_InstantEffect")]
// public class SO_InstantEffect : ScriptableObject
// {
//     [Header("Stats")]
//     public FYU_Stats_Dictionary stats;

//     [Header("Type")]
//     public InstantEffectType type;
//     public bool fixedValue = false; // regardless of stats like attack and defense
//     public bool percentageValue = false; // effect on percentage(e.g. 50% of HP)
// }

public enum InstantEffectType {
    Attack,
    Heal
};

[Serializable]
public class InstantEffect
{
    [Header("Stats")]
    [SerializeField] int value;

    [Header("Type")]
    public InstantEffectType type;
    public StatType valueType;
    [SerializeField] bool fixedValue = false; // regardless of stats like attack and defense
    [SerializeField] bool percentageValue = false; // effect on percentage(e.g. 50% of HP)

    int CalculatePercentageValue(Stats to) {//(Stats from, Stats to) {
        // if (InstantEffectType.Attack == type) {
        //     return (int)(((float)value / (float)to.total[StatType.HP_MAX]) * 100f);
        // } else {
        //     return (int)((float)value / (float)to.total[StatType.HP_MAX] * 100f);
        // }
        return (int)((float)value / (float)to.total[StatType.HP_MAX] * 100f);
    }

    public int Calculate(Stats to) {
        if (fixedValue) return value;
        if (percentageValue) return CalculatePercentageValue(to);
        if (InstantEffectType.Attack == type) {
            return (int)(((float)(to.total[StatType.Attack] + value)) / ((float)(to.total[StatType.Defense] + 100) / 100f));
        } else {
            return (int)(((float)(to.total[StatType.Attack] + value)) * 1.25f);
        }
    }

    public int Calculate(Stats from, Stats to) {
        if (fixedValue) return value;
        if (percentageValue) return CalculatePercentageValue(to);
        if (InstantEffectType.Attack == type) {
            return (int)(((float)(from.total[StatType.Attack] + value)) / ((float)(to.total[StatType.Defense] + 100) / 100f));
        } else {
            return (int)(((float)(from.total[StatType.Attack] + value)) * 1.25f);
        }
    }
}