using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyObject;
    [SerializeField] private Transform enemyBox;

    [SerializeField] private int[] chance;
    [SerializeField] private int maxEnemies;
    private int enemiesAmount;
    [SerializeField] private float spawnTime;
    
    IEnumerator Spawn()
    {
        while (enemiesAmount < maxEnemies)
        {
            int random = Random.Range(0, 101);
            for (int i = 0; i < chance.Length; i++)
            {
                if (random < chance[i])
                {
                    Instantiate(enemyObject[i], transform.position, transform.rotation, enemyBox);
                    break;
                }
                else
                {
                    random -= chance[i];
                }
            }
            enemiesAmount++;
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void NewWave(int _maxEnemies)
    {
        maxEnemies = _maxEnemies;
        enemiesAmount = 0;
        StartCoroutine(Spawn());
    }

    public bool CheckEnd()
    {
        return maxEnemies == enemiesAmount;
    }
}
