using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;


public class PlayerController : MonoBehaviourPun
{
    public GameManager gameManager;

    private Animator animator;  
    private Rigidbody rigid;

    private Vector3 moveDir;
    private Vector3 DodgeDir;

    private float scrollWheel;
    private Vector3 cameraDir;

    private Vector3 boxSize = new Vector3(1.5f, 0.6f, 1.5f);
    private Vector3 feetpos;

    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cookie;

	[SerializeField]
	private GameObject weaponL;
	[SerializeField]
	private GameObject weaponR;

	[SerializeField]
	private Transform cameraArm;
    [SerializeField]
    private Transform playerCamera;

	public float playerSpeed = 5.5f;
    public float jumpPower = 7.0f;
	public float cameraMoveSpeed = 2.0f;
    public float cameraScrollSpeed = 1000.0f;

    public PotionInventory thepotionInventory;
    private int potionHp;

    private PhotonView pv;


    // --------------스킬관련-------------
    private Coroutine attackCoroutine; //공격후딜
    string controllerPath;
    float fireDelay;
    public bool equip;
    string equipIdx;
    Weapon w;
    WeaponData weaponData;
    string url = "PlayerModel/root/pelvis/spine_01/spine_02/spine_03" +
              "/clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"; // 무기가 보관된 경로
    Boolean thsSkill=false;
    Boolean ssSkill=false;
    int magic;

    string nowLayer;
    string changeLayer;
    //------------------------------

    //--------아이템관련-----------------
    //public Boolean canMove;  //먹는동안 못움직이게하기
   
    public Boolean potionDrinking = false; //마시고있는가 판단 
    string nowWeapon;
    int slotNumber;
    public GameObject UI;
    FillAmount fillAmount;
    public Boolean usingSkill;
    //--------------------------------------
    public Text nickName;

    //-----------MimiMap.............
    public GameObject MiniMapP;
    //---------------------

    private void Awake()
    {
        
        pv = GetComponent<PhotonView>();
        if (pv == null)
        {
            Debug.LogError($"{nameof(pv)}가 없습니다");
        }
        
    }

    void Start()
    {

        nickName.text = photonView.Owner.NickName;
     //   UI = GameObject.Find("UIController").gameObject;
     //   thepotionInventory = GameObject.Find("Canvas").transform.Find("Potion_Inventory").GetComponent<PotionInventory>();

        if (pv.IsMine)
        {
            Camera.main.GetComponent<SmoothFollow>().playerCamera = GameObject.Find("Player Camera").transform;
            Camera.main.GetComponent<SmoothFollow>().cameraArm = GameObject.Find("Camera Arm").transform;
            Camera.main.GetComponent<SmoothFollow>().playerbody = GameObject.Find("player").transform;

        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;

        }

        //스킬관련------------
        animator = playerBody.GetComponent<Animator>();
        nowLayer = "NoWeapon";
        PlayerMain.canMove = true;

        StartCoroutine(GameStartWait(3f));
        equip = false;  // 장비 착용 중이지 않다
        fireDelay = 0;   //쿨타임
       
   
        magic = 0;
        //---------------스킬관련
        //transform.position = gameManager.SpawnPos();
        animator = playerBody.GetComponent<Animator>();
        rigid = playerBody.GetComponent<Rigidbody>();

        weaponL.SetActive(false); 
        weaponR.SetActive(false);

        fillAmount = UI.GetComponent<FillAmount>();
    }

    IEnumerator GameStartWait(float second)
    {
        yield return new WaitForSeconds(second);
        //playerBody.gameObject.SetActive(true);
        getWeaponData();//무기 정보 가져오기
    }

