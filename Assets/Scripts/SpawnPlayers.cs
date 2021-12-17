using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    public float minX, maxX, minZ, maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 1.0f, Random.Range(minZ, maxZ));

        GameObject temp = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        if (temp.GetComponent<PhotonView>().IsMine)
            temp.GetComponent<PlayerMovement>().SetJoysticks(Instantiate(cameraPrefab, randomPosition, Quaternion.identity));
    }
}