using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireWeapon : MonoBehaviour
{
    public AudioSource laserFire;
    public GameObject laserCrosshair;
    public LineRenderer laser;
    public GameObject ship;
    int laserDamage = 50;
    public GameObject TargetPos;
    public GameObject missile;
    public int MissileLimit = 1;
    public int MissileCount;
    public GameObject[] Missiles;
    public int missileRange = 50;
    public int laserRange = 50;

    void Start()
    {
        //Find the target object that already exists in the sceene
        //TargetPos = GameObject.FindGameObjectWithTag("enemy");
        MissileLimit = GameManager.Instance.missileLimit;
        missileRange = GameManager.Instance.missileRange;
        laserRange = GameManager.Instance.laserRange;
        laser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, ship.transform.position);
        //pulling values from Gamemanager
        MissileLimit = GameManager.Instance.missileLimit;
        missileRange = GameManager.Instance.missileRange;
        laserRange = GameManager.Instance.laserRange;
        laserCrosshair.transform.localPosition = new Vector3(0f, 0f, laserRange);

        Missiles = GameObject.FindGameObjectsWithTag("Missile");
        //find the length of the list of missiles
        MissileCount = Missiles.Length;

        //make a list of all the missiles in the scene
        Missiles = GameObject.FindGameObjectsWithTag("Missile");

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("One");
            FireLasers();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Two");
            FireMissiles();
        }
    }

    void FireLasers()
    {
        StartCoroutine("laserFlash");
        RaycastHit hit;

        if (Physics.Raycast(ship.transform.position, ship.transform.forward, out hit, laserRange))
        {
            //hit.transform is the object you hit
            enemy enemyhit = hit.transform.GetComponent<enemy>();
            if (enemyhit != null)
            {
                enemyhit.TakeDamage(laserDamage);
                laser.SetPosition(1, hit.transform.position);
            }
        }
    }

    void FireMissiles()
    {
        //find out if there are too many missiles already in the scene
        if (MissileCount <= MissileLimit - 1)
        {
            //instantiate the missile Prefab
            Instantiate(missile, ship.transform.position, ship.transform.rotation);
            Debug.Log("Missile fired");
        }
    }

    IEnumerator laserFlash()
    {
        laser.SetPosition(1, laserCrosshair.transform.position);
        laserFire.Play();
        laser.enabled = true;
        yield return new WaitForSeconds(0.25f);
        laser.enabled = false;
        StopCoroutine("laserFlash");
    }
}
