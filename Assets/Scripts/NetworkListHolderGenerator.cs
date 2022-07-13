using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class NetworkListHolderGenerator : NetworkBehaviour
{
    [SerializeField] private NetworkListHolder _networkListHolderPrefab;

    private void Update()
    {
        if (!IsServer) return;

        // NetworkList is not synchronized.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.LogError("Update NetworkList before spawn.");
            var holder = Instantiate(_networkListHolderPrefab);
            holder.UpdateNetworkList();
            holder.GetComponent<NetworkObject>().Spawn();
        }

        // NetworkList is not synchronized.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.LogError("Update NetworkList after spawn.");
            var holder = Instantiate(_networkListHolderPrefab);
            holder.GetComponent<NetworkObject>().Spawn();
            holder.UpdateNetworkList();
        }

        // NetworkList is synchronized.
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(UpdateNetworkListAfter1Sec());
        }
    }

    private IEnumerator UpdateNetworkListAfter1Sec()
    {
        Debug.LogError("Update NetworkList after 1 sec spawn.");
        var holder = Instantiate(_networkListHolderPrefab);
        holder.GetComponent<NetworkObject>().Spawn();
        yield return new WaitForSeconds(1f);
        holder.UpdateNetworkList();
    }
}
