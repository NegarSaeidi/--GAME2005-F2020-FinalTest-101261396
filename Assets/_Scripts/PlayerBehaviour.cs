using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;
  

    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    private float startTime;
    public bool inAir;
   
    public bool fallDown;
    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;
    public Vector3 dir;
    private bool activate;
    void start()
    {
        startTime =0.0f;
      
        inAir = false;
        fallDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            activate = true;
        }

        if (inAir)
        {
           
            if (Time.time- startTime > 0.05)
            {
               
                fallDown = true;
               
            }
        }
        if(activate)
        {
            _Fire();
            _Move();
        }
        
       
    }

    private void _Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f) 
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y
            
            
            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
               
                inAir = true;
                if (Input.GetAxisRaw("Vertical") > 0.0f)
                    dir = playerCam.transform.forward + transform.up;
              
                else
                    dir = transform.up;
                body.velocity = dir * speed * 0.1f * Time.deltaTime;
                startTime = Time.time;

            }
          if(fallDown)
            {
                body.velocity = -1 * transform.up * speed * 0.1f * Time.deltaTime;
                if (cube.isColliding)
                {
                    inAir = false;
                    fallDown = false;
                  
                }
               

            }

         
           
                transform.position += body.velocity;
            if (body.HitTop)
                transform.position.Set(transform.position.x,body.platformHeight, transform.position.z);


        }
    }


    private void _Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            // delays firing
          //  if (Time.frameCount % fireRate == 0)
           // {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
           // }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
