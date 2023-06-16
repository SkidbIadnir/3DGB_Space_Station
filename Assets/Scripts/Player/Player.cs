using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum PlayerClass {
    Jaljada,
    Heleumeseu
};

public class Player : MonoBehaviour
{
    // Stats
    public Stats stats = new Stats();

    // Status
    [SerializeField] StatusManager _statusManager = new StatusManager(); 
            

    void Start() {
        // if (MenuManager.Instance.InALevel) {
        //     Debug.Log("In a level");
        //     int val = MenuManager.Instance.GetClass();
        //     // if (val == 0) SetClass(PlayerClass.Jaljada);
        //     // else SetClass(PlayerClass.Heleumeseu);
        // }
        //SetClass(PlayerClass.Jaljada);
        stats.Start();
    }

    void Update() {
        // Status
        _statusManager.Update(stats);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region Status
    public void ApplyStatus(StatusEffect status) { _statusManager.ApplyStatus(status, stats); }
    public void RemoveStatus(StatusEffect status) { _statusManager.RemoveStatus(status, stats); }
    public bool HasDebuff() { return _statusManager.HasDebuff(); }
    public bool HasStatus(StatusEffect status) { return _statusManager.HasStatus(status); }
    #endregion

    // Inventory
    // public void ItemCollected(Item item)
    // {

    // }

    // Endgame
    void Die()
    {

    }

    public void NewGame()
    {
        //_killCounter = 0;
    }

    public void Use()
    {
        // if ()
    }
}
