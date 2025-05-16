using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movingSpeed,timeStamp;
    Vector2 movement;
    Rigidbody2D rb;
    public int stamina;
    bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        stamina=100;
        timeStamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.LeftShift) && stamina >0)
        {
            timeStamp += Time.deltaTime;
            movingSpeed =3;
            if (timeStamp >= 0.3f)
            {
                stamina--;
                timeStamp = 0;
            }
        }
        else
        {
            movingSpeed =2;
        }
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina < 100)
            {
                timeStamp += Time.deltaTime;
                if (timeStamp >= 0.5f)
                {
                    stamina++;
                    timeStamp = 0;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        rb.velocity=movement*movingSpeed;
    }
}
