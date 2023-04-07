using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Zone : MonoBehaviour
{
    public float Speed = 1f;
    private Vector3 myVector;

    public bool[] XYZ = new[] { true, false, true };

    void Start()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
        myVector = transform.localScale;
    }

    void Update()
    {
        if (XYZ[0] == true)
        {
            myVector.x -= Time.deltaTime * Speed;
            if (myVector.x <= 0)
            {
                myVector.x = 0;
            }
        }
        if (XYZ[1] == true)
        {

            myVector.y -= Time.deltaTime * Speed;
            if (myVector.y <= 0)
            {
                myVector.y = 0;
            }
        }
        if (XYZ[2] == true)
        {

            myVector.z -= Time.deltaTime * Speed;
            if (myVector.z <= 0)
            {
                myVector.z = 0;
            }
        }
        transform.localScale = myVector;
    }
}