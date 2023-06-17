using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHP : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "HP : " + player.stats.total[StatType.HP] + "/" + player.stats.total[StatType.HP_MAX];
    }
}
