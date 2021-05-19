using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePaddle : MonoBehaviour, IPowerup
{
    #region VARIABLES
    [SerializeField] PowerupManager manager;
    #endregion

    #region APPLY POWERUP
    public void applypowerup()
    {
        manager.LargePaddle();
        destroypowerup();
    }
    #endregion

    #region DESTROY POWERUP
    public void destroypowerup()
    {
        Destroy(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("PowerupManager").GetComponent<PowerupManager>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10f)
        {
            destroypowerup();
        }
    }

    #region COLLISION WITH PADDLE
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "paddle")
        {
            applypowerup();
        }
    }
    #endregion
}
