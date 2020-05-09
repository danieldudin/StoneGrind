using UnityEngine;

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

        // PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), GameManager.GS.spawnPoints[spawnPicker].position, GameManager.GS.spawnPoints[spawnPicker].rotation, 0);
    }
}
