using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam; //quick reference to main camera
    float thrust; //forward/backward throttle (must be outside functions)
    Rigidbody rb;//reference to attached rigidbody
    float topSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
		//setup camera and rigidbody
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
		//run these functions in fixedUpdate so that they are framerate independent and run at a steady rate
        move();
        turn();
        camstick();
    }

    void move()
    {
		//add or remove from thrust based on user input
        thrust += Input.GetAxis("Vertical");
		//clamp thrust to topSpeed
        thrust = Mathf.Clamp(thrust, -topSpeed, topSpeed);
		//create variables for each axis of velocity
        float fwd = thrust;
        float horz = 0;//TODO: add strafing
        float vert = 0;
		//create a new vector using all 3 axes
        Vector3 vel = new Vector3(horz, vert, fwd);
		//rotate vector to align with players rotation
        vel = transform.rotation * vel;
		//We're setting velocity so that we can have direct control of player movement
        rb.velocity = vel;
    }

    void turn()
    {
        float sensitivity = 15;     //use 5 when building
		//set roll equal to horizontal mouse movement and multiply it by sensitivity
        float roll = -Input.GetAxis("Mouse X") * sensitivity;
		//set roll equal to vertical mouse movement and multiply it by sensitivity
        float pitch = Input.GetAxis("Mouse Y") * sensitivity;
		//multiply rotation by a new quaternion using previouse variables
        transform.rotation *= Quaternion.Euler(pitch, 0, roll);

		//show local forward and right orientations for testing purposes
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        Debug.DrawRay(transform.position, transform.right * 10, Color.magenta);
    }

	//think of a selfieStick attached to back of ship
    void camstick()
    {
		//TODO: add raycast for collisions with camera stick
		//offset is the length and direction of stick
        Vector3 offset = new Vector3(0.5f, 3.45f, -12.5f);
		//rotate stick to match ships orientation
        offset = transform.rotation * offset;
		
		//both of these functions are to make camera movement smoother and less stiff, it feels better trust me
		//interpolate camera position towards ship's selfieStick at the speed of 1 + speed of ship
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + offset, (5 + Mathf.Abs(thrust)) * Time.deltaTime);
		//interpolate camera rotation towards ships rotation
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation,transform.rotation,5 * Time.deltaTime);
    }
}
