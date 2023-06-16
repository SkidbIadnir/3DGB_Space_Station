using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Item_SO _itemSO;

    public Item_SO GetItemSO() { return _itemSO; }
}
