using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AreaofEffect : MonoBehaviour
{
    [Header("General")]
    //public SAbility sa;
    bool ready = false;

    [Header("Duration")]
    [SerializeField] protected Clock _duration;
    [SerializeField] protected bool _repeat;
    [SerializeField] protected Clock _repeatTime;

    [Header("Hitboxs")]
    [SerializeField] protected bool _extraHitbox = false;
    // [SerializeField] protected GroupHitbox _hitboxs;
    [SerializeField] protected List<ExtraHitbox> _extras;
    [SerializeField] protected List<Dummy> _ins = new List<Dummy>();
    [SerializeField] protected bool _playerIn;

    [Header("Spe")]
    bool _onContact = false;
    protected List<Dummy> _encounters = new List<Dummy>();
    bool _encounterPlayer = false;

    [Header("Movement")]
    public float offset;
    [SerializeField] protected bool _movable;
    public bool extraMovement;
    public float _speed;

    #region General
    /*public void Spawn(SAbility ability) {
        sa = ability;

        _duration = new Clock(sa.so.duration);
        if (sa.so.repeatable) {
            _repeat = true;
            _repeatTime = new Clock(sa.so.repeatTime);
        }

        _onContact = sa.so.onContact;

        _duration.Start();
        if (_repeat) _repeatTime.Start();

        ready = true;
    }*/
    void Update() {
        if (!ready) return;

        // Duration
        _duration.Update();
        if (_repeat) _repeatTime.Update();

        if (_repeatTime.IsCompleted()) {
            CheckHitbox();
            _repeatTime.Reset();
        }
        if (_duration.IsCompleted()) Kill();

        // Movement
        if (_movable) {
            // transform.position += _movement * Time.deltaTime;
            if (extraMovement) {
                foreach (var extra in _extras) extra.Movement(_speed);
            } else transform.position += transform.forward * _speed * Time.deltaTime;
        }

        // On Contact
        if (_onContact) OnContactHitboxs();
    }
    #endregion

    #region Duration
    void Kill() {
        //if (sa.so.instant) CheckHitbox();
        Destroy(this.gameObject);
    }
    #endregion

    #region Hitboxs
    void CheckHitbox() {
        if (_extraHitbox) {
            foreach (var extra in _extras) {
                Debug.Log(extra.ins.Count);
                if (extra.playerIn) _playerIn = true;
                foreach (var enn in extra.ins) {
                    DummyFound(enn);
                } 
            }
        }
        //foreach (var d in _ins) sa.EnnemyHit(d);

        Debug.Log(_playerIn);
        //if (_playerIn) sa.PlayerHit();        
    }
    void OnContactHitboxs() {
        if (_extraHitbox) {
            foreach (var extra in _extras) {
                if (extra.playerIn) _playerIn = true;
                foreach (var enn in extra.ins)
                    DummyFound(enn);
            }
        }

        foreach (var dummy in _ins) {
            if (_encounters.Contains(dummy)) continue;
            else {
                _encounters.Add(dummy);
                //sa.EnnemyHit(dummy);
            }
        }

        if (_playerIn) {
            if (!_encounterPlayer) {
                _encounterPlayer = true;
                //sa.PlayerHit();
            }
        }
        if (_playerIn) _encounterPlayer = true;

        _ins.Clear();
    }

    public void DummyFound(Dummy d) { if (!_ins.Contains(d)) _ins.Add(d); }
    public void DummyLost(Dummy d) { if (_ins.Contains(d)) _ins.Remove(d); }

    void OnTriggerEnter(Collider other) {
        if (_extraHitbox) return;

        if (other.gameObject && other.gameObject.GetComponent<Dummy>())
            DummyFound(other.gameObject.GetComponent<Dummy>());
        if (other.gameObject && other.gameObject.GetComponent<Player>())
            _playerIn = true;
    }

    void OnTriggerExit(Collider other) {
        if (_extraHitbox) return;

        foreach (var dummy in _ins) {
            if (other.gameObject && other.gameObject.GetComponent<Dummy>() == dummy)
                DummyLost(other.gameObject.GetComponent<Dummy>());
        }
        if (other.gameObject && other.gameObject.GetComponent<Player>())
            _playerIn = false;
    }
    #endregion
}
