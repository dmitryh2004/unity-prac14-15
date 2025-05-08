using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

// Класс для обнаружения хостов
public class LANDiscovery : MonoBehaviour
{
    [SerializeField] private int broadcastPort = 7777;
    private UdpClient _udpClient;
    private Coroutine _searchRoutine;

    // Хост: отправляет сообщения о себе
    public void StartBroadcast(string gameName)
    {
        _udpClient = new UdpClient();
        _udpClient.EnableBroadcast = true;
        StartCoroutine(BroadcastCoroutine(gameName));
    }

    IEnumerator BroadcastCoroutine(string gameName)
    {
        while (true)
        {
            byte[] data = Encoding.UTF8.GetBytes(gameName);
            _udpClient.Send(data, data.Length, "255.255.255.255", broadcastPort);
            yield return new WaitForSeconds(1);
        }
    }

    // Клиент: ищет хосты
    public void StartSearch(Action<string> onHostFound)
    {
        _udpClient = new UdpClient(broadcastPort);
        _searchRoutine = StartCoroutine(SearchCoroutine(onHostFound));
    }

    IEnumerator SearchCoroutine(Action<string> onHostFound)
    {
        while (true)
        {
            IPEndPoint remoteEP = null;
            byte[] data = _udpClient.Receive(ref remoteEP);
            string gameName = Encoding.UTF8.GetString(data);
            onHostFound?.Invoke($"{gameName} [{remoteEP.Address}]");
        }
    }
}
