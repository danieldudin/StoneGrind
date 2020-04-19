using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterScreenController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    [SerializeField]
    private TMP_InputField playerNameInput = default;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        }
        playerNameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Lobby joined");
        PhotonNetwork.LoadLevel(2);
    }

    public void JoinLobbyOnClick()
    {
        PhotonNetwork.JoinLobby();
    }
}
