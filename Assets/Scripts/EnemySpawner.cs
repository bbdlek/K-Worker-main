using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;

    public bool enableSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        GameObject enemy = Instantiate(Enemies[Random.Range(0, 5)], new Vector3(52, -1.86f), Quaternion.identity);
        enemy.GetComponent<Enemy>().isStage2 = true;
        enemy.GetComponent<Enemy>().hp = 1;
        enemy.tag = "Enemy";
        enemy.layer = 9;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        if(enableSpawn)
            StartCoroutine(Spawn());
    }
}
