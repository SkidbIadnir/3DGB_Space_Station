using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPStatusUI : MonoBehaviour
{
    Player _player;

    public Slider hpSlider;
    public TMP_Text hpText;
    public Image fillImg;

    public Color lifeColor;
    public Color debuffColor;

    public TMP_Text attackText;
    public TMP_Text defenseText;

    void Start() {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        fillImg.color = lifeColor;
    }

    void Update() {
        int percentage = GetPercentage();
        hpSlider.value = percentage;
        hpText.text = $"{percentage}%";

        if (_player.HasDebuff()) {
            fillImg.color = debuffColor;
        } else {
            fillImg.color = lifeColor;
        }

        attackText.text = _player.stats.total[StatType.Attack].ToString();
        defenseText.text = _player.stats.total[StatType.Defense].ToString();
    }

    int GetPercentage() {
        return (_player.stats.total[StatType.HP] * 100) / _player.stats.total[StatType.HP_MAX];
    }
}
