using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstantEffectUI : MonoBehaviour
{
    [Header("Player")]
    public Player player;
    public TMP_Text php;
    public TMP_Text php_max;
    public TMP_Text pattack;
    public TMP_Text pdefence;

    [Header("Dummy")]
    public Dummy dummy;
    public TMP_Text dhp;
    public TMP_Text dhp_max;
    public TMP_Text dattack;
    public TMP_Text ddefence;

    [Header("Abilities")]
    public List<AbilityManager> abilities;
    public TMP_Dropdown target;

    void Start() {
        foreach(var ability in abilities) ability.Start();
    }

    void Update() {
        foreach(var ability in abilities) ability.Update();
        php.text = player.stats.total[StatType.HP].ToString();
        php_max.text = player.stats.total[StatType.HP_MAX].ToString();
        pattack.text = player.stats.total[StatType.Attack].ToString();
        pdefence.text = player.stats.total[StatType.Defense].ToString();
        dhp.text = dummy.stats.total[StatType.HP].ToString();
        dhp_max.text = dummy.stats.total[StatType.HP_MAX].ToString();
        dattack.text = dummy.stats.total[StatType.Attack].ToString();
        ddefence.text = dummy.stats.total[StatType.Defense].ToString(); 
    }

    public void Use(int abilityIdx) {
        if (target.options[target.value].text == "Player")
            abilities[abilityIdx].Use(player, dummy, false);
        else abilities[abilityIdx].Use(player, dummy);
    }
}
