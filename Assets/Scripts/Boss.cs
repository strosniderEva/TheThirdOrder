using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Vector3 destination;
    Rigidbody rb;
    float speed;
    public int health;
    public GameObject explosion;
    public GameObject minion;
    float spawnTimer = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 1000;

        float xpos = Random.Range(-130, 130);
        float zpos = Random.Range(-130, 130);
        float ypos = Random.Range(-80, 80);
        destination = new Vector3(xpos, ypos, zpos);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //enemy dies
            Die();
        }
    }

    void Die()
    {
        GameObject ex = Instantiate(explosion, transform.position, transform.rotation);
        ex.transform.Find("Particle System").GetComponent<ParticleSystem>().startSize = 10;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        patrol();
        move();
        turn();
    }

    private void Update()
    {
        spawnTimer -= 1 * Time.deltaTime;
        if(spawnTimer<=0)
        {
            deploy();
            spawnTimer = 4;
        }
    }

    void patrol()
    {
        Debug.DrawRay(transform.position, destination - transform.position, Color.green);
        if(Vector3.Distance(transform.position,destination) < 20 || destination == null)    
        {
            float xpos = Random.Range(-130, 130);
            float zpos = Random.Range(-130, 130);
            float ypos = Random.Range(-80, 80);
            destination = new Vector3(xpos, ypos, zpos);
        }
    }

    void deploy()
    {
        float maxEnemies = 5;
        enemy[] enemies = FindObjectsOfType<enemy>();
        if(enemies.Length < maxEnemies)
        {
            //spawn enemy in front of ship
            GameObject newEnemy = Instantiate(minion, transform.position + transform.forward, transform.rotation);
            Physics.IgnoreCollision(newEnemy.GetComponent<Collider>(), GetComponent<Collider>());
            foreach (enemy e in enemies)
            {
                Physics.IgnoreCollision(e.GetComponent<Collider>(), GetComponent<Collider>());
            }            
        }
    }

    void move()
    {
        speed = 0.2f + Vector3.Distance(transform.position, destination) * 0.1f;
        rb.velocity = transform.forward * speed;
    }

    void turn()
    {
        Quaternion rot = Quaternion.LookRotation(destination - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 0.1f + speed * 0.1f);
    }
}
