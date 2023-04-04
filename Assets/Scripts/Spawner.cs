using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject melee;
    [SerializeField] private GameObject quickmelee;
    [SerializeField] private GameObject kamikaze;
    const int zombieLimit = 4;
    [SerializeField] private float spawnRate = 10;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    public void Spawn(int id) {
        switch (id) {
            case 0:
                Instantiate(melee, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(quickmelee, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(kamikaze, transform.position, Quaternion.identity);
                break;
        }
    }

    IEnumerator SpawnRoutine() {
        while (GameManager.Instance.canSpawn) 
        {
            if (GameManager.Instance.zombieCount < zombieLimit + (GameManager.Instance.GetCoins() * 2)) {
                int zombieID = Random.Range(0,3);
                GameManager.Instance.AddZombie();
                Spawn(zombieID);
                yield return new WaitForSeconds(spawnRate);
                
            }
            else {
                yield return new WaitForSeconds(20);
            }
        }
        yield break;
    }
}
