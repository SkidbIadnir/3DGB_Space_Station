using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Clock
{
    [SerializeField] MyTime _timeRequired = new MyTime();
    [SerializeField] MyTime _current = new MyTime();
    [SerializeField] bool _decreasing = false;
    public bool started = false;
    float _time = 0;

    // Constructor
    public Clock(bool decreasing = false) {
        _timeRequired = new MyTime();
        _decreasing = decreasing;
    }
    public Clock(MyTime time, bool decreasing = false) {
        _timeRequired = new MyTime(time.Get(TimeType.Second), time.Get(TimeType.Minute), time.Get(TimeType.Hour));
        _decreasing = decreasing;
    }

    public void Start() {
        if (_decreasing) _current = new MyTime(_timeRequired.Get(TimeType.Second), _timeRequired.Get(TimeType.Minute), _timeRequired.Get(TimeType.Hour));
        else _current = new MyTime();
        started = true;
    }

    // Control clock
    public void Stop() { started = false; }
    public void Resume() { started = true; }
    public void Reset() {
        if (_decreasing) _current.Reset(_timeRequired.Get(TimeType.Second), _timeRequired.Get(TimeType.Minute), _timeRequired.Get(TimeType.Hour));
        else _current.Reset();
    }

    // Update clock
    public void Update() {
        _time += Time.deltaTime;
        if (_time >= 1) {
            if (started) _current.Add(_decreasing ? -1 : 1);
            _time -= 1;
        }
    }

    // Manipulate clock
    public void Add(MyTime other) { _current = _current + other; }

    // Status
    public bool IsCompleted() {
        if (!started) return false;
        if (_decreasing) return new MyTime() == _current;
        else return _timeRequired <= _current;
    }

    public float GetPercentage() {
        if (_decreasing) { // To test
            MyTime done = _timeRequired - _current;
            if (_current == _timeRequired) return 0f;
            return (float)done.GetAllSeconds() / (float)_timeRequired.GetAllSeconds();
        } else {
            return (float)_current.GetAllSeconds() / (float)_timeRequired.GetAllSeconds();
        }
    }

    public MyTime GetLimit() { return _timeRequired; }
    public MyTime GetCurrent() { return _current; }
}
