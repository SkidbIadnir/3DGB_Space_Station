using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class StatusManager
{
    [SerializeField] List<StatusEffect> _onGoingStatus = new List<StatusEffect>();
    
    // UI
    [SerializeField] bool _ui = false;
    [SerializeField] Transform _content;
    [SerializeField] GameObject _statusPrefab;
    [SerializeField] List<GameObject> _statusUI = new List<GameObject>();

    public void Update(Stats targetStats) {
        List<int> over = new List<int>();

        for (int i = 0; _onGoingStatus.Count > i; ++i) {
            StatusEffect effect = _onGoingStatus[i];
            effect.duration.Update();
            if (effect.repeatable) effect.repeatTime.Update();
            
            if (_ui) DisplayStatusTime(_statusUI[i], effect);

            if (effect.duration.IsCompleted())
                over.Add(i);

            if (effect.repeatable && effect.repeatTime.IsCompleted()) {
                 int value = effect.Calculate(targetStats);
                 targetStats.AddEffect(effect.valueType, value);
                 effect.repeatTime.Reset();
            }
        }

        foreach (int idx in over)
            Remove(idx, targetStats);
    }

    public void ApplyStatus(StatusEffect status, Stats targetStats) {
        if (_ui) {
            GameObject obj = GameObject.Instantiate(_statusPrefab, _content);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = status._sprite;
            _statusUI.Add(obj);
            DisplayStatusTime(obj, status);
        }
    
        int value = status.Calculate(targetStats);
        targetStats.AddEffect(status.valueType, value);
        
        if (!status.infinite) status.duration.Start();
        if (status.repeatable) status.repeatTime.Start();

        _onGoingStatus.Add(status);
    }

    public void RemoveStatus(StatusEffect status, Stats targetStats) {
        for (int i = 0; _onGoingStatus.Count > i; ++i)
            if (status == _onGoingStatus[i]) Remove(i, targetStats);
    }

    void Remove(int idx, Stats targetStats) {
        StatusEffect effect = _onGoingStatus[idx];
        
        if (!effect.infinite) {
            effect.duration.Stop();
            effect.duration.Reset();
        }
        if (effect.repeatable) {
            effect.repeatTime.Stop();
            effect.repeatTime.Reset();
        }

        if (!_onGoingStatus[idx].repeatable) {
            int value = effect.Calculate(targetStats);
            targetStats.RemoveEffect(effect.valueType, value);
        }
        _onGoingStatus.RemoveAt(idx);
        
        if (_ui) {
            GameObject.Destroy(_statusUI[idx]);
            _statusUI.RemoveAt(idx);
        }
    }

    void DisplayStatusTime(GameObject ui, StatusEffect effect) {
        if (effect.infinite) { ui.transform.GetChild(1).GetComponent<TMP_Text>().text = "inf"; return; }
        
        MyTime time = effect.duration.GetCurrent();
        if (0 == time.Get(TimeType.Minute)) ui.transform.GetChild(1).GetComponent<TMP_Text>().text = time.Get(TimeType.Second).ToString();
        else ui.transform.GetChild(1).GetComponent<TMP_Text>().text = time.Get(TimeType.Minute).ToString() + "m";
    }

    public bool HasDebuff() {
        foreach (StatusEffect effect in _onGoingStatus) {
            if (StatusType.Debuff == effect.status) return true;
        }
        return false;
    }

    public bool HasStatus(StatusEffect status) {
        foreach (StatusEffect effect in _onGoingStatus) {
            if (status == effect) return true;
        }
        return false;
    }
}
