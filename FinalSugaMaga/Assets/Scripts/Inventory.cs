using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public static bool inventoryActivated = false;

    // 필요한 컴포넌트
    // [SerializeField]
    // private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;


    public GameObject weapons;
    GameObject child = null;
    public Transform[] potionList;
    int selected;


    // 슬롯들
    public Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {


     /*   if (Input.GetKey(KeyCode.Alpha1))
        {
            WeaponActive(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            WeaponActive(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            WeaponActive(2);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            WeaponActive(3);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            WeaponActive(4);
        }

        else if (Input.GetKeyDown(KeyCode.F) && child != null)
        {
            Debug.Log("무기 F");
            WeaponDrop(selected);
        }*/
    }

    public void WeaponActive(int slot)
    {
        int childs = weapons.transform.childCount;

        for (int i = 0; i < childs; i++)
        {
            weapons.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        weapons.transform.GetChild(slot).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        child = weapons.transform.GetChild(slot).gameObject;
        selected = slot;
    }
    
    void WeaponDrop(int slot)
    {
        Slot clearSlot = slots[slot];
        clearSlot.ClearSlot();
    }



    // 슬롯에 아이템 채워넣기
    public int AcquireItem(Item _item, int _count = 1)
    {
        if (_item.itemType == Item.ItemType.Equipment) {
            // 슬롯에 같은 아이템이 이미 존재하는지 검사
            int cnt = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    cnt++;
                    if (slots[i].item == _item)
                    {
                        // 슬롯에 같은 아이템이 이미 있으면 추가하지 않고 함수를 종료
                        return 1;
                    }
                }
                // 아이템이 없을 때 슬롯에 채워 넣기
                else if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
               
                    return 0;
                }
                if (cnt == 5)
                {
                    return 1;
                }
            }
        }
        return 0;
    }


}
