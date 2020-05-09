using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;

public class NetworkPlayerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The DarkRift client to communicate on.")]
    UnityClient client;

    public Dictionary<ushort, AgarObject> networkPlayers = new Dictionary<ushort, AgarObject>();

    public void Awake()
    {
        client.MessageReceived += MessageReceived;
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
            else if (message.Tag == Tags.SetRadiusTag)
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    ushort id = reader.ReadUInt16();

                    if (networkPlayers.ContainsKey(id))
                        networkPlayers[id].SetRadius(reader.ReadSingle());
                }
            }
        }
    }

    public void Add(ushort id, AgarObject player)
    {
        networkPlayers.Add(id, player);
    }

    public void DestroyPlayer(ushort id)
    {
        AgarObject o = networkPlayers[id];

        Destroy(o.gameObject);

        networkPlayers.Remove(id);
    }
}
