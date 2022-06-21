using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTManager : MonoBehaviour
{
    public int lives;
    public ParticleSystem fire;
    public ParticleSystem explosion;
    bool death;
    private void Start()
    {
        death = false;
    }
    private void Update()
    {
        if (lives <= 0 && !death)
        {
            death = true;
            GameManager.points += 15;
            StartCoroutine(Explosion());
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
    IEnumerator Explosion()
    {
        explosion.Play();
        fire.Play();
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 12);
        foreach (Collider2D c in colls)
        {
            if (c.GetComponent<Rigidbody2D>() != null)
            {
                c.GetComponent<Rigidbody2D>().AddForce((c.transform.position - transform.position) * 105);
            }
            if (c.GetComponent<StructureManager>() != null)
            {
                c.GetComponent<StructureManager>().lives -= 1;
            }
            if (c.GetComponent<TNTManager>() != null)
            {
                c.GetComponent<TNTManager>().lives -= 1;
            }
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