    void FixedUpdate()
    {

        if (pv.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        CameraRotate();
        CameraMove();
        CameraZoom();

        UsePotion();

        PlayerMove();
        PlayerJump();
        PlayerDodge();
        PlayerAttack();

        equipmentWeapon(); // 무기 장착하기
        skillAttack();

        if (rigid.velocity.y < 0) { GroundCheck(); }
    }

    ///-----------------------------스킬관련 코드---------------------------------- <summary>
    ///

    void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Z) && potionDrinking == false)
        {
            Debug.Log("z 실행");
            if (cameraArm.GetComponent<ActionController>().thepotionInventory.slots[0].itemCount != 0
                && playerBody.GetComponent<PlayerMain>().hp != playerBody.GetComponent<PlayerMain>().hpmax)
            {
                potionDrinking = true;
                if (thepotionInventory.slots[0].item.itemName == "Potion")
                {
                    potionHp = 30;
                }
                else if (thepotionInventory.slots[0].item.itemName == "Heart")
                {
                    potionHp = 50;
                }
                playerBody.GetComponent<PlayerMain>().HpUp(potionHp, 0);
                UIController.instance.HpUp(potionHp);

            }
        }
        if (Input.GetKeyDown(KeyCode.C) && potionDrinking == false)
        {
            Debug.Log("c 실행");
            if (cameraArm.GetComponent<ActionController>().thepotionInventory.slots[1].itemCount != 0
                && playerBody.GetComponent<PlayerMain>().hp != playerBody.GetComponent<PlayerMain>().hpmax)
            {
                potionDrinking = true;
                if (thepotionInventory.slots[1].item.itemName == "Potion")
                {
                    potionHp = 30;
                }
                else if (thepotionInventory.slots[1].item.itemName == "Heart")
                {
                    potionHp = 50;
                }
                playerBody.GetComponent<PlayerMain>().HpUp(potionHp, 1);
                UIController.instance.HpUp(potionHp);

            }
        }
    }
    public void ChangeWeapon(string nowWeapon, string newWeapon)
    {
        if (!PlayerMain.canMove || usingSkill) return;

       weaponL.gameObject.SetActive(false);
        weaponR.gameObject.SetActive(false);

        if (equip)//이미 장착하고 있다면 
        {
            playerBody.Find(url + nowWeapon)
                .gameObject.SetActive(false); //원래 무기 장착 해제
            Debug.Log("무기해제했다.");
        }
        fillAmount.enabled = false;     
        fillAmount.enabled = true;

        playerBody.Find(url + newWeapon).gameObject.SetActive(true);//찾아서 트루로
        w = playerBody.Find(url + newWeapon).GetComponent<Weapon>(); //현재 쓰고 있는 무기로의 스크립트로 가져오기
        playerBody.Find("PlayerModel/root/pelvis/spine_01/spine_02/spine_03" +
        "/clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/107").gameObject.SetActive(false);//왼손에 낀거 장착해제
        equip = true; //장착했다

        fillAmount.skillState = false;
        fillAmount.cooltime = w.skillCooltime;
        fillAmount.objCoolTime.SetActive(true);
        fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
        fillAmount.CallSkillCooltime();

       
        if (w.equipType == 1) //대검이면
        {
            changeLayer = "THS";
        }
        else if (w.equipType == 2) // 한손검이면
        {
            changeLayer = "SingleSword";
        }
        else if (w.equipType == 3) // 마법 지팡이면
        {
            changeLayer = "MagicWand";
        }
        else if (w.equipType == 4) // 쌍검이면
        {
            changeLayer = "DoubleSword";
            playerBody.Find("PlayerModel/root/pelvis/spine_01/spine_02/spine_03" +
          "/clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/107").gameObject.SetActive(true);
        }



        animator.SetLayerWeight(animator.GetLayerIndex(nowLayer),0f);
        
        nowLayer = changeLayer;
        animator.SetLayerWeight(animator.GetLayerIndex(nowLayer), 1f);
        /*
             // "RuntimeAnimatorController" 파일 로드
         RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(controllerPath);

         // 로드된 "RuntimeAnimatorController"를 Animator 컴포넌트에 할당
         animator.runtimeAnimatorController = controller;
        */
         animator = playerBody.GetComponent<Animator>();

        photonView.RPC(nameof(WhatWeaponEquiped),RpcTarget.Others,
            w.equipIdx, PhotonNetwork.NickName);
    }
    [PunRPC]
    private void WhatWeaponEquiped(int weaponName, string senderName)
    {
        if (pv.Owner.NickName != senderName)
        { 
            return;
        }
        
        //무기 모델이 있다면 제거
        if (equip==true)
        {
            playerBody.Find(url + nowWeapon)
               .gameObject.SetActive(false); //원래 무기 장착 해제
        }
        Debug.Log("------------"+url);
        Debug.Log("------------"+weaponName);
        playerBody.Find(url + weaponName).gameObject.SetActive(true);//찾아서 트루로


    }
        

    public void getWeaponData()
    {
        weaponData = GameObject.Find("WeaponManager").GetComponent<WeaponData>();  //게임매니저에 넣어논 무기 데이터 가져오기
        Debug.Log(weaponData.name);
        foreach (Transform t in playerBody.Find(url)) // 해당 경로 안에있는 게임오브젝트 하나씩 가져옴
        {
            w = t.GetComponent<Weapon>();  //가져온 오브젝트의 Weapon스크립트 접근
            for (int i = 0; i < weaponData.weaponTable.Length; i++)
            { //무기정보 가져와서 내 캐릭터에 들어있는 무기에 데이터 집어 넣기
                if (t.name == weaponData.weaponTable[i].equipItemIdx.ToString())
                {
                    w.getWeaponDatabase(t, weaponData.weaponTable[i]);
                    break;
                }
            }
        }
    }


    void equipmentWeapon()
    {
        if(Input.GetKey(KeyCode.Alpha1)|| Input.GetKey(KeyCode.Alpha2)|| Input.GetKey(KeyCode.Alpha3)|| Input.GetKey(KeyCode.Alpha4)|| Input.GetKey(KeyCode.Alpha5)) 
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                slotNumber = 0;
            }
        
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                slotNumber = 1;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                slotNumber = 2;
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                slotNumber = 3;
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                slotNumber = 4;
            }
            equipIdx = cameraArm.GetComponent<ActionController>().theInventory.GetComponent<Inventory>().slots[slotNumber].item.name;
            cameraArm.GetComponent<ActionController>().theInventory.GetComponent<Inventory>().WeaponActive(slotNumber);
            ChangeWeapon(nowWeapon,equipIdx);
            nowWeapon = equipIdx;
            
        }


    }

    public void skillAttack()
    {

        if (equip == false) //무기가 있을때만 실행되도록 장비체크
            return;


        // fireDelay += Time.deltaTime; //공격딜레이에 시간을 더해줌
        // Boolean isFireReady = w.skillCooltime < fireDelay; //스킬 쿨타임 여부 확인

        if (Input.GetMouseButton(1) && w.equipType == 3 && fillAmount.skillState) //스킬사용
        {
            if (magic == 0)
            {
                animator.ResetTrigger("Idle");
                fillAmount.skillState = false;
               
                animator.SetTrigger("SkillA");
                magic = 1;
                fillAmount.imgSkill.color = new Color(0f, 1f, 1f, 1f);
                w.UseMagic(); //조건 충족시 Use 실행
                PlayerMain.canMove = false;
                usingSkill = true;
  
            }

        }
        else if (Input.GetMouseButtonUp(1) && w.equipType == 3 && magic == 1)
        {
            magic = 0;
            w.meleeArea.enabled = false;

          //  w.particleSystem.Stop();
            animator.SetTrigger("Idle");


            fillAmount.skillState = false;
            fillAmount.objCoolTime.SetActive(true);
            fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
            fillAmount.CallSkillCooltime();
            PlayerMain.canMove = true;
            usingSkill = false;
            if (pv.IsMine)
                PhotonNetwork.Destroy(GameObject.Find("Fire(Clone)").gameObject);
        }
        /*
        else if (Input.GetMouseButton(1) && w.equipType == 1 && isFireReady)
        {
            if (THS == 0) { 
                

            animator.SetBool("isMove", false);
            animator.SetBool("isJump", false);
                animator.SetTrigger("Charging");
                w.THSCharging();
                
            }
            THS = 1;
            
           // w.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            fillAmount.imgSkill.color = new Color(0f, 1f, 1f, 1f);
            canMove = false;
        }
        기모으는 코드
         */
        else if (Input.GetMouseButton(1) && w.equipType == 1 && fillAmount.skillState) //스킬사용
        {
            animator.ResetTrigger("Idle");
            if (thsSkill == false)
            {
                fillAmount.skillState = false;
                animator.SetTrigger("SkillA");
                thsSkill = true;
                fillAmount.imgSkill.color = new Color(0f, 1f, 1f, 1f);
                w.UseTHSSkill(); //조건 충족시 Use 실행
                usingSkill = true;
            }
        }
        else if (Input.GetMouseButtonUp(1) && w.equipType == 1 && thsSkill == true)
        {
            thsSkill = false;
            w.meleeArea.enabled = false;

         //   w.particleSystem.Stop();
            animator.SetTrigger("Idle");

            fillAmount.skillState = false;
            fillAmount.objCoolTime.SetActive(true);
            fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
            fillAmount.CallSkillCooltime();
            usingSkill = false;
            if(pv.IsMine)
                PhotonNetwork.Destroy(GameObject.Find("THSSkill(Clone)").gameObject);
        }


        else if (Input.GetMouseButton(1) && w.equipType == 2 && fillAmount.skillState)
        {
            animator.ResetTrigger("Idle");
            if (ssSkill == false)
            {
                
                fillAmount.skillState = false;
                animator.SetTrigger("Charging");
             //   w.particleSystem.Play();
                ssSkill = true;
                fillAmount.imgSkill.color = new Color(0f, 1f, 1f, 1f);
                w.UseSSSkill(); //조건 충족시 Use 실행
                usingSkill = true;
            }
        }

        else if (Input.GetMouseButtonUp(1) && w.equipType == 2 && ssSkill)
        {
            ssSkill = false;
            w.meleeArea.enabled = false;

            //w.particleSystem.Stop();
            animator.SetTrigger("Idle");

            fillAmount.skillState = false;
            fillAmount.objCoolTime.SetActive(true);
            fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
            fillAmount.CallSkillCooltime();
            usingSkill = false;
            if (pv.IsMine)
                PhotonNetwork.Destroy(GameObject.Find("candy(Clone)").gameObject);


        }
        else if (Input.GetMouseButtonDown(1) && w.equipType == 4 && fillAmount.skillState)
        {
            animator.ResetTrigger("Idle");
            w.meleeArea.enabled = true;

            //   animator.SetBool("isMove", false);
            //  animator.SetBool("isJump", false);
            animator.SetTrigger("SkillA");
            w.UseDSSkill();

            fillAmount.skillState = false;
            fillAmount.objCoolTime.SetActive(true);
            fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
            fillAmount.CallSkillCooltime();
        }
        /*
        else if (Input.GetMouseButtonDown(1)) //스킬사용
        {
            canMove = false;
            fillAmount.objCoolTime.SetActive(true);
            animator.SetTrigger("SkillA");
            w.UseSkillA(); //조건 충족시 Use 실행
            fireDelay = 0; //쿨타임 다시 재기

            fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
            fillAmount.CallSkillCooltime();
            
        }
       
        */
    }
    public void UseMagicStop()
    {
        magic = 0;
        w.meleeArea.enabled = false;

        //w.particleSystem.Stop();
        animator.SetTrigger("Idle");


        fillAmount.skillState = false;
        fillAmount.objCoolTime.SetActive(true);
        fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
        fillAmount.CallSkillCooltime();
        PlayerMain.canMove = true;
        usingSkill = false;
        if (pv.IsMine)
            PhotonNetwork.Destroy(GameObject.Find("Fire(Clone)").gameObject);
    }
    public void SSSkillStop()
    {
        ssSkill = false;
        w.meleeArea.enabled = false;
        
      //  w.particleSystem.Clear();
       // w.particleSystem.Stop();

        animator.SetTrigger("Idle");

        fillAmount.skillState = false;
        fillAmount.objCoolTime.SetActive(true);
        fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
        fillAmount.CallSkillCooltime();
        
        PlayerMain.canMove = true;
        usingSkill = false;
        if (pv.IsMine)
            PhotonNetwork.Destroy(GameObject.Find("candy(Clone)").gameObject);
    }
    public void THSSkillStop()
    {
        thsSkill = false;
        w.meleeArea.enabled = false;

     //   w.particleSystem.Clear();           
     //   w.particleSystem.Stop();
        
        animator.SetTrigger("Idle");

        fillAmount.skillState = false;
        fillAmount.objCoolTime.SetActive(true);
        fillAmount.imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
        fillAmount.CallSkillCooltime();
        PlayerMain.canMove = true;
        usingSkill = false;
        if (pv.IsMine)
            PhotonNetwork.Destroy(GameObject.Find("THSSkill(Clone)").gameObject);
    }
    /// -----------------------------스킬관련 코드----------------------------------
    /// </summary>



    // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // 마우스 움직임에 따라 cameraArm이 회전하게 하는 함수
    private void CameraRotate()
	{
        // 마우스 X, Y축 움직임 변화량 = mouseDelta
        // 카메라 rotation 값을 오일러 각으로 바꿈
		Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * cameraMoveSpeed, Input.GetAxis("Mouse Y") * cameraMoveSpeed);
        Vector3 cameraAngle = cameraArm.rotation.eulerAngles;

        // 카메라 각도 위쪽 40도, 아래쪽 25도 제한 
        float x = cameraAngle.x - mouseDelta.y;

        if (x < 180f) { x = Mathf.Clamp(x, -1.0f, 40.0f); }
		else { x = Mathf.Clamp(x, 335f, 361f); }

        // cameraArm의 rotation값 지정
		cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
	}

    // cameraArm이 플레이어를 추적하게 하는 함수
    private void CameraMove() 
    {
        Vector3 cameraArmPos = player.transform.position;
        cameraArm.position = cameraArmPos;
    }

    // 스크롤하면 캐릭터 중심 확대/축소하게 하는 함수
    // playerCamera의 Z position 값이 변함 (cameraArm이 변하지 않는다)

    // [!!!!!!!수정필요!!!!!!] 확대/축소 제한 필요함 - 카메라 벡터 제한하기
    private void CameraZoom()
	{
        if (playerCamera.transform.localPosition.z > -8f && playerCamera.transform.localPosition.z < 2f)
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollWheel != 0)
            {
                if (playerCamera.transform.localPosition.z <= -4.9f) { scrollWheel = 0.1f; }
                if (playerCamera.transform.localPosition.z >= -1.1f) { scrollWheel = -0.1f; }
                Debug.Log("scrollWheel : " + scrollWheel);

                Vector3 cameraDir = playerCamera.rotation * Vector3.forward;
                playerCamera.transform.position += cameraDir * Time.deltaTime * scrollWheel * cameraScrollSpeed;
            }
        }
    }

    // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    /// //////////////////////////////////////////////////////////////////////////////////////// 


    private void PlayerMove()
    {
        // 플레이어가 사망하지 않은 경우
        if (!PlayerMain.isDead)
        {
            // 감속 및 공격 중 이동 중지
            if (!PlayerMain.isMove && PlayerMain.onGround) { rigid.velocity = new Vector3(0, 0, 0); }
            if (animator.GetBool("isAttack")) { rigid.velocity = new Vector3(0, 0, 0); return; }
            // WASD 입력값을 벡터로 변환, 0이 아닌 경우 isMove = true ---> 애니메이터 isMove 패러미터도 true가 됨
            // 1프레임마다 Update 되므로 입력값이 없어지면 애니메이터 패러미터도 갱신됨
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            PlayerMain.isMove = moveInput.magnitude != 0;
            animator.SetBool("isMove", PlayerMain.isMove);
            // Dodge(회피)는 별도의 이동값을 가지므로 적용X
            // moveDir을 실시간 입력값 + 카메라가 보는 방향에 따라 갱신 후 이동
            if (PlayerMain.isMove && !PlayerMain.isDodge)
            {
                Vector3 lookFront = new Vector3(cameraArm.forward.x, 0.0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0.0f, cameraArm.right.z).normalized;
                moveDir = (lookFront * moveInput.y) + (lookRight * moveInput.x);

                playerBody.forward = moveDir;
                rigid.velocity = new Vector3((moveDir * playerSpeed).x, rigid.velocity.y, (moveDir * playerSpeed).z);
                //transform.position += moveDir * Time.deltaTime * playerSpeed;
            }
        }
        MiniMapP.transform.position = new Vector3(playerBody.position.x, 0, playerBody.position.z);
    }

    //[삭제금지] GroundCheck 디버그용 Gizmos
    //private void OnDrawGizmos()
    //{
    //	feetpos = new Vector3(playerBody.position.x, playerBody.position.y + 0.2f, playerBody.position.z);

    //	Gizmos.color = Color.blue;
    //  Gizmos.DrawCube(feetpos, boxSize);
    //}

    private void GroundCheck()
    {
        PlayerMain.onGround = false;
        feetpos = new Vector3(playerBody.position.x, playerBody.position.y + 0.2f, playerBody.position.z);

        //RaycastHit ratHit = Physics.BoxCast(feetpos, boxSize / 2, Vector3.down, out ratHit, Quaternion.identity, 1.0f, LayerMask.GetMask("Ground"))
        RaycastHit ratHit;
        Debug.Log("Ground Check");

        if (Physics.BoxCast(feetpos, boxSize / 2, Vector3.down, out ratHit, Quaternion.identity, 0.6f, LayerMask.GetMask("Ground")))
        {
            if (ratHit.distance < 0.5f)
            {
                PlayerMain.isJump = false;
                PlayerMain.onGround = true;
                animator.SetBool("isJump", false);
            }
        }
    }

    private void PlayerJump()
    {
        // 점프 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 애니메이터 활성화 및 플레이어 상태 변수 체크
            animator.SetBool("isJump", true);
            if (!PlayerMain.isJump)
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                PlayerMain.isJump = true;
            }
        }
    }

    // 스태미나 소모식/스킬식 뭘로하지?
    private void PlayerDodge()
    {
        // 구르기 회피(임시 회피키 LShift)
        // 점프/공격/피격경직 중에는 불가능하게
        if (PlayerMain.isJump && PlayerMain.isDamaged) { return; }

        if (PlayerMain.isMove && Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 회피 중에 방향이 바뀌지 않도록 fix
            DodgeDir = moveDir;
            animator.SetTrigger("isDodge");

            // [!!!!!!!수정필요!!!!!!]playerSpeed는 디버프 때문에 감소할 수 있지만 회피는 영향 안받게 하자
            if (!PlayerMain.isDodge)
            {

                PlayerMain.isDodge = true;
                player.layer = 6; // 슈퍼아머 레이어

                playerSpeed /= 2f;
                transform.position += DodgeDir * Time.deltaTime * playerSpeed;

                // 선택지 1번
                // 이거 건드리면 무적탐
                //Invoke("DodgeEnd", 0.4f);
                StartCoroutine(DodgeEnd(1f));
            }

            // 선택지 2번 123
            // 얘를 DodgeEnd 안에 넣느냐 마느냐...
            //PlayerMain.isDodge = false;
        }
    }

    IEnumerator DodgeEnd(float second)
    {
        PlayerMain.isDodge = false;
        yield return new WaitForSeconds(second);
        playerSpeed *= 2f;
        player.layer = 7; // 플레이어 레이어
    }

    private IEnumerator GetButtonStop(float second)
    {
        PlayerMain.isAttack = true;
        PlayerMain.canClick = false;

        yield return new WaitForSeconds(second);

        PlayerMain.isAttack = false;
        PlayerMain.canClick = true;
    }

    private IEnumerator ComboStop(float second)
    {
        float currentTime = 0f;
        while (true)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= second) { break; }
            yield return null;
        }

        PlayerMain.combo = 0;
        animator.SetInteger("combo", 0);

        animator.SetBool("isAttack", false);
    }

    private void PlayerAttack()
    {
        if (PlayerMain.canClick && Input.GetMouseButtonDown(0)&&!PlayerMain.isJump)
        {
            PlayerMain.isMove = false;
            animator.SetBool("isMove", false);

            if (!PlayerMain.isAttack && PlayerMain.combo < 3)
            {
                StartCoroutine("GetButtonStop", 0.3f);

                PlayerMain.combo++;
                animator.SetBool("isAttack", true);
                if (PlayerMain.combo == 3) { PlayerMain.combo = 1; }
                animator.SetInteger("combo", PlayerMain.combo);

                if (attackCoroutine != null)
                {

                    StopCoroutine(attackCoroutine);
                }
                attackCoroutine = StartCoroutine(ComboStop(0.6f));
            }
        }
    }

    // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void PlayerDead() 
    {
        player.SetActive(false);
        cookie.SetActive(true);

        animator.SetBool("isDead", true);

        // 패배시 표시되는 페이지 - 박해준
        UI.GetComponent<UIController>().DefeatPage();
        UI.GetComponent<UIController>().ArriveDown();
    }
}
