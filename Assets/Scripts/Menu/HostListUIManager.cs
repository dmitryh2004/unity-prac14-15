using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���� ����������� TextMeshPro ��� ������

public class HostListUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform contentParent; // ��������� ��� ��������� ������ (��������, ScrollView Content)
    [SerializeField] private GameObject hostItemPrefab; // ������ �������� ������ (������ � �������)

    // ������� ��� �������� IP �� �������� ����� (����� � �� ������� �����)
    private Dictionary<GameObject, string> hostItems = new Dictionary<GameObject, string>();

    /// <summary>
    /// ��������� ���� � UI ������.
    /// </summary>
    /// <param name="hostInfo">������������ ���������� (�������� + IP)</param>
    /// <param name="ipAddress">IP ����� ��� �����������</param>
    public void AddHostToUIList(string hostInfo, string ipAddress)
    {
        // ������� ����� ������� ������ �� �������
        GameObject item = Instantiate(hostItemPrefab, contentParent);

        // ������� ��������� ������ (Text ��� TextMeshPro)
        TextMeshProUGUI textComponent = item.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = hostInfo;
        }
        else
        {
            Text text = item.GetComponentInChildren<Text>();
            if (text != null)
                text.text = hostInfo;
        }

        // ��������� IP ��� ����� ��������
        hostItems[item] = ipAddress;

        // ��������� ���������� ������� �� ������
        Button btn = item.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => OnHostItemClicked(item));
        }
    }

    /// <summary>
    /// ������� ������ ������.
    /// </summary>
    public void ClearHostList()
    {
        foreach (var item in hostItems.Keys)
        {
            Destroy(item);
        }
        hostItems.Clear();
    }



    /// <summary>
    /// ��������� ������� �� ������� ������.
    /// </summary>
    /// <param name="item">������ UI ��������, �� ������� ������</param>
    private void OnHostItemClicked(GameObject item)
    {
        if (hostItems.TryGetValue(item, out string ipAddress))
        {
            Debug.Log("������������ � �����: " + ipAddress);

            // ����� �������� ����� ����������� � ���������� IP
            ConnectToHost(ipAddress);
        }
    }

    /// <summary>
    /// ����� ����������� � ����� (���������� ������ ����������� � ����� NetworkManager)
    /// </summary>
    /// <param name="ipAddress"></param>
    private void ConnectToHost(string ipAddress)
    {
        var utp = (Unity.Netcode.Transports.UTP.UnityTransport)Unity.Netcode.NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        utp.SetConnectionData(ipAddress, 7778); // ���� ������ ��������� � ����������� �����

        Unity.Netcode.NetworkManager.Singleton.StartClient();
    }
}
