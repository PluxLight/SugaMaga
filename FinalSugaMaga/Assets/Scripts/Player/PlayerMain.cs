using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerMain : MonoBehaviour
{
	[SerializeField]
	private PlayerController playerController;

    [SerializeField]
    private GameObject hitFx;
    [SerializeField]
    private GameObject healFx;
    [SerializeField]
    private Transform cameraArm;

    [SerializeField]
    public Inventory theInventory;
    public PotionInventory thepotionInventory;

    [SerializeField]
    private GameObject Cookie;
    [SerializeField]
    private GameObject Character;
    [SerializeField]
    private GameObject DefeatUI;


    public static bool isMove = false;
    public static Boolean canMove;
    public static bool onGround = true;
    public static bool isJump = false;


    public static bool isDead = false;

    public static Boolean isDamaged = false;

    public static bool canClick = true;
    public static bool isAttack = false;
    public static int combo = 0;

    public static Boolean isDodge = false;

    public float hpmax = 100f;
    public float hp;

    public Image plazma;
    private bool _wait;
    public PhotonView pv;
    // ---------------아이템관련---------------


    //--------------------아이템관련----------------
    //public override bool SetHp(float hp, float hpMax) {
    //    if (hp > hpMax) { hp = hpMax; }
    //    hpNow = hp;
    //    hpMaxNow = hpMax;
    //}
    private void Awake()
    {
        GameSceneManager gameSceneManager = GameObject.Find("GameSceneManager").gameObject.GetComponent<GameSceneManager>();
        transform.position = GameSceneManager.pos;
        transform.eulerAngles = new Vector3(GameSceneManager.rot.x, GameSceneManager.rot.y, GameSceneManager.rot.z);

        pv = GetComponent<PhotonView>();
        if (pv == null)
        {
            Debug.LogError($"{nameof(pv)}가 없습니다");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PlayerAttack"))
        {
            isDamaged = true;
            HpDown(10);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // 나중에 switch case 문으로 바꾸기
        if (other.gameObject.CompareTag("Item")) { 
            Debug.Log("Get item"); 
        }
        
        if (other.gameObject.CompareTag("DeadSpace")) { 
            Debug.Log("Trigger Dead");
            hp = 0; isDead = true;
            playerController.PlayerDead();
        }
        //if (other.gameObject.CompareTag("Attack"))
    }

    //일단 hp--; 지만 무기데미지 * @ 필요.... 뭐 무기뎀*거리비례뎀 등등...
    public void HpDown(int value)
    {
        hp -= value;
        StartCoroutine(HitEffect(1f));
        isDamaged = false;

        if (hp <= 0)
        {
            // hp = 0; isDead = true;
            // playerController.PlayerDead();
            Debug.Log("HP 0.... Player Dead");
            Debug.Log("inventory items leng : " + theInventory.slots.Length);

            // 죽으면 아이템 드롭 및 캐릭터 -> 쿠키 변형.
            for (int i = 0; i < theInventory.slots.Length; i++)
            {
                Debug.Log("theInventory i번째 " + theInventory.slots[0].item);
                if (theInventory.slots[i].item != null)
                {
                    Debug.Log("아이템 드랍했습니다. " + theInventory.slots[i].item.name);
                    Instantiate(theInventory.slots[i].item.itemPrefab, transform.position = new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);
                }
            }
            for (int i = 0; i < thepotionInventory.slots.Length; i++)
            {
                Debug.Log("theInventory i번째 " + thepotionInventory.slots[0].item);
                if (thepotionInventory.slots[i].item != null)
                {
                    Instantiate(thepotionInventory.slots[i].item.itemPrefab, transform.position = new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);
                    Debug.Log("아이템 드랍했습니다. " + thepotionInventory.slots[i].item.name);
                }
            }

            //Cookie.SetActive(true);
            Character.transform.gameObject.SetActive(false);
            Instantiate(Cookie, transform.position = new Vector3(Character.transform.position.x, 5, Character.transform.position.z), Quaternion.identity);

            DefeatUI.SetActive(true);
        }
    }

    public void HpUp(int upHp, int slotNum)
    {
        // hp가 0일때 바로 사망처리 하기 위해서 0일땐 회복 불가
        Debug.Log("HpUp 실행");
        Debug.Log(thepotionInventory.slots.Length);
        if (hp <= 0) { return; }
        StartCoroutine(HealEffect(2.3f, slotNum));

        hp += upHp;

        if (hp > hpmax) { hp = hpmax; }
    }

    IEnumerator HitEffect(float second) 
    {
        hitFx.SetActive(true);
        yield return new WaitForSeconds(second);
        hitFx.SetActive(false);
    }
    IEnumerator HealEffect(float second, int slotnum)
    {

        canMove = false;
        healFx.SetActive(true);
        gameObject.GetComponent<Animator>().SetTrigger("drinkPotion");

        cameraArm.GetComponent<ActionController>().thepotionInventory.PotionDecrease(slotnum);
        yield return new WaitForSeconds(second);

        healFx.SetActive(false);
        canMove = true;
        playerController.potionDrinking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetUserCustom");
        // Set Default hp
        hp = hpmax;
       // theInventory = GameObject.Find("Canvas").transform.Find("Weapon_Inventory").GetComponent<Inventory>();
      //  DefeatUI = GameObject.Find("Canvas").transform.Find("Defeat").gameObject;
      //  thepotionInventory = GameObject.Find("Canvas").transform.Find("Potion_Inventory").GetComponent<PotionInventory>();
        hitFx.SetActive(false);
        healFx.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        // Getting zone current safe zone values
        var zonePos = CoolBattleRoyaleZone.Zone.Instance.CurrentSafeZone.Position;
        var zoneRadius = CoolBattleRoyaleZone.Zone.Instance.CurrentSafeZone.Radius;
        // Checking distance between player and circle
        var dstToZone = Vector3.Distance(new Vector3(transform.position.x, zonePos.y, transform.position.z),
                                           zonePos);
        // Checking if we inner of circle or not by radius and if not, start applying damage to health
        if (dstToZone > zoneRadius && !_wait)
        {
            
                StartCoroutine(DoDamageCoroutine());
                StartCoroutine(ShowPlazma());
            
        }
    }

    IEnumerator GetUserCustom()
    {
        yield return new WaitForSeconds(5f);
        GameObject characterManager = transform.Find("CharacterManager").gameObject;
        Debug.Log("텍스트 " + gameObject.transform.parent.GetComponent<PlayerController>().nickName.text);
        string uid = gameObject.transform.parent.GetComponent<PlayerController>().nickName.text;
        Debug.Log("텍스트 2" + uid);
        characterManager.GetComponent<StartCharacter>().GetCostume(uid);
    }
    private IEnumerator DoDamageCoroutine()
    {
        _wait = true;
        DoDamage();
        yield return new WaitForSeconds(1); // Waiting between damages.
        _wait = false;
    }

    private void DoDamage()
    {
        hp -= CoolBattleRoyaleZone.Zone.Instance.CurStep + 1; // Applying damage based on current step index
                                                              // Then choose text color : red if health less than 25 and green if greater than 25
        /*var hpColor = Health > 25 ? "<color=green>" : "<color=red>";
        if (HealthText)
            HealthText.text = "Health: " + hpColor + Health + "</color>"; // Then setup this color to text*/
        if (hp <= 0)
            Destroy(gameObject); // And if health amount is zero,destroying the simple player
    }

    IEnumerator ShowPlazma()
    {
        GameObject.Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, UnityEngine.Random.Range(0.2f, 0.3f));
        /*        plazma.color = new Color(1, 1, 1, UnityEngine.Random.Range(0.2f, 0.3f));
        */
        yield return new WaitForSeconds(2);
        GameObject.Find("Image").GetComponent<Image>().color = Color.clear;
    }
    private void OnParticleCollision()//스킬맞았을때
    {
        Debug.Log("스킬맞앗다");
        hp -= 10;
    }




}
