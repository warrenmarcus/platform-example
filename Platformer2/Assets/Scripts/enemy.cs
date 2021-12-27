using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float leftcap;
    [SerializeField] private float rightcap;
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D ball;


    //   for adding animations
            private bool facingleft = true;

    void Start()
    {

        coll = GetComponent<Collider2D>();
        ball = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingleft)
        {
            if (transform.position.x > leftcap)
            {
                if(transform.localScale.x !=1)
                {
                    transform.localScale = new Vector3(1, 1);
                }


                if(coll.IsTouchingLayers(ground))
                {
                    ball.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else
            {
                facingleft = false;

            }
        }

        else
        if (transform.position.x < rightcap)
        {
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1, 1);
            }


            if (coll.IsTouchingLayers(ground))
            {
                ball.velocity = new Vector2(jumpLength, jumpHeight);
            }
        }
        else
        {
            facingleft = true;

        }
    }

}
