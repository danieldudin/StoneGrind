using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GS;

    public Transform[] spawnPoints;

    private void OnEnable() {
        if (GameManager.GS == null)
        {
            GameManager.GS = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer() {
        Debug.Log("Creating Player");
        int spawnPicker = Random.Range(0, GameManager.GS.spawnPoints.Length);

        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Warrior_NoWeapon"), GameManager.GS.spawnPoints[spawnPicker].position, GameManager.GS.spawnPoints[spawnPicker].rotation, 0);
    }

    public void DisconnectPlayer() 
    {
        StartCoroutine(DisconnectAndLoad());
    }

    public void LeaveRoom() {
        StartCoroutine(LeaveRoomAndLoad());
    }

    IEnumerator DisconnectAndLoad() 
    {
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected) 
        {
            yield return null;
        }

        SceneManager.LoadScene(0);
    }

    IEnumerator LeaveRoomAndLoad()
    {
        PhotonNetwork.LeaveRoom();

        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }

        PhotonNetwork.LoadLevel(1);
    }
}
