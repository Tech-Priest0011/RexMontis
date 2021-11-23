using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints1 : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject player;

    private Vector3 respawnLocation;

    void Awake() {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");  
    }

    // Start is called before the first frame update
    void Start()
    {
     player = (GameObject)Resources.Load("Player1", typeof (GameObject));

     respawnLocation = player.transform.position;

     SpawnPlayer();
    }

    private void SpawnPlayer(){
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(player, spawnLocations[spawn].transform.position, Quaternion.identity);
    }
}
