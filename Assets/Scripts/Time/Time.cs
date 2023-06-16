using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeType {
    Second,
    Minute,
    Hour
};

[Serializable]
public class MyTime
{
    [SerializeField] int _seconds = 0;
    [SerializeField] int _minutes = 0;
    [SerializeField] int _hours = 0;

    // Constructor
    public MyTime(int seconds = 0, int minutes = 0, int hours = 0) {
        _seconds = seconds;
        _minutes = minutes;
        _hours = hours;
        Set();
    }

    // Control time
    void Set() {
        if (_seconds >= 60) {
            _seconds = _seconds - 60;
            ++_minutes;
        }
        if (_minutes >= 60) {
            _minutes = _minutes - 60;
            ++_hours;
        }

        if (0 > _seconds) {
            --_minutes;
            _seconds = 60 + _seconds;
        }
        if (0 > _minutes) {
            --_hours;
            _minutes = 60 + _minutes;
        }
        if (_hours < 0) Reset();
    }

    // Manipulate time
    public void Add(int value, TimeType type = TimeType.Second) {
        switch (type) {
            case TimeType.Minute: _minutes += value; break;
            case TimeType.Hour: _hours += value; break;
            default: _seconds += value; break;
        }
        Set();
    }
    public void Reset(int seconds = 0, int minutes = 0, int hours = 0) {
        _seconds = seconds;
        _minutes = minutes;
        _hours = hours;
        Set();
    }

    // Get
    public int Get(TimeType type = TimeType.Second) {
        switch (type) {
            case TimeType.Minute: return _minutes;
            case TimeType.Hour: return _hours;
            default: return _seconds;
        }
    }
    public int GetAllSeconds() {
        return _seconds + (_minutes * 60) + (_hours * 3600);
    }
    public string GetString(string separator = ":") {
        string s = $"{(0 != _hours ? _hours.ToString() + separator : "")}";
        s += $"{(0 != _minutes || 0 != _hours ? _minutes.ToString() + separator : "")}";
        s += _seconds.ToString();
        return s;
    }
    public string GetFullString(string separator = ":") {
        string s = _hours.ToString() + separator;
        s += _minutes.ToString() + separator;
        s += _seconds.ToString();
        return s;
    }

    // Overload functions
    public override bool Equals(object o)
    {
        if(o == null)
            return false;
        MyTime other = o as MyTime;
        return (_seconds == other._seconds &&
                _minutes == other._minutes &&
                _hours == other._hours);
    }
    public override int GetHashCode()
    {
        return _seconds;
    }

    // Overload operator
    public static bool operator == (MyTime lhs, MyTime rhs) {
        return (lhs._seconds == rhs._seconds &&
                lhs._minutes == rhs._minutes &&
                lhs._hours == rhs._hours);
    }
    public static bool operator > (MyTime lhs, MyTime rhs) {
        if (lhs._hours > rhs._hours) return true;
        else if (lhs._hours != rhs._hours) return false;
        if (lhs._minutes > rhs._minutes) return true;
        else if (lhs._minutes != rhs._minutes) return false;
        if (lhs._seconds > rhs._seconds) return true;
        return false;
    }
    public static bool operator >= (MyTime lhs, MyTime rhs) {
        if (lhs == rhs) return true;
        else return lhs > rhs;
    }
    public static bool operator < (MyTime lhs, MyTime rhs) {
        if (lhs._hours < rhs._hours) return true;
        else if (lhs._hours != rhs._hours) return false;
        if (lhs._minutes < rhs._minutes) return true;
        else if (lhs._minutes != rhs._minutes) return false;
        if (lhs._seconds < rhs._seconds) return true;
        return false;
    }
    public static bool operator <= (MyTime lhs, MyTime rhs) {
        if (lhs == rhs) return true;
        else return lhs < rhs;
    }
    public static bool operator != (MyTime lhs, MyTime rhs) {
        return (lhs._seconds != rhs._seconds &&
                lhs._minutes != rhs._minutes &&
                lhs._hours != rhs._hours);
    }
    
    public static MyTime operator - (MyTime lhs, MyTime rhs) {
        return new MyTime(lhs._seconds - rhs._seconds,
                        lhs._minutes - rhs._minutes,
                        lhs._hours - rhs._hours);
    }
    public static MyTime operator + (MyTime lhs, MyTime rhs) {
        return new MyTime(lhs._seconds + rhs._seconds,
                        lhs._minutes + rhs._minutes,
                        lhs._hours + rhs._hours);
    }
}
