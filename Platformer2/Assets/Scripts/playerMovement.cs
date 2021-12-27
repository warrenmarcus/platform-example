using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{


    private  Rigidbody2D player;
    private Collider2D coll;


    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int pumpkin = 0;
    [SerializeField] private Text pumpkinText;
    [SerializeField] private float hurtForce = 10f;

    public enum State {idle, running, jumping, falling, hurt}
    public State state = State.idle;
    
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        
    }

    private void Update()
    {

        if (state != State.hurt)
        {
            Movement();
            
        }

        stateSelect();
    }





    private void Movement()
    {
        float xDirection = Input.GetAxis("Horizontal");

        if (xDirection < 0)
        {
            player.velocity = new Vector2(-speed, player.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (xDirection > 0)
        {
            player.velocity = new Vector2(speed, player.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
        {
            Jump();
        }

    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, jumpForce);
        state = State.jumping;

    }

    private void stateSelect()
    {
        if (state == State.jumping)
        {
            if(player.velocity.y < .1)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {

            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (state == State.hurt)
        {
            if (Mathf.Abs(player.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }


        else if (Mathf.Abs(player.velocity.x) > 2f )
        {
            state = State.running;
        }


        else
        {
            state = State.idle;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            pumpkin++;
            pumpkinText.text = pumpkin.ToString();
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            if (state == State.falling)
            {
                Destroy(other.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    player.velocity = new Vector2(-hurtForce, player.velocity.y);

                }
                else
                {
                    // move player to the right and take damage
                    player.velocity = new Vector2(hurtForce, player.velocity.y);
                }
                
            }
            
        }

        

    }

}
