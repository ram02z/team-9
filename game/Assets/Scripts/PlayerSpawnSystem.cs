using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawnSystem : NetworkBehaviour
{
    
    [SerializeField] private GameObject playerPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform) {
        spawnPoints.Add(transform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    // public override void OnStartServer() => NetworkRoomManager.On
}
