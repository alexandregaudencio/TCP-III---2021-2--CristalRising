using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            usernameInput.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        }
        else
        {
            usernameInput.text = "Player " + Random.Range(0, 1000).ToString("0000");
            OnUsernameInputValueChange();
        }
    }

    public void OnUsernameInputValueChange()
    {
        PhotonNetwork.NickName = usernameInput.text;
        Debug.Log(PhotonNetwork.NickName);
        PlayerPrefs.SetString("username", usernameInput.text);
    }
}

