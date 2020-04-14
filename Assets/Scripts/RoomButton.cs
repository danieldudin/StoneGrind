﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText = default;
    [SerializeField]
    private Text sizeText = default;

    private string roomName;
    private int roomSize;
    private int playerCount;

    public void SetRoom(string nameInput, int sizeInput, int countInput) {
        roomName = nameInput;
        roomSize = sizeInput;
        playerCount = countInput;

        nameText.text = nameInput;
        sizeText.text = countInput + "/" + sizeInput;
    }

    public void JoinRoomOnClick() {
        PhotonNetwork.JoinRoom(roomName);
    }
}