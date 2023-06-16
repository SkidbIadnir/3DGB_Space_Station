using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_Ability", menuName = "Abilities/SO_Ability")]
public class SO_Ability : ScriptableObject
{
    [Header("General")]
    public string _name;
    public string description;
    public Sprite sprite;

    [Header("Time")]
    public MyTime countdown;
    public bool repeatable;
    public MyTime repeatTime;

    [Header("Effets")]
    public List<InstantEffect> instantEffects;
    public List<StatusEffect> statusEffects;
}
