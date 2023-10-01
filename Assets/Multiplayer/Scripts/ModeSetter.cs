using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class ModeSetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputText;

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        Debug.Log(NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address);
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = inputText.text;
        NetworkManager.Singleton.StartClient();
        Debug.Log(NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address);
    }
}
