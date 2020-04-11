using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject lobbyConnectButton;
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private TMP_InputField playerNameInput;

    private string roomName;
    private int roomSize;

    private List<RoomInfo> roomListings;
    [SerializeField]
    private Transform roomsContainer;
    [SerializeField]
    private GameObject roomsListingPrefab;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        lobbyConnectButton.SetActive(true);
        roomListings = new List<RoomInfo>();

        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
                
        }
        else {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        }
        playerNameInput.text = PhotonNetwork.NickName;
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel(1);
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public void JoinLobbyOnClick() {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;

        foreach (RoomInfo room in roomList)
        {
            if (roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else {
                tempIndex = -1;
            }

            if (tempIndex != -1) {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsContainer.GetChild(tempIndex).gameObject);
            }

            if (room.PlayerCount > 0) {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name) {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomsListingPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();

            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameInput)
    {
        roomName = nameInput;
    }

    public void OnRoomSizeChanged(string sizeInput)
    {
        roomSize = int.Parse(sizeInput);
    }

    public void CreateRoom()
    {
        Debug.Log("Creating new room with name: " + roomName);

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public void LeaveLobby() {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);

        PhotonNetwork.LeaveLobby();
    }
}
