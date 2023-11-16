using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;

    [SerializeField] float MinX = 0;
    [SerializeField] float MaxX = 0;
    [SerializeField] float Minz = 0;
    [SerializeField] float MaxZ = 0;

    private void Start()
    {
        Vector3 randamPosition = new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(Minz, MaxZ));
        PhotonNetwork.Instantiate(PlayerPrefab.name, randamPosition, Quaternion.identity);
    }
}
