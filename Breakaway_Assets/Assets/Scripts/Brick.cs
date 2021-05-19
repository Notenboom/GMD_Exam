using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour , Ibrick
{
    #region VARIABLES
    [SerializeField] public int score;
    [SerializeField] public int health;
    [SerializeField] public Sprite damagedsprite;
    [SerializeField] private Transform GameOverParticle;
    [SerializeField] private bool RotateBrick;
    [SerializeField] private bool clockwise;
    [SerializeField] public float rotationstart;
    [SerializeField] public float rotationspeed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameoverevent += GameOverBurst;

        #region ROTATION LOGIC
        rotationstart = 0f;
        rotationspeed = Random.Range(1f, 3f);
        int randomdirection = Random.Range(0, 2);
        if(randomdirection == 0)
        {
            clockwise = true;
        }
        if (randomdirection == 1)
        {
            clockwise = false;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region ROTATION LOGIC
        if (RotateBrick == true)
        {
            if (clockwise == true)
            {
                transform.rotation = Quaternion.Euler(0,0, rotationstart);
                rotationstart -= rotationspeed;
            }
            if (clockwise == false)
            {
                transform.rotation = Quaternion.Euler(0, 0, rotationstart);
                rotationstart += rotationspeed;
            }
        }
        #endregion
    }

    private void OnDestroy()
    {
        GameManager.gameoverevent -= GameOverBurst;
    }

    public void DammageBrick()
    {
        FindObjectOfType<AudioManager>().Play("brick_dammaged");
        health--;
        GetComponent<SpriteRenderer>().sprite = damagedsprite;
    }

    public void GameOverBurst()
    {
        Transform temp_particle = Instantiate(GameOverParticle, this.transform.position, this.transform.rotation);
        Destroy(temp_particle.gameObject, 2f);
        Destroy(gameObject); 
    }
}
