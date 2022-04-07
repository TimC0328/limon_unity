using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
        
    private GameObject slots;
    private Text description;

    void Awake()
    {
        slots = transform.GetChild(1).gameObject;
        description = transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>();
    }

    void Start()
    {
        inventory = Inventory.instance;
        UpdateInventory();
    }

    void UpdateInventory()
    {
        GameObject slot;
        for(int i = 0; i < inventory.items.Count; i++)
        {
            slot = slots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = inventory.items[i].icon;
        }
    }

    public void ChangeDescription(int pos)
    {
        if(pos == -1 || pos >= inventory.items.Count)
        {
            description.text = "Note:";
            return;
        }
        description.text = "Note: " + inventory.items[pos].description;
    }

    public void ToggleInventory()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }
}
