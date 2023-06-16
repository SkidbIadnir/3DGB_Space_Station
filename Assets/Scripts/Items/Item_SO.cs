using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Item_SO : ScriptableObject
{
    public enum ItemType {
        Object,
        PowerUp
    }
    
    [Header("General")]
    public string itemName;
    public ItemType type;
    [TextArea(15, 20)] public string description;
    public Sprite icon;
    public GameObject itemPrefab;
    
    [Header("Settings")]
    public int maxStack = 99;

    [Header("Market")]
    public bool sellable = true;
    public bool sellableMarket = true;
    public int cost;
    public int sellCost;

    /*
    public enum PowerUpAnim {
        None
    }

    [Header("Power-Ups")]
    public PowerUpAnim powerUpAnimation = powerUpAnimation.None;
    */
}