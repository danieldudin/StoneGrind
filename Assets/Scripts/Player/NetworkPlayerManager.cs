using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;

using TMPro;

public class NetworkPlayerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The DarkRift client to communicate on.")]
    UnityClient client = default;

    public Dictionary<ushort, PlayerObject> networkPlayers = new Dictionary<ushort, PlayerObject>();
    public GameObject networkInterface;
    public Transform playerPanel;
    public Transform playerPanelPlayerID;

    public void Awake()
    {
        client.MessageReceived += MessageReceived;
    }

    void Start() {
        networkInterface = Instantiate(Resources.Load<GameObject>("UI/NetworkInterface/NetworkInterface"));
        playerPanel = networkInterface.GetComponent<NetworkInterface>().playerPanel;
    }

    void MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage() as Message)
        {
            if (message.Tag == Tags.MovePlayerTag)
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    ushort id = reader.ReadUInt16();
                    Vector3 newPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                    if (networkPlayers.ContainsKey(id))
                        networkPlayers[id].SetMovePosition(newPosition);
                }
            }
        }
    }

    public void Add(ushort id, PlayerObject player)
    {
        networkPlayers.Add(id, player);
    }

    public void CreatePlayerPanel(int id) {
        GameObject newPlayerPanel = Instantiate(Resources.Load<GameObject>("UI/NetworkInterface/PlayerPanel"));
        newPlayerPanel.transform.SetParent(playerPanel);

        GameObject playerPanelPlayerID = Instantiate(Resources.Load<GameObject>("UI/NetworkInterface/PlayerID"));
        playerPanelPlayerID.transform.SetParent(newPlayerPanel.transform);
        playerPanelPlayerID.GetComponent<TextMeshProUGUI>().text = id.ToString();
    }

    public void DestroyPlayer(ushort id)
    {
        PlayerObject o = networkPlayers[id];

        Destroy(o.gameObject);

        networkPlayers.Remove(id);
    }
}
