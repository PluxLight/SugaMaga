using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static string UID = "";

    void Awake()
    {
        instance = this;
    }

    public void setUID(string uid)
    {
        UID = uid;
    }

    public string getUID() 
    {
        return UID;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
