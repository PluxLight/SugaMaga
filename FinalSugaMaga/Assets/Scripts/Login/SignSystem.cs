using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SignSystem : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPpassword;

    public TMP_Text outputText;

    // Start is called before the first frame update
    void Start()
    {
        AuthController.Instance.Init();
        AuthController.Instance.LoginState += onChangeState;
    }

    private void onChangeState(bool sign)
    {
        outputText.text = sign ? "Login - " : "Logout - ";
        outputText.text += AuthController.Instance.UserId;

        if (sign)
        {
            SceneLoader.Instance.LoadScene("MainMenuScene");
            PhotonNetwork.NickName = GameManager.instance.getUID();
            
            /*SceneManager.LoadScene("MainMenuScene");*/
            music();
        }
    }
    public void music()
    {
        DontDestroyOnLoad(GameObject.Find("BackgroundMusic"));
    }

    public void Create()
    {
        string email = inputEmail.text;
        string password = inputPpassword.text;

        AuthController.Instance.Create(email, password);

        outputText.text = "sign up! - ";
        outputText.text += AuthController.Instance.UserId;
    }

    public void Login()
    {
        AuthController.Instance.Login(inputEmail.text, inputPpassword.text);
    }

}
