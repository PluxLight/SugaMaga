using MiniJSON;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * 
 * ----------게임매니저에 넣을거 -------------------------------------
 * 
 */

public class WeaponData : MonoBehaviour
{
    [System.Serializable]
    public class WeaponTable
    {
        public int equipItemIdx; // ���� �ڵ� �̸� �Է�
        public string equipName;
        public float equipDamage;
        // Start is called before the first frame update
        public float equipSpeed;
        public string skillName;
        public float skillDamage;
        public float skillCooltime;
        public int equipType;
    }

    // Start is called before the first frame update
    public WeaponTable[] weaponTable;

    void Awake()
    {
        StartCoroutine("GetWeaponTable");
    }

    IEnumerator GetWeaponTable()
    {
        var str = new Dictionary<string, object>();
        str.Add("equipmentItemIdx", 0);
        var data = Json.Serialize(str);
        ApiManager.Instance.GET("game/equip", data, PhotonNetwork.NickName,delegate (UnityWebRequest request)
        {
            weaponTable = JsonHelper.FromJson<WeaponTable>(request.downloadHandler.text);
        });
        yield return new WaitForSeconds(1f);
        //player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}
