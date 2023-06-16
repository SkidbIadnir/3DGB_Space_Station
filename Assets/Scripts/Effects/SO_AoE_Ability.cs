using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum TargetType {
    Player,
    Enemy
};

[Serializable]
public struct AoE_InstantEffect {
    public TargetType target;
    public InstantEffect effect;
}

[Serializable]
public struct AoE_StatusEffect {
    public TargetType target;
    public StatusEffect effect;
}

[CreateAssetMenu(fileName = "SO_AoE_Ability", menuName = "Abilities/SO_AoE_Ability")]
public class SO_AoE_Ability : ScriptableObject
{
    [Header("General")]
    public string _name;
    public string description;
    public Sprite sprite;

    [Header("Time")]
    public MyTime countdown;
    public bool instant;
    public MyTime duration;
    public bool repeatable;
    public MyTime repeatTime;

    [Header("Spe")]
    public bool onContact;

    [Header("Effets")]
    public List<AoE_InstantEffect> instantEffects;
    public List<AoE_StatusEffect> statusEffects;
}
