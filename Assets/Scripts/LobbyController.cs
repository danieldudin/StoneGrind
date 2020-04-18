using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks 
{
    private string roomName;
    private int roomSize;

    private List<RoomInfo> roomListings;
    [SerializeField]
    private Transform roomsContainer = default;
    [SerializeField]
    private GameObject roomsListingPrefab = default;

    private void Start()
    {
        roomListings = new List<RoomInfo>();
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

    public override void OnJoinedRoom()
    {
        Debug.Log("Room joined");
        PhotonNetwork.LoadLevel(3);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Room left, going back to character select");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobby left, going back to character select");
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveLobby() {
        PhotonNetwork.LeaveLobby();
    }
}
