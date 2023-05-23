using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInventory : MonoBehaviour
{
    // public static bool inventoryActivated = false;

    // 필요한 컴포넌트
    // [SerializeField]
    // private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;


    public GameObject potion;
    GameObject child = null;
    public Transform[] objList;
    int selected;


    // 슬롯들
    public Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    void Update()
    {
        //TryOpenInventory();

     
    }

        // Update is called once per frame
        
        public void WeaponActive(int slot)
        {
            int childs = potion.transform.childCount;

            for (int i = 0; i < childs; i++)
            {
                potion.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            potion.transform.GetChild(slot).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            child = potion.transform.GetChild(slot).gameObject;
            selected = slot;
        }

    /*void PotionDrop(int slot)
    {
        Slot clearSlot = slots[slot];
        clearSlot.ClearSlot();
    }
    */

    public Boolean PotionDecrease(int slot)
    {
        if (slots[slot].item != null && slots[slot].itemCount!=0)
        {
            slots[slot].SetSlotCount(-1);
            return true;
        }
        return false;
    }

    // 인벤토리 열기
    //private void TryOpenInventory()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        inventoryActivated = !inventoryActivated;

    //        if (inventoryActivated)
    //            OpenInventory();
    //        else
    //            CloseInventory();
    //    }
    //}

    //private void OpenInventory()
    //{
    //    go_InventoryBase.SetActive(true);
    //}

    //private void CloseInventory()
    //{
    //    go_InventoryBase.SetActive(false);
    //}

    // 슬롯에 아이템 채워넣기
    public int AcquireItem(Item _item, int _count = 1)
    {
        // 먹는 아이템이 회복템일 경우
        if (Item.ItemType.Ingredient == _item.itemType)
        {
            int cnt = 0;
            // 아이템이 있을 때 갯수 늘려주기
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    cnt++;
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return 0;
                    }
                }
                if (cnt == 2)
                {
                    return 1;
                }
            }
            // 아이템이 없을 때 슬롯에 채워 넣기
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
                    return 0;
                }
            }
        }
        return 0;
     }
}