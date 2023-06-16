using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeObjectUI : MonoBehaviour
{
    public TMP_InputField timeFstH;
    public TMP_InputField timeFstMin;
    public TMP_InputField timeFstScd;
    public TMP_Dropdown dropdown;
    public TMP_InputField timeScdH;
    public TMP_InputField timeScdMin;
    public TMP_InputField timeScdScd;
    public TMP_Text resText;

    void Start() {
        timeFstScd.text = "0";
        timeFstMin.text = "0";
        timeFstH.text = "0";
        timeScdScd.text = "0";
        timeScdMin.text = "0";
        timeScdH.text = "0";
    }

    public void DoTheThing() {
        string option = dropdown.options[dropdown.value].text;
        MyTime fst = new MyTime(int.Parse(timeFstScd.text), int.Parse(timeFstMin.text), int.Parse(timeFstH.text));
        MyTime scd = new MyTime(int.Parse(timeScdScd.text), int.Parse(timeScdMin.text), int.Parse(timeScdH.text));
        bool res = false;
        MyTime tres = new MyTime();

        switch(option) {
            case ">": res = fst > scd; break;
            case ">=": res = fst >= scd; break;
            case "<=": res = fst <= scd; break;
            case "<": res = fst < scd; break;
            case "!=": res = fst != scd; break;
            case "+": tres = fst + scd; break;
            case "-": tres = fst - scd; break;
            default: res = fst == scd; break;
        }

        if ("+" == option || "-" == option) resText.text = tres.GetFullString();
        else resText.text = res.ToString();
    }
}
