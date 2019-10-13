using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    //list of all enemies in a wave
    private List<Enemy> waveEnemies;

    //current wave number
    private int waveNumber;

    //field for the player character
    [SerializeField]
    private Player player;

    //field for the minimum and maximum range of enemy spawning
    [SerializeField]
    private Vector2 spawnRange;

    //field for a list of obstacle tile positions
    [SerializeField]
    private List<Vector3Int> obstacleTiles;

    //field for the level's tile map and its tilemap component
    [SerializeField]
    private GameObject levelTiles;
    private Tilemap levelMap;

    //field for obstacle tile base
    [SerializeField]
    private TileBase obstacleTile;

    //field to check if the player is in an encounter
    private bool inEncounter;

    // Start is called before the first frame update
    void Start()
    {
        //initialize wave number
        waveNumber = 0;

        //initialize encounter bool
        inEncounter = false;

        //initialize living enemy list
        livingEnemies = new List<Enemy>();

        //intialize tile map
        levelMap = levelTiles.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //spawn the next wave when all living enemies are destroyed
        if(inEncounter == true)
        {
            if (livingEnemies.Count == 0)
            {
                //debug for wave changes
                Debug.Log("wave cleared");

                //increase the wave count
                waveNumber++;

                //end the encounter when all waves are clear
                if (waveNumber >= encounter.Count)
                {
                    Debug.Log("Encounter over");

                    //remove obstacle tiles
                    for (int i = 0; i < obstacleTiles.Count; i++)
                    {
                        levelMap.SetTile(obstacleTiles[i], null);
                    }

                    inEncounter = false;

                    Destroy(gameObject);
                }
                else
                {
                    //create new wave of enemies
                    CreateWave(encounter[waveNumber]);
                }
            }

            //remove an enemy if it's been destroyed
            foreach (Enemy enemy in waveEnemies)
            {
                if (enemy == null)
                {
                    livingEnemies.Remove(enemy);
                }
            }
        }
    }

    private void CreateWave(Vector4 enemies)
    {
        //initialize lists to hold enemies for a specific wave
        livingEnemies = new List<Enemy>();
        waveEnemies = new List<Enemy>();

        //add specified number of golems to living enemy list
        for(int i = 0; i < enemies.x; i++)
        {
            Enemy golem = Instantiate(enemyTypes[0], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>();
            livingEnemies.Add(golem);
            waveEnemies.Add(golem);
        }

        //add specified number of dark nights to living enemy list
        for (int i = 0; i < enemies.y; i++)
        {
            Enemy darkKnight = Instantiate(enemyTypes[1], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>();
            livingEnemies.Add(darkKnight);
            waveEnemies.Add(darkKnight);
        }

        //add specified number of dark mages to living enemy list
        for (int i = 0; i < enemies.z; i++)
        {
            Enemy darkMage = Instantiate(enemyTypes[2], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>();
            livingEnemies.Add(darkMage);
            waveEnemies.Add(darkMage);
        }

        //add specified number of test enemies to living enemy list
        for (int i = 0; i < enemies.w; i++)
        {
            Enemy test = Instantiate(enemyTypes[3], GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>();
            livingEnemies.Add(test);
            waveEnemies.Add(test);
        }
    }

    /// <summary>
    /// method that spawns an object at a random distance away from the player 
    /// </summary>
    /// <returns> the enemy's spawn location </returns>
    private Vector2 GetRandomLocation()
    {
        //get encounter object's position
        Vector3 spawnCenter = gameObject.transform.position;
        float spawnHeight = GetComponent<BoxCollider2D>().bounds.extents.x;
        float spawnWidth = GetComponent<BoxCollider2D>().bounds.extents.y;
        Vector2 spawnOffset = GetComponent<BoxCollider2D>().offset;

        return (Vector2) spawnCenter + spawnOffset + new Vector2(Random.value * spawnWidth, Random.value * spawnHeight);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //disable object's collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Debug.Log("Encounter start");

        //close off openings with obstacle tiles
        for(int i = 0; i < obstacleTiles.Count; i++)
        {
            levelMap.SetTile(obstacleTiles[i], obstacleTile);
        }
        
        //set inencounter to true
        inEncounter = true;

        //spawn the first wave of enemies
        CreateWave(encounter[waveNumber]);
    }
}
