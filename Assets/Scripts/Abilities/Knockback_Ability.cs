using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback_Ability : Ability
{
    [Header("Knockback")]
    public int knockbackPower = 1;

    void Start() {
        _duration.Start();
        
    }

    void Update() {
        _duration.Update();
        if (_duration.IsCompleted()) {
            Destroy(this);
        }
    }

    protected override void SpecialAction(Dummy dummy) {
        dummy.transform.position += Vector3.back * knockbackPower;
    }
}
