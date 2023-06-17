using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType {
    HP,
    HP_MAX,
    Attack,
    Defense,
    MovementSpeed,
    JumpForce
};

[Serializable]
public class Stats
{
    [SerializeField] FYU_Stats_Dictionary source;
    public FYU_Stats_Dictionary total;

    // Default constructor
    public Stats() {
        source = new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, 1}, { StatType.HP_MAX, 100}, { StatType.Attack, 0}, { StatType.Defense, 0}, { StatType.MovementSpeed, 2}, { StatType.JumpForce, 2}});
        source.Start();
        total = new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, 0}, { StatType.HP_MAX, 0}, { StatType.Attack, 0}, { StatType.Defense, 0}, { StatType.MovementSpeed, 2}, { StatType.JumpForce, 2}});
        total.Start();
        CheckValues(total);
        UpToStats();
    }
    public Stats(FYU_Stats_Dictionary stats) {
        source = new FYU_Stats_Dictionary(stats);
        source.Start();
        total = new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, 0}, { StatType.HP_MAX, 0}, { StatType.Attack, 0}, { StatType.Defense, 0}, { StatType.MovementSpeed, 2}, { StatType.JumpForce, 2}
        });
        total.Start();
        CheckValues(total);
        UpToStats();
    }

    public void Start() {
        // Reset if not completed
        if (4 != source.Count())
            source = new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
                { StatType.HP, 1}, { StatType.HP_MAX, 100}, { StatType.Attack, 0}, { StatType.Defense, 0}, { StatType.MovementSpeed, 2}, { StatType.JumpForce, 2}});
        else source.Start();

        // Total should not be manipulated before Start
        total = new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, 0}, { StatType.HP_MAX, 0}, { StatType.Attack, 0}, { StatType.Defense, 0}, { StatType.MovementSpeed, 2}, { StatType.JumpForce, 2}});
        total.Start();
        CheckValues(total);
        UpToStats();
    }

    // Control values
    void CheckValues(FYU_Stats_Dictionary stats) {
        if (stats[StatType.HP] < 0) stats[StatType.HP] = 0;
        if (stats[StatType.HP_MAX] < 0) stats[StatType.HP_MAX] = 0;
        if (stats[StatType.Attack] < 0) stats[StatType.Attack] = 0;
        if (stats[StatType.Defense] < 0) stats[StatType.Defense] = 0;

        if (stats[StatType.HP] > stats[StatType.HP_MAX]) stats[StatType.HP] = stats[StatType.HP_MAX];
    }

    // Do a version with list and one without.
    void UpToStats() { // param List of effects
        foreach (StatsPair p in source)
            total[p.Key] = p.Value;
        
        // Add for each effect in param
        // CheckValues(total); only if list of effects
    }

    // Manipulate stats
    public void SetEffects(FYU_Stats_Dictionary stats) {
        foreach (StatsPair p in stats)
            total[p.Key] = p.Value;
        CheckValues(total);
    }
    public void AddEffect(StatType type, int value) {
        total[type] += value;
        CheckValues(total);
    }
    public void AddEffects(FYU_Stats_Dictionary stats) {
        foreach (StatsPair p in stats)
            total[p.Key] += p.Value;
        CheckValues(total);
    }
    public void RemoveEffect(StatType type, int value) {
        total[type] -= value;
        CheckValues(total);
    }
    public void RemoveEffects(FYU_Stats_Dictionary stats) {
        foreach (StatsPair p in stats)
            total[p.Key] -= p.Value;
        CheckValues(total);
    }
}
