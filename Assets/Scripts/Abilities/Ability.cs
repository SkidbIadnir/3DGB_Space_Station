using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] protected Player _player;
    [SerializeField] protected bool _playerIn;

    [Header("Hitboxs")]
    [SerializeField] protected bool _extraHitbox = false;
    // [SerializeField] protected GroupHitbox _hitboxs;
    [SerializeField] protected List<ExtraHitbox> _extras;
    [SerializeField] protected List<Dummy> _ins = new List<Dummy>();

    [Header("Effects")]
    // [SerializeField] protected AbilityManager _effects;
    [SerializeField] protected SO_Ability _so;
    [SerializeField] protected Clock _countdown;
    [SerializeField] protected Clock _repeatTime;
    [SerializeField] protected Image _logo;

    [Header("Attributes")]
    public Vector3 _offset;
    [SerializeField] protected Clock _duration;
    [SerializeField] protected Vector3 _movement;


    void Start() {
        _duration.Start();

        // Effects
        _countdown = new Clock(_so.countdown);
        // if (_so.repeatable) _repeatTime = new Clock(_so._repeatTime);

        // Player
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        // Effects
        
        
        _duration.Update();
        
        if (_duration.IsCompleted()) {
            Destroy(this);
        }
    }

    protected void HandleClock() {
        // Effects
        // _countdown.Update();
        // if (_countdown.started) UpdateAbilityDisplay();
        // if (_countdown.IsCompleted()) {
        //     _countdown.Stop();
        //     _countdown.Reset();
        // }

        // if (_so.repeatable) {
        //     _repeatTime.Update();
        //     if (_repeatTime.IsCompleted()) {
        //         // Inflige effect and status on every mob in hitbox
        //         _countdown.Reset();
        //     }
        // }

        // _duration.Update();
        // if (_duration.IsCompleted()) Destroy(this);
    }

    public void EnnemyHit(Dummy target, int value = 0) {
    }

    public void SetLogo(Image image) {
        // _effects._logo = image;
    }

    virtual protected void SpecialAction(Dummy dummy) {

    }

    // Hitboxs
    protected void ManageHitbox() {
        // if (_extraHitbox) {
        //     foreach (var extra in _extras)
        //         foreach (var enn in extra.ins)
        //             DummyFound(enn);   
        // }


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
}
