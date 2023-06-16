using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{
    public Clock clock;

    public TMP_InputField hs;
    public TMP_InputField mins;
    public TMP_InputField scds;
    public Toggle decreasing;
    public TMP_Text current;
    public TMP_Text status;
    public TMP_InputField ahs;
    public TMP_InputField amins;
    public TMP_InputField ascds;

    void Start() {
        hs.text = "0";
        mins.text = "0";
        scds.text = "10";
    }

    void Update() {
        clock.Update();
        
        status.text = $"Completed: {clock.IsCompleted().ToString()}";
        
        current.text = clock.GetCurrent().GetFullString();
    }

    public void StartClock() {
        clock = new Clock(new MyTime(int.Parse(scds.text), int.Parse(mins.text), int.Parse(hs.text)), decreasing.isOn);
        clock.Start();
    }
    public void StopClock() { clock.Stop(); }
    public void ResumeClock() { clock.Resume(); }
    public void ResetClock() { clock.Reset(); }

    public void Add() { clock.Add(new MyTime(int.Parse(ascds.text), int.Parse(amins.text), int.Parse(ahs.text))); }
}
