using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;

public class ApiManager : MonoBehaviour
{
    // If Game Start or Login, Token Update
    // example is expired token
    private string ACCESS_TOKEN = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFlOTczZWUwZTE2ZjdlZWY0ZjkyMWQ1MGRjNjFkNzBiMmVmZWZjMTkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vdGF5b3ZlcnNlLTQ5YWYxIiwiYXVkIjoidGF5b3ZlcnNlLTQ5YWYxIiwiYXV0aF90aW1lIjoxNjc5MjczNDQwLCJ1c2VyX2lkIjoiMU1ObEcwUUF2YVhBRmZZZ2Q0cDlPeWpwR0pxMiIsInN1YiI6IjFNTmxHMFFBdmFYQUZmWWdkNHA5T3lqcEdKcTIiLCJpYXQiOjE2NzkyNzM0NDAsImV4cCI6MTY3OTI3NzA0MCwiZW1haWwiOiJkZW1vMDFAZW1haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJmaXJlYmFzZSI6eyJpZGVudGl0aWVzIjp7ImVtYWlsIjpbImRlbW8wMUBlbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.A2IxXlmilbAIAjhma_FaaLdtznnPod11VlXGRa07w9IOd0iyRSzl0nRRS-UcMHZpcCKOFMVxe6lzGH2gfzT4mwC1tIIRdEfSOl9b4Nq8ZPgPPTrrx8ub4HJPPOu27QtMd-HcMImpteuK4Q0HlosSVho4PG_fzP3PU6E47JO1vZagKfN9cMS5MAYSe2GYGdIxChkz6j0Hm51V27_FK70t-d1VUOG3DfmVtsZoHd0JQYo5ra3LifCau3Do08p--oIFDS45jfesSLrCbdjobbMTtGen7WiVWZzPJyuhegffHZm7Tzj2fhE_-n6Uf8iyZHgVxsqPxCxNzf5snTpsVugOKw";
    private string REFRESH_TOKEN = "APJWN8epyB1N-XKKkiqV3B0HimZNzfbDEfkI8FldGXv14Y0JTw-pPlwKfScPkKm_zbq3ugjt6o-in27EQ4_wv27jG8h1hC6A_LaiOXbDbYhDCIIM3kak1vLqFtyJlWpaSdQMVwapQQ7b7d5fTwp6wnjnpyNuxMedwvIUyOglLgaWZlhRdZzNCgKTs_a3eN9lMpRkg2ttFm-B1mq575uf3M2Rh6EvkmTUWw";
    private string GOOGLE_KEY = "AIzaSyAbrcuxBD_VyzQDSlAJfxPUZOfsx8wfNy0";
    private string SERVER_URL = "http://localhost:18081/api/";

    public static ApiManager instance;

    private void Awake()
    {
        instance = this;
    }

    public string CallApi(string url, string method, string json, Action<string> callback)
    {
        switch (method)
        {
            case "GET":
                StartCoroutine( GetStart(url, json, (request) =>
                {
                    if (request.responseCode == 200)
                    {
                        string jsonString = request.downloadHandler.text;
                        Debug.Log("jsonString : " + jsonString);
                        callback(jsonString);
                    }
                    else
                    {
                        callback(null);
                    }
                }
                ));
                break;
            case "POST":
                break;
            case "PUT":
                 break;
            case "DELETE":
                break;
        }

        return null;
    }

    public IEnumerator GetStart(string url, string json, Action<UnityWebRequest> callback)
    {
        /*if (json != null)
        {
            json = json.Replace("{", "");
            json = json.Replace("}", "");
            json = json.Replace("\"", "");
            string[] jsonParse = json.Split(',');
            string temp = "?";
            
            foreach (string i in jsonParse)
            {
                temp += i.Split(":")[0] + "=" + i.Split(":")[1] + "&";
            }
            temp = temp.Substring(0, temp.Length - 1);

            url += temp;
        }*/

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

        Debug.Log("url : " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + url))
        {
            request.SetRequestHeader("accessToken", ACCESS_TOKEN);
            yield return request.SendWebRequest();

            // Debug.Log(request.result);
            // Debug.Log(request.downloadHandler.text);

            if ("EXPIRED_ID_TOKEN".Equals(request.downloadHandler.text))
            {
                // Debug.Log("run refresh");
                StartCoroutine( RefreshToken() );
            }

            Debug.Log("Server responded: " + request.downloadHandler.text);
            callback(request);
        }
    }

    public IEnumerator RefreshToken()
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "refresh_token");
        form.AddField("refresh_token", REFRESH_TOKEN);
        string refreshUrl = "https://securetoken.googleapis.com/v1/token?key=" + GOOGLE_KEY;

        using (UnityWebRequest request = UnityWebRequest.Post(refreshUrl, form))
        {
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return request.SendWebRequest();

            /*Debug.Log("request : " + request);
            Debug.Log("request.result : " + request.result);
            Debug.Log("request.downloadHandler : " + request.downloadHandler.text);
            Debug.Log("accesstoken : " + request.downloadHandler);*/

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error : " + request.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

}
