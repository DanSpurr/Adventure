using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 50f;

    // Variables for jump
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    bool isGrounded = false;
    public Transform groundPoint;
    public float jumpForce = 700;


    private Rigidbody2D rb2d;
    private Animator anim;
    bool facingPositive = true;
    

	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
		
	}
	


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float w = Input.GetAxis("Vertical");

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);

        anim.SetBool("Ground", isGrounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


        rb2d.velocity = new Vector2(h * speed, rb2d.velocity.y);
        if (h > 0 || h < 0 && isGrounded)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Glide", true);
        }
        else
        {
            anim.SetBool("Walk", false);
            //anim.SetBool("Glide", true);
        }

        


        if (h > 0 && !facingPositive)
        {
            Flip();
        }
        else if (h < 0 && facingPositive)
        {
            Flip();
        }


       
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
            anim.SetBool("Ground", false);

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }


    }
    void Flip()
    {
        facingPositive = !facingPositive;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

   // private bool IsGrounded()
   // {
   //     if (rb2d.velocity.y <= 0)
   //     {
   //         foreach (Transform point in groundPoint)
   //         {
   //             Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
   //
   //             for (int i = 0; i < colliders.Length; i++)
   //             {
   //                 if (colliders[i].gameObject != gameObject)
   //                 {
   //                     return true;
   //                 }
   //             }
   //         }
   //
   //     }
   //
   //     return false;
   // }
}
