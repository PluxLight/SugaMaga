using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SlotSelect : MonoBehaviour
{
    public GameObject weapons;
    GameObject child = null;
    public Transform[] objList;

    [SerializeField]
    private GameObject go_SlotsParent;

    // 슬롯들
    private Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();

        // child = weapons.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        // child.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
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
    }

    void WeaponActive(int slot)
    {
        int childs = weapons.transform.childCount;

        for (int i = 0; i < childs; i++)
        {
            weapons.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        weapons.transform.GetChild(slot).gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    // 슬롯에 아이템 채워넣기
    public void AcquireItem(Item _item, int _count = 1)
    {
        // 먹는 아이템이 장비가 아닐 경우(회복템일 경우)
        if (Item.ItemType.Equipment != _item.itemType)
        {
            // 아이템이 있을 때 갯수 늘려주기
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        // 아이템이 없을 때 슬롯에 채워 넣기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
