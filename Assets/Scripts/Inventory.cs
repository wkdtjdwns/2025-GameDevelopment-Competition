using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnSlotCntChange(int value);
    public OnSlotCntChange onSlotCntChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCntChange?.Invoke(slotCnt);
        }
    }

    private void Start()
    {
        SlotCnt = 4;

        if (onSlotCntChange == null)
        {
            onSlotCntChange += (value) => { Debug.Log("슬롯 수 변경: " + value); };
        }

        if (onChangeItem == null)
        {
            onChangeItem += () => { Debug.Log("아이템이 추가됨."); };
        }
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            onChangeItem?.Invoke();
            return true;
        }

        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItem fieldItem = collision.GetComponent<FieldItem>();
            if (AddItem(fieldItem.GetItem()))
            {
                fieldItem.DestroyItem();
            }
        }
    }
}
