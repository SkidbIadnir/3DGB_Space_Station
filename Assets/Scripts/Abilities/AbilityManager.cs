using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AbilityManager
{
    [SerializeField] SO_Ability _so;
    
    [Header("Countdown")]
    [SerializeField] Clock _countdown;

    [Header("Logo")]
    public Image _logo;

    public void Start() {
        _countdown = new Clock(_so.countdown);
    }

    public void Update() {
        _countdown.Update();
        if (_countdown.started) UpdateAbilityDisplay();
        if (_countdown.IsCompleted()) {
            _countdown.Stop();
            _countdown.Reset();
        }
    }

    void UpdateAbilityDisplay() {
        float progress = _countdown.GetPercentage();
        _logo.fillAmount = progress;
    }

    public void Use(Player player, Dummy target, bool forEnemy = true) { // Dummy == Ennemy
        if (_countdown.started) return;

        foreach (InstantEffect effect in _so.instantEffects) {
            int value = effect.Calculate(player.stats, target.stats);
            switch (effect.type) {
                case InstantEffectType.Attack:
                    if (forEnemy) target.stats.AddEffect(StatType.HP, -value);
                    else player.stats.AddEffect(StatType.HP, -value);
                    break;
                case InstantEffectType.Heal:
                    if (forEnemy) target.stats.AddEffect(StatType.HP, value);
                    else player.stats.AddEffect(StatType.HP, value);
                    break;
            }
        }

        foreach (StatusEffect effect in _so.statusEffects) {
            if (forEnemy) target.ApplyStatus(effect);
            else player.ApplyStatus(effect);
        }

        _countdown.Start();
        _logo.fillAmount = 0;
    }

    public void Use(Player player) {
        if (_countdown.started) return;

        foreach (InstantEffect effect in _so.instantEffects) {
            int value = effect.Calculate(player.stats);
            switch (effect.type) {
                case InstantEffectType.Attack:
                    player.stats.AddEffect(StatType.HP, -value);
                    break;
                case InstantEffectType.Heal:
                    player.stats.AddEffect(StatType.HP, value);
                    break;
            }
        }

        foreach (StatusEffect effect in _so.statusEffects) {
            player.ApplyStatus(effect);
        }

        _countdown.Start();
        _logo.fillAmount = 0;
    }
}
