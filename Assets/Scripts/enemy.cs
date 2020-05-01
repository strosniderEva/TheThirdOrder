using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    float speed;
    public int health;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        //health = 100;
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
        GameManager.Instance.credits += 500;
    }

    private void FixedUpdate()
    {
        move();
        turn();
        shoot();
    }

    void move()
    {
        speed = Vector3.Distance(transform.position, player.transform.position) * 0.2f;
        rb.velocity = transform.forward * speed;
    }

    void turn()
    {
        Quaternion rot = Quaternion.LookRotation(player.transform.position - transform.position, player.transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 1 + 1 * (speed * 0.9f));
    }

    void shoot()
    {
        LineRenderer laser = transform.Find("Line").GetComponent<LineRenderer>();
        laser.useWorldSpace = enabled;
        int range = 20;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
            if (hit.transform.GetComponent<Player>())
            {
                Vector3 dir = hit.point - transform.position;
                Debug.DrawRay(transform.position, dir, Color.green);
                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, transform.position + dir);
                laser.enabled = true;
                FindObjectOfType<PlayerStats>().hurt(20 * Time.deltaTime);
            }
            else
            {
                laser.enabled = false;
            }
        }
        else
        {
            laser.enabled = false;
        }
    }
}