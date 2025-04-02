using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    public GameObject inventoryObj;
    bool isInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        inventory = Inventory.instance;

        slots = slotHolder.GetComponentsInChildren<Slot>();
        inventory.onSlotCntChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;

        inventoryObj.SetActive(isInventory);
    }

    private void SlotChange(int value)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;

            if (i < inventory.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }

            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Update()
    {
        InventoryOnOff();
    }

    private void InventoryOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventory = !isInventory;
            inventoryObj.SetActive(isInventory);
        }
    }

    public void AddSlot()
    {
        inventory.SlotCnt++;
    }

    private void RedrawSlotUI()
    {
        for (int i = 0;i < slots.Length;i++)
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
