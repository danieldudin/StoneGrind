using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    [SerializeField]
    private int multiPlayerSceneIndex;

    [SerializeField]
    private GameObject lobbyPanel;

    [SerializeField]
    private Text roomNameDisplay;
}
