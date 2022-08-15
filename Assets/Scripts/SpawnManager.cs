using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] powerup;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool stop_Spawning = false;
  
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
       
        while (stop_Spawning == false)
        {
            
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
          GameObject  newEnemy= Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5);
        }

  
       
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (stop_Spawning == false)
        {
            int powerUpValue = Random.Range(0, 3); 
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(powerup[powerUpValue], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,7));
        }
       
    }

    public void OnPlayerDeath()
    {
        stop_Spawning = true;
    }
}
