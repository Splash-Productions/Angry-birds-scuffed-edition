using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public int lives;
    private void Update()
    {
        if(lives <= 0)
        {
            GameManager.points += 5;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.isInGame)
        {
            lives--;

            if (collision.gameObject.CompareTag("Player"))
            {
                lives -= 2;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            lives = 0;
        }
    }
}
