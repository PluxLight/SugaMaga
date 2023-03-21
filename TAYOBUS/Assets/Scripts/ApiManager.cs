using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using System.Threading.Tasks;

public class ApiManager : MonoBehaviour
{
    // If Game Start or Login, Token Update
    // example is expired token
    private string ACCESS_TOKEN = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFlOTczZWUwZTE2ZjdlZWY0ZjkyMWQ1MGRjNjFkNzBiMmVmZWZjMTkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vdGF5b3ZlcnNlLTQ5YWYxIiwiYXVkIjoidGF5b3ZlcnNlLTQ5YWYxIiwiYXV0aF90aW1lIjoxNjc5MjczNDQwLCJ1c2VyX2lkIjoiMU1ObEcwUUF2YVhBRmZZZ2Q0cDlPeWpwR0pxMiIsInN1YiI6IjFNTmxHMFFBdmFYQUZmWWdkNHA5T3lqcEdKcTIiLCJpYXQiOjE2Nzk0MTQyNDIsImV4cCI6MTY3OTQxNzg0MiwiZW1haWwiOiJkZW1vMDFAZW1haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJmaXJlYmFzZSI6eyJpZGVudGl0aWVzIjp7ImVtYWlsIjpbImRlbW8wMUBlbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.ExdyP7Gm0v6a2tTep16QLGOIt7_sNDkxQLuG6PBazbnid3xJRcESp355B08TlFj0BcJKbiPp_vNieL1o802aN2ZobwrMR1ojQ5dcHq6_lwMA6A9dwuHt-xw91gKVF0kK-wGK6JlQuBC3uJRSx9c7AurAm8NSjfzdmNoEqaA0DU8dKa9_r6l74a2e1TQqHVaF1UMGT4g6QVjm3DHKa_fMOAcR2wL7w__2M1zeWX7QsW7sZ2ZAlA_ETMZ2apfcqqM4vZlW4Nd82pUlC9Wak2zauroLL9rZ8_pCWlIfxIbPqv2fQ5dmipzoKHDbkbui89xZFkLS2LUPkYg_jYpq2memlA";
    private string REFRESH_TOKEN = "APJWN8epyB1N-XKKkiqV3B0HimZNzfbDEfkI8FldGXv14Y0JTw-pPlwKfScPkKm_zbq3ugjt6o-in27EQ4_wv27jG8h1hC6A_LaiOXbDbYhDCIIM3kak1vLqFtyJlWpaSdQMVwapQQ7b7d5fTwp6wnjnpyNuxMedwvIUyOglLgaWZlhRdZzNCgKTs_a3eN9lMpRkg2ttFm-B1mq575uf3M2Rh6EvkmTUWw";
    private string GOOGLE_KEY = "AIzaSyAbrcuxBD_VyzQDSlAJfxPUZOfsx8wfNy0";
    private string SERVER_URL = "http://localhost:18081/api/";
    
    static ApiManager instance;
    static GameObject container;
    static GameObject Container {
        get { return container; }
    }


    public static ApiManager Instance
    {
        get
        {
            if ( !instance )
            {
                container = new GameObject();
                container.name = "ApiManager";
                instance = container.AddComponent( typeof(ApiManager) ) as ApiManager;
            }

            return instance;
        }
    }


    public void GET(string url, string json, Action<UnityWebRequest> callback)
    {
        if (json != null)
        {
            var dict = Json.Deserialize(json) as Dictionary<string, object>;
            List<string> keys = new List<string>(dict.Keys);

            string temp = "?";

            foreach (string key in keys)
            {
                temp += key + "=" + dict[key] + "&";
            }
            temp = temp.Substring(0, temp.Length - 1);

            url += temp;
        }

        if ( !Verify() )
        {
            callback(null);
        }

        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + url);
        request.SetRequestHeader("accessToken", ACCESS_TOKEN);

        StartCoroutine( WaitRequest(request, callback) );
    }

    public void POST(string url, string json, Action<UnityWebRequest> callback)
    {
        /*if (!Verify())
        {
            callback(null);
        }*/

        UnityWebRequest request = UnityWebRequest.Post(SERVER_URL + url, json);
        /*request.SetRequestHeader("accessToken", ACCESS_TOKEN);*/
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(WaitRequest(request, callback));
    }

    public void PUT(string url, string json, Action<UnityWebRequest> callback)
    {
        if (!Verify())
        {
            callback(null);
        }

        UnityWebRequest request = UnityWebRequest.Put(SERVER_URL + url, json);
        request.SetRequestHeader("accessToken", ACCESS_TOKEN);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(WaitRequest(request, callback));
    }


    public IEnumerator WaitRequest(UnityWebRequest request, Action<UnityWebRequest> callback)
    {
        yield return request.SendWebRequest();
        callback(request);
    }


    public bool Verify()
    {
        Debug.Log("Start Verify");

        bool verifyResult = true;
        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + "sign/verify");
        request.SetRequestHeader("accessToken", ACCESS_TOKEN);

        StartCoroutine( WaitRequest(request, delegate (UnityWebRequest result)
        {
            if (result.responseCode == 403)
            {
                Debug.Log("Refresh Forbidden");
                
                if (!RefreshToken())
                {
                    verifyResult = false;
                }
            }
            else if (request.responseCode == 400)
            {
                Debug.Log("Refresh Bad Request");
                verifyResult = false;
            }

        } ));

        Debug.Log("End Verify");

        return verifyResult;
    }


    public bool RefreshToken()
    {
        bool refreshResult = true;

        Debug.Log("Start RefreshToken");

        WWWForm form = new WWWForm();
        form.AddField("grant_type", "refresh_token");
        form.AddField("refresh_token", REFRESH_TOKEN);
        string refreshUrl = "https://securetoken.googleapis.com/v1/token?key=" + GOOGLE_KEY;

        UnityWebRequest request = UnityWebRequest.Post(refreshUrl, form);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        StartCoroutine(WaitRequest(request, delegate(UnityWebRequest res)
        {
            Debug.Log("res download text : " + res.downloadHandler.text);
        }));

        Debug.Log("End RefreshToken");

        return refreshResult;
    }

}