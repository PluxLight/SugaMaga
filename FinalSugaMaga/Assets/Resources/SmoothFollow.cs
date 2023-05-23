using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField]
    public Transform cameraArm;
    [SerializeField]
    public Transform playerCamera;
    [SerializeField]
    public Transform playerbody;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!cameraArm || !playerCamera || !playerbody) return;


        transform.position = playerCamera.position;
        transform.rotation = playerCamera.rotation;


    }

}
