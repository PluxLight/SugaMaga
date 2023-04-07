using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HpEdit : MonoBehaviourPunCallbacks
{
    public GameObject player;
    PlayerMain playerMain;

    
    private void Start()
    {
        playerMain = player.GetComponent<PlayerMain>();
    }

    [PunRPC]
    public void HpUp()
    {
       // playerMain.hp += 10;
        UIController.instance.HpUp(10);
    }

    [PunRPC]
    public void HpDown()
    {
        playerMain.HpDown(10);
        UIController.instance.HpDown();
    }
}
