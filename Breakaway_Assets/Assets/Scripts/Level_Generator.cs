using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generator : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject[] spawnbrick;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        generatelevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region RANDOM BRICK SPAWNING
    public void generatelevel()
    {
        int randomshouldwespawn = Random.Range(0, 101);
        if (randomshouldwespawn < 95)
        {
            int randomspawn = Random.Range(0, spawnbrick.Length);
            Instantiate(spawnbrick[randomspawn], transform.position, Quaternion.identity);
        }
    }
    #endregion
}
