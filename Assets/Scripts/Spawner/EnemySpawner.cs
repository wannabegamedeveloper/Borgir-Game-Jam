using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        InvokeRepeating("Spawn", 1.5f, 1.5f);
    }

    private void Spawn()
    {
        GameObject temp = Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(temp, 2f);
    }
}
