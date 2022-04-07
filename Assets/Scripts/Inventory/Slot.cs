using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private InventoryUI inventory;
    public int slotNum = 0;
    
    void Start()
    {
        inventory = transform.parent.parent.GetComponent<InventoryUI>();
        slotNum = transform.GetSiblingIndex();
    }
    public void MouseEnter()
    {
        inventory.ChangeDescription(slotNum);
    }

    public void MouseExit()
    {
        inventory.ChangeDescription(-1);
    }
}
