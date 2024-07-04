using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{

    private string gameVersion = "0.1";

    private bool isTeacher;

    [SerializeField]
    public Button playAsStudentButton;
    public Button playAsTeacherButton;

    [SerializeField]
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;

    public GameObject pannelloStud;
    public GameObject pannelloTeach;

    float timer = 0f;
    float count = 2f;

    void Awake() {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        isTeacher = false;
    }

    void Start() {
        playAsTeacherButton.onClick.AddListener(ConnectAsTeacher);
        playAsStudentButton.onClick.AddListener(Connect);

    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > count)
        {
            pannelloStud.SetActive(false);
            pannelloTeach.SetActive(false);
        }
    }
    public void ConnectAsTeacher() {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        if(AuthManager.Instance.AuthenticateAsTeacher(username, password)) {
            isTeacher = true;
            Connect();
        }
        else
        {
            pannelloTeach.SetActive(true);
            pannelloStud.SetActive(false);
            timer = 0f;
        }
    }
    public void Connect() {
        if (usernameInputField.text != "") {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        } else {
            // pannelloStud.SetActive(true);
            usernameInputField.text = "DefaultUser";
            pannelloTeach.SetActive(false);
            timer = 0f;
        }
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Launcher: Connected to master server.");
        PhotonNetwork.NickName = usernameInputField.text;
        if(isTeacher){
            SceneManager.LoadScene(1);
        }
        else {
            SceneManager.LoadScene(2);
        }
    }
}
