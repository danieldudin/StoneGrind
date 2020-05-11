using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetworkSpawn : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The DarkRift client to communicate on.")]
    UnityClient client = default;

    [SerializeField]
    [Tooltip("The controllable player prefab.")]
    GameObject controllablePrefab = default;

    [SerializeField]
    [Tooltip("The network controllable player prefab.")]
    GameObject networkPrefab = default;

    [SerializeField]
    [Tooltip("The network player manager.")]
    NetworkPlayerManager networkPlayerManager = default;
    void Awake()
    {
        if (client == null)
        {
            Debug.LogError("Client unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (controllablePrefab == null)
        {
            Debug.LogError("Controllable Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (networkPrefab == null)
        {
            Debug.LogError("Network Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }

        client.MessageReceived += Client_MessageReceived;
    }

    private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage() as Message)
        {
            if (message.Tag == Tags.SpawnPlayerTag)
                SpawnPlayer(sender, e);
            else if (message.Tag == Tags.DespawnPlayerTag)
                DespawnPlayer(sender, e);
        }
    }

    void SpawnPlayer(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage())
        using (DarkRiftReader reader = message.GetReader())
        {
            while (reader.Position < reader.Length)
            {
                ushort id = reader.ReadUInt16();
                Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                Debug.Log("Spawning client for ID = " + id + ".");

                GameObject obj;
                if (id == client.ID)
                {
                    obj = Instantiate(controllablePrefab, position, Quaternion.identity) as GameObject;

                    Player player = obj.GetComponent<Player>();
                    player.Client = client;

                    Camera.main.GetComponent<CameraController>().Target = obj.transform;
                }
                else
                {
                    obj = Instantiate(networkPrefab, position, Quaternion.identity) as GameObject;
                }

                PlayerObject agarObj = obj.GetComponent<PlayerObject>();

                networkPlayerManager.Add(id, agarObj);
            }
        }
    }

    void DespawnPlayer(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage())
            using (DarkRiftReader reader = message.GetReader())
                networkPlayerManager.DestroyPlayer(reader.ReadUInt16());
    }
}
