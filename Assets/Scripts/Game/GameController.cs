using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameController : NetworkBehaviour
{
    [SerializeField] PlayerController player1, player2;
    [SerializeField] TownHall th1, th2;
    [SerializeField] List<ElixirAccelerator> accelerators;
    public static GameController Instance = null;

    public GameController()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    void OnClientConnected(ulong clientId)
    {
        NetworkObject playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (NetworkManager.Singleton.IsHost && (clientId == NetworkManager.Singleton.LocalClientId))
        {
            Debug.Log("Host: " + clientId);
            player1 = playerObject.transform.GetChild(1).GetComponent<PlayerController>();
            th1.teamColor = player1.GetTeamColor();
            foreach(ElixirAccelerator ea in accelerators)
            {
                ea.UpdateColors();
            }
            player1.SetTownHalls(th1, th2);
            player1.SetEA(accelerators);
            th1.Repaint();
        }
        else
        {
            Debug.Log("Client: " + clientId);
            player2 = playerObject.transform.GetChild(1).GetComponent<PlayerController>();
            th2.teamColor = player2.GetTeamColor();
            foreach (ElixirAccelerator ea in accelerators)
            {
                ea.UpdateColors();
            }
            player2.SetTownHalls(th2, th1);
            player2.SetEA(accelerators);
            th2.Repaint();
        }
    }

    public Color GetPlayerColor(int player)
    {
        switch(player)
        {
            case 1:
                return (player1 != null) ? player1.GetTeamColor() : Color.white;
            case 2:
                return (player2 != null) ? player2.GetTeamColor() : Color.white;
            default:
                return Color.white;
        }
    }
}
