using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simulate the player
public class Dummy : MonoBehaviour
{
    public string _name;

    public Stats stats = new Stats();

    // Status
    [SerializeField] StatusManager _statusManager = new StatusManager(); 

    public void ApplyStatus(StatusEffect status) { _statusManager.ApplyStatus(status, stats); }
    public void RemoveStatus(StatusEffect status) { _statusManager.RemoveStatus(status, stats); }
    public bool HasDebuff() { return _statusManager.HasDebuff(); }
    
    void Start() {
        stats.Start();
    }

    void Update() {
        // Status
        _statusManager.Update(stats);

        if (stats.total[StatType.HP] <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().stats.total[StatType.HP] -= stats.total[StatType.Attack];
        }
    }
}
