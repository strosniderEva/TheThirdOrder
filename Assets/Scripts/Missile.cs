using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public float Speed = 40;
    public int LookSpeed = 10;
    public float TimeTillTrack = 0;
    public float Timer;
    public float DistanceTillStopLooking;
    public float CalculatedDistance;
    public Vector3 Target;
    public Quaternion targetRotation;
    public GameObject FoundTargetObject;
    public GameObject Explosion;
    public bool stopTurning;
    public int TimeTillExpire;
    public bool Die;
    public int missileDamage = 50;

    void Start()
    {
        //Find the target object
        FoundTargetObject = FindClosestEnemy();
        if (FoundTargetObject != null)
        {
            Target = FoundTargetObject.transform.position;
        }

        TimeTillExpire = 5;
    }

    void Update()
    {
        //pulling values from Gamemanager
        missileDamage = GameManager.Instance.missileDamage;
        DistanceTillStopLooking = GameManager.Instance.missileRange;

        //Find the closest target object
        FoundTargetObject = FindClosestEnemy();
        if (FoundTargetObject != null)
        {
            Target = FoundTargetObject.transform.position;
        }

        //find the distance from missile to target
        CalculatedDistance = Vector3.Distance(gameObject.transform.position, Target);

        //set up the timer
        Timer += Time.deltaTime;
        //destroy if missile's time is up
        if (Timer > TimeTillExpire)
        {
            Die = true;
            Debug.Log("life timer");
        }

        //give the missile speed
        transform.Translate(0, 0, Speed / 100);
        //delay tracking for a certain amount of time...
        if (Timer > TimeTillTrack)
        {
            if (stopTurning == false)
            {
                //look at the target object at speed
                targetRotation = Quaternion.LookRotation(Target - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);
            }
        }


        //set up instances that the missile will die...
        //if all enemies are too far away
        if (CalculatedDistance > DistanceTillStopLooking)
        {
            stopTurning = true;
            //Destroy(gameObject);
            Die = true;
            Debug.Log("distance death");
        }
        if (Die == true)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {

        enemy enemyhit = other.gameObject.GetComponent<enemy>();

        if (enemyhit != null)
        {
            enemyhit.TakeDamage(missileDamage);
            Debug.Log("damage given");
        }
        Debug.Log("impact");
        // Destroy(gameObject);
        Die = true;
    }

    //sorts through array of enemies until it finds the closest one
    public GameObject FindClosestEnemy()
    {
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in allEnemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        return closest;
    }

}
