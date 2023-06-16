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
        Debug.Log(player.stats.total[effect.statusEffects[0].valueType]);
        player.ApplyStatus(effect.statusEffects[0]);
        number -= 1;
    }

}
