using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

[System.Serializable]
public class User {
    public string username;
    public string password;
}

[System.Serializable]
public class UserCollection {
    public User[] users;
}


public class AuthManager : MonoBehaviour
{
    private readonly string pathToJson = "/Auth/auth.json";
    private UserCollection userCollection;
    public static AuthManager Instance {get; private set;}

    void Awake() {
        //Singleton pattern
        if(Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        GetAuthorizedUsers();
    }

    public bool AuthenticateAsTeacher(string username, string password) {
        if(userCollection != null) {
            foreach(User user in userCollection.users) {
                if(string.Compare(user.username, username) == 0 && string.Compare(user.password, password) == 0) {
                    Debug.Log("AuthManager: Authentication successful.");

                    //La presenza di questo oggetto mi dice che un insegnante sta accedendo, in questo modo se si era scollegato
                    //è possibile renderlo di nuovo master client.
                    //Il caso limite in cui nello stesso momento uno studente si stia collegando non si pone perchè
                    //questo oggetto non è condiviso in rete, dunque è visibile solo in locale dalla macchina del docente
                    GameObject authHelper = new GameObject("AuthHelper");
                    DontDestroyOnLoad(authHelper);

                    return true;
                }
            }
            Debug.Log("AuthManager: Authentication failed.");
            return false;
        }
        else {
            Debug.Log("AuthManager: User list is null.");
            return false;
        }
    }

    #region private methods

    private void GetAuthorizedUsers() {
        string filename = Application.streamingAssetsPath + pathToJson;
        string jsonString = File.ReadAllText(filename);
        userCollection = JsonUtility.FromJson<UserCollection>(jsonString);
    }


    #endregion
}
