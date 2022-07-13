using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class NetworkListHolderGenerator : NetworkBehaviour
{
    [SerializeField] private NetworkListHolder _networkListHolderPrefab;

    private void OnGUI()
    {
        if (!IsServer) return;
        
        GUILayout.Space(300);

        // NetworkList is not synchronized.
        if (GUILayout.Button("before spawn"))
        {
            var holder = Instantiate(_networkListHolderPrefab);
            holder.UpdateNetworkList();
            holder.GetComponent<NetworkObject>().Spawn();
        }

        // NetworkList is not synchronized.
        if (GUILayout.Button("after spawn"))
        {
            var holder = Instantiate(_networkListHolderPrefab);
            holder.GetComponent<NetworkObject>().Spawn();
            holder.UpdateNetworkList();
        }

        // NetworkList is synchronized.
        if (GUILayout.Button("1 second after spawn"))
        {
            StartCoroutine(UpdateNetworkListAfter1Sec());
        }
    }

    private IEnumerator UpdateNetworkListAfter1Sec()
    {
        var holder = Instantiate(_networkListHolderPrefab);
        holder.GetComponent<NetworkObject>().Spawn();
        yield return new WaitForSeconds(1f);
        holder.UpdateNetworkList();
    }
}
