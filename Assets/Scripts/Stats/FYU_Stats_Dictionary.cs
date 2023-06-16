using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsPair {
    public StatsPair(StatType key, int value) { Key = key; Value = value; }
    public StatType Key;
    public int Value;
}

[Serializable]
public class FYU_Stats_Dictionary : IEnumerable<StatsPair>
{
    [SerializeField] List<StatsPair> display = new List<StatsPair>();
    Dictionary<StatType, int> values = new Dictionary<StatType, int>();

    // Constructor
    public FYU_Stats_Dictionary(Dictionary<StatType, int> values) {
        foreach (KeyValuePair<StatType, int> pair in values)
            display.Add(new StatsPair(pair.Key, pair.Value));
        
        values = new Dictionary<StatType, int>();
        foreach (StatsPair pair in display)
            values.Add(pair.Key, pair.Value);
    }
    public FYU_Stats_Dictionary(FYU_Stats_Dictionary others) { // Copy
        display.Clear();
        values = new Dictionary<StatType, int>();

        foreach (KeyValuePair<StatType, int> pair in others.values)
            display.Add(new StatsPair(pair.Key, pair.Value));
        
        values = new Dictionary<StatType, int>();
        foreach (StatsPair pair in display)
            values.Add(pair.Key, pair.Value);
    }

    public void Start() {
        values = new Dictionary<StatType, int>();
        foreach (StatsPair pair in display)
            values.Add(pair.Key, pair.Value);
    }

    // Deconstructor
    ~FYU_Stats_Dictionary() {
        display.Clear();
        values.Clear();
    }

    // List/Dictionary functions
    public bool Contains(StatType key) { return values.ContainsKey(key); }

    public int Count() { return display.Count; }

    // Get and Set
    int GetValue(StatType key) { return values[key]; }

    void SetValue(StatType key, int value) {
        if (values.ContainsKey(key)) {
            values[key] = value;
            SetDisplay(key, value);
        } else {
            values.Add(key, value);
            display.Add(new StatsPair(key, value));
        }
    }
    void SetDisplay(StatType key, int value) {
        foreach (StatsPair pair in display)
            if (pair.Key == key) {
                pair.Value = value;
                break;
            }
    }

    // Overload operator
    public int this[StatType key] {
        get => GetValue(key);
        set => SetValue(key, value);
    }
    
    // IEnumerator
    public IEnumerator<StatsPair> GetEnumerator()
    {
        return display.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return display.GetEnumerator();
    } 
}
