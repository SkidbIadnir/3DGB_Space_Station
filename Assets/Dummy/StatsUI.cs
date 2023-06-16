using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public Stats stats = new Stats();

    [Header("Current")]
    public TMP_Text hp;
    public TMP_Text hpMax;
    public TMP_Text attack;
    public TMP_Text defense;

    [Header("Set")]
    public TMP_InputField sHp;
    public TMP_InputField sHpMax;
    public TMP_InputField sAttack;
    public TMP_InputField sDefense;

    [Header("Add")]
    public TMP_InputField aHp;
    public TMP_InputField aHpMax;
    public TMP_InputField aAttack;
    public TMP_InputField aDefense;

    [Header("Remove")]
    public TMP_InputField rHp;
    public TMP_InputField rHpMax;
    public TMP_InputField rAttack;
    public TMP_InputField rDefense;

    void Start() {
        stats.Start();
        sHp.text = "0";
        sHpMax.text = "0";
        sAttack.text = "0";
        sDefense.text = "0";
        aHp.text = "0";
        aHpMax.text = "0";
        aAttack.text = "0";
        aDefense.text = "0";
        rHp.text = "0";
        rHpMax.text = "0";
        rAttack.text = "0";
        rDefense.text = "0";
    }

    void Update() {
        hp.text = stats.total[StatType.HP].ToString();
        hpMax.text = stats.total[StatType.HP_MAX].ToString();
        attack.text = stats.total[StatType.Attack].ToString();
        defense.text = stats.total[StatType.Defense].ToString();
    }

    public void Set() {
        stats.SetEffects(new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, int.Parse(sHp.text)}, { StatType.HP_MAX, int.Parse(sHpMax.text)},
            { StatType.Attack, int.Parse(sAttack.text)}, { StatType.Defense, int.Parse(sDefense.text)}}));
    }
    public void Add() {
        stats.AddEffects(new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, int.Parse(aHp.text)}, { StatType.HP_MAX, int.Parse(aHpMax.text)},
            { StatType.Attack, int.Parse(aAttack.text)}, { StatType.Defense, int.Parse(aDefense.text)}}));
    }
    public void Remove() {
        stats.RemoveEffects(new FYU_Stats_Dictionary(new Dictionary<StatType, int>() { 
            { StatType.HP, int.Parse(rHp.text)}, { StatType.HP_MAX, int.Parse(rHpMax.text)},
            { StatType.Attack, int.Parse(rAttack.text)}, { StatType.Defense, int.Parse(rDefense.text)}}));
    }
}
