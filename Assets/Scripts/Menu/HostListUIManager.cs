using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Если используете TextMeshPro для текста

public class HostListUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform contentParent; // Контейнер для элементов списка (например, ScrollView Content)
    [SerializeField] private GameObject hostItemPrefab; // Префаб элемента списка (кнопка с текстом)

    // Словарь для хранения IP по названию хоста (можно и по другому ключу)
    private Dictionary<GameObject, string> hostItems = new Dictionary<GameObject, string>();

    /// <summary>
    /// Добавляет хост в UI список.
    /// </summary>
    /// <param name="hostInfo">Отображаемая информация (название + IP)</param>
    /// <param name="ipAddress">IP адрес для подключения</param>
    public void AddHostToUIList(string hostInfo, string ipAddress)
    {
        // Создаем новый элемент списка из префаба
        GameObject item = Instantiate(hostItemPrefab, contentParent);

        // Находим компонент текста (Text или TextMeshPro)
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

        // Сохраняем IP для этого элемента
        hostItems[item] = ipAddress;

        // Добавляем обработчик нажатия на кнопку
        Button btn = item.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => OnHostItemClicked(item));
        }
    }

    /// <summary>
    /// Очищает список хостов.
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
    /// Обработка нажатия на элемент списка.
    /// </summary>
    /// <param name="item">Объект UI элемента, на который нажали</param>
    private void OnHostItemClicked(GameObject item)
    {
        if (hostItems.TryGetValue(item, out string ipAddress))
        {
            Debug.Log("Подключаемся к хосту: " + ipAddress);

            // Здесь вызываем метод подключения к выбранному IP
            ConnectToHost(ipAddress);
        }
    }

    /// <summary>
    /// Метод подключения к хосту (реализуйте логику подключения в вашем NetworkManager)
    /// </summary>
    /// <param name="ipAddress"></param>
    private void ConnectToHost(string ipAddress)
    {
        var utp = (Unity.Netcode.Transports.UTP.UnityTransport)Unity.Netcode.NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        utp.SetConnectionData(ipAddress, 7778); // Порт должен совпадать с настройками хоста

        Unity.Netcode.NetworkManager.Singleton.StartClient();
    }
}
