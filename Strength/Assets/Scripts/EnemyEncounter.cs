using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    //field for a list of waves
    [SerializeField]
    private List<Vector4> encounter;

    //field for a list of enemy gameObjects
    [SerializeField]
    private List<GameObject> enemyTypes;

    //list of living enemies
    private List<Enemy> livingEnemies;

    //current wave number
    private int waveNumber;

    //field for the player character
    [SerializeField]
    private Player player;

    //field for the minimum and maximum range of enemy spawning
    [SerializeField]
    private Vector2 spawnRange;

    // Start is called before the first frame update
    void Start()
    {
        //initialize wave number
        waveNumber = 0;

        //initialize living enemy list
        livingEnemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(livingEnemies.Count == 0)
        {
            //debug for wave changes
            Debug.Log("wave cleared");

            //create new wave of enemies
            CreateWave(encounter[waveNumber]);

            //increase the wave count
            waveNumber++;

            //end the encounter when all waves are clear
            if (waveNumber >= encounter.Count)
            {
                //TODO end encounter
            }
        }
    }

    private void CreateWave(Vector4 enemies)
    {
        //create temporary list to hold enemies for a specific wave
        livingEnemies = new List<Enemy>();

        //add specified number of golems to living enemy list
        for(int i = 0; i < enemies.x; i++)
        {
            livingEnemies.Add(Instantiate(enemyTypes[0], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>());
        }

        //add specified number of dark nights to living enemy list
        for (int i = 0; i < enemies.y; i++)
        {
            livingEnemies.Add(Instantiate(enemyTypes[1], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>());
        }

        //add specified number of dark mages to living enemy list
        for (int i = 0; i < enemies.z; i++)
        {
            livingEnemies.Add(Instantiate(enemyTypes[2], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>());
        }

        //add specified number of test enemies to living enemy list
        for (int i = 0; i < enemies.w; i++)
        {
            livingEnemies.Add(Instantiate(enemyTypes[3], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>());
        }
    }

    private Vector2 GetRandomLocation()
    {
        //range of enemy spawn
        float enemyRange = Random.Range(spawnRange.x, spawnRange.y);

        //angle of spawn
        float spawnAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

        //Direction of spawn raycast
        Vector2 spawnDirection = new Vector2(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle));

         RaycastHit2D[] spawnCast = Physics2D.RaycastAll(player.Location, spawnDirection, enemyRange);

        foreach(RaycastHit2D spawnCheck in spawnCast)
        {
            //check if the raycast hit a wall, or else run the method again
            if(spawnCheck.collider.tag == "Wall")
            {
                return GetRandomLocation();
            }
        }

        return player.Location + spawnDirection * spawnRange;
    }
}
