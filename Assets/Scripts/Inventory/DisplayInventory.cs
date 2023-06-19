using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;

    public Inventory inventory;
    public GameObject slotPrefab;
    Dictionary<ItemSlot, GameObject> itemsDisplayed = new Dictionary<ItemSlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // inventory.OnInventoryUpdated += Inventory_OnInventoryUpdated
        CreateDisplay();
    //     GameObject.Find("Canvas/Money/Cost").GetComponent<TextMeshProUGUI>().text = inventory.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateDisplay();
        // GameObject.Find("Canvas/Money/Cost").GetComponent<TextMeshProUGUI>().text = inventory.Money.ToString();
    }

    // public void ResetInv()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }
    //     itemsDisplayed.Clear();
    //     CreateDisplay();
    // }

    public void CreateDisplay()
    {
        List<ItemSlot> temp = inventory.GetItems();
        for (int i = 0; i < temp.Count; ++i) {
            var obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponent<Image>().sprite = temp[i].GetItem().GetItemSO().icon;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = temp[i].GetAmount().ToString("n0");
            // obj.GetComponent<ClickableObject>().item = temp[i].item;
            itemsDisplayed.Add(temp[i], obj);
        }
    }

    public void Inventory_OnInventoryUpdated()
    {
        foreach (Transform child in transform) GameObject.Destroy(child.gameObject);
        itemsDisplayed.Clear();
        List<ItemSlot> temp = inventory.GetItems();
        CreateDisplay();
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }

    // private ItemObject GetCopy(ItemObject item)
    // {
    //     if (ItemType.Food == item.type)
    //     {
    //         FoodObject food = new FoodObject { };
    //         food.Copy(item);
    //         return food as ItemObject;
    //     }
    //     else if (ItemType.Equipment == item.type)
    //     {
    //         EquipmentObject equipment = new EquipmentObject { };
    //         equipment.Copy(item);
    //         return equipment as ItemObject;
    //     }
    //     else
    //     {
    //         DefaultObject obj = new DefaultObject { };
    //         obj.Copy(item);
    //         return obj as ItemObject;
    //     }
    // }

    /*************************/
    /*   MANAGE CLICK ITEM   */
    /*************************/
    // public void HandleClick(ItemObject item)
    // {
    //     Debug.Log("hello");
    //     //current = item;
    //     /*if (player.withMerchant)
    //     {
    //         inventory.Sell(item, 1);
    //     }
    //     else
    //     {
    //         itemHUD.DisplayItemHUD(item);
    //     }*/
    // }
}