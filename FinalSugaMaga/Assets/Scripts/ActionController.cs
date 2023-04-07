using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // 습득 가능한 최대거리.

    private RaycastHit hitInfo; // 충돌체 정보 저장.
    private bool pickupActivated = false; // 습득 가능할 시 true.


    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    public Inventory theInventory;
    [SerializeField]
    public PotionInventory thepotionInventory;

    void Start()
    {
       // actionText = GameObject.Find("Canvas").transform.Find("ShowText").transform.Find("actionTxT").GetComponent<Text>();
     //   theInventory = GameObject.Find("Canvas").transform.Find("Weapon_Inventory").GetComponent<Inventory>();
      //  thepotionInventory = GameObject.Find("Canvas").transform.Find("Potion_Inventory").GetComponent<PotionInventory>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Equipment)
                {
                    if (theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item) == 0)
                    {
                    Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");
                    Destroy(hitInfo.transform.gameObject);
                    InfoDisappear();
                    }
                }
                else
                {
                    if (thepotionInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item) == 0)
                    {
                    Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");
                    Destroy(hitInfo.transform.gameObject);
                    InfoDisappear();
                    }
                }
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        if (hitInfo.transform.GetComponent<ItemPickUp>() == null)
        {
            return;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (theInventory.slots[i].item == null)
                {
                    continue;
                }
               
                if (theInventory.slots[i].item.itemName == (hitInfo.transform.GetComponent<ItemPickUp>().item.itemName))
                {
                    actionText.text = "이미 가지고 있는 아이템입니다.";
                    return;
                }
            }
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득" + "<color=yellow>" + "(E)" + "</color>";
        }

    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
