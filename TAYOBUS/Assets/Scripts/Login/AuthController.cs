using UnityEngine;
using Firebase.Auth;
using System;

public class AuthController
{
    private static AuthController instance = null;

    private static bool SceneChange = false;

    public static AuthController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AuthController();
            }
            
            return instance;
        }
    }

    private FirebaseAuth auth;
    private FirebaseUser user;

    public string UserId => user.UserId;

    public Action<bool> LoginState;

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user) 
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user!= null ) 
            {
                Debug.Log("Logout");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed) 
            {
                Debug.Log("Login");
                LoginState?.Invoke(true);
            }
        }
    }

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("is calcelded");
                return;
            }
            if (task.IsFaulted)
            {
                //
                Debug.LogError("is faulted");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("signup success");
        });
    }

    public void Login(string email, string password)
    {
        Debug.Log(email);

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("is calcelded");
                return;
            }
            if (task.IsFaulted)
            {
                // 
                Debug.LogError("is faulted");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("signin success");
            ApiManager.Instance.InputUID(newUser.UserId);
            SceneChange = true;
            

        });
    }

}
