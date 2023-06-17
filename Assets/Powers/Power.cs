using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public SO_Ability effect;
    public int number = 1;

    private Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
    }

    public void UsePowerUp()
    {
        if (effect.statusEffects.Count > 0)
            player.ApplyStatus(effect.statusEffects[0]);
        else
            player.stats.AddEffect(effect.instantEffects[0].valueType, effect.instantEffects[0].Calculate(player.stats));
        number -= 1;
    }

}
