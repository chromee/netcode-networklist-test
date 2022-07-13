using System;
using System.Text;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class NetworkListHolder : NetworkBehaviour
{
    private NetworkList<int> _networkNumbers;

    public void UpdateNetworkList()
    {
        for (var i = 0; i < 5; i++)
        {
            _networkNumbers.Add(Random.Range(0, 10));
        }
    }

    public override void OnNetworkSpawn()
    {
        name = $"NetworkSomething{NetworkObjectId}";
    }

    private void Awake()
    {
        _networkNumbers = new NetworkList<int>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var sb = new StringBuilder();
            foreach (var num in _networkNumbers)
            {
                sb.Append($"{num}, ");
            }

            Debug.LogError($"{name}: {sb}");
        }
    }
}
