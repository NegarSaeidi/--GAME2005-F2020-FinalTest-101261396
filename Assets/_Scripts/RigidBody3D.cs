using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    STATIC,
    DYNAMIC
}


[System.Serializable]
public class RigidBody3D : MonoBehaviour
{
    [Header("Gravity Simulation")]
    public float gravityScale;
    public float mass;
    public BodyType bodyType;
    public float timer;
    public bool isFalling;
   
    [Header("Attributes")]
    public Vector3 velocity;
    public Vector3 acceleration;
    private float gravity;
     public float speed;
    public bool HitFront,HitTop;
    public Vector3 direction;
    public bool isplane;
    public bool isplayer;
    public float platformHeight;
    //public string[] platformState = { "ground","onplatform","offplatform"};
    //public string onPlatform;
    public float platformPos;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        gravity = -0.001f;
       
        velocity = Vector3.zero;
        if (bodyType == BodyType.DYNAMIC)
            acceleration = new Vector3(0.0f, gravity * gravityScale, 0.0f);
        else
          acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        if (bodyType == BodyType.DYNAMIC)
        {
            isFalling = true;
        }
        direction = new Vector3(0.0f, 0.0f, 0.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyType == BodyType.DYNAMIC)
        {
            timer += Time.deltaTime;
            if (isFalling)
            {
               

                if (gravityScale < 0)
                {
                    gravityScale = 0;
                    Debug.Log("Ground");
                }

                if (gravityScale > 0)
                {
                    velocity += acceleration * 0.5f * timer * timer;
                    transform.position += velocity;
                }
              //  isFalling = false;
            }
            else
            {
                if (!isplayer)
                {
                  
                    acceleration = speed * direction;
                  
                    velocity = (acceleration * 0.5f * 20);
                    if(velocity.magnitude>0)
                  
                    transform.position += velocity;
                }  
            }
        }
        else if (bodyType == BodyType.STATIC)
        {
           
        }
    }

    public void Stop()
    {
        timer = 0;
        isFalling = false;
    }
}
