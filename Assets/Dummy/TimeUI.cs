using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using TMPro;

public class TimeUI : MonoBehaviour
{
    MyTime time;
    public int tscd = 0;
    public int tmin = 0;
    public int th = 0;
    public string startScd = "0";
    public string startMin = "0";
    public string startH = "0";

    public TMP_Text shortTime;
    public TMP_Text full;
    public TMP_InputField seconds;
    public TMP_InputField minutes;
    public TMP_InputField hours;
    public Toggle update;

    void Start() {
        time = new MyTime(tscd, tmin, th);
        shortTime.text = time.GetString();
        full.text = time.GetFullString();
        seconds.text = startScd;
        minutes.text = startMin;
        hours.text = startH;
    }

    float elapsed = 0;
    void Update() {
        elapsed += Time.deltaTime;
        if (1 <= elapsed) {
            elapsed = elapsed % 1f;
            if (update.isOn) {
                AddSecond();
                AddMinute();
                AddHour();
            }
        }
    }

    public void AddSecond() {
        time.Add(int.Parse(seconds.text), TimeType.Second);
        shortTime.text = time.GetString();
        full.text = time.GetFullString();
    }
    public void AddMinute() {
        time.Add(int.Parse(minutes.text), TimeType.Minute);
        shortTime.text = time.GetString();
        full.text = time.GetFullString();
    }
    public void AddHour() {
        time.Add(int.Parse(hours.text), TimeType.Hour);
        shortTime.text = time.GetString();
        full.text = time.GetFullString();
    }
}
