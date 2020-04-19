using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviourPunCallbacks
{

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        // Connected and ready to dive into the Character Screen
        PhotonNetwork.LoadLevel(1);
    }
}
