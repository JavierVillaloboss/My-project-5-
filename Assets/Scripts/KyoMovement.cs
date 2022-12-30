using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyoMovement : MonoBehaviour
{
    public float SpeedKyo;
    public float JumpForce;
    public bool Colision;
    public bool Muerte;
    

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    public int Health = 10;
   

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        

        if (Horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        Animator.SetBool("Correr", Horizontal != 0.0f);
         

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        Animator.SetBool("Saltar", true);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if (collision.gameObject.tag == "Pincho")
        {
            Hit();
            
            Muerte = true;
        }
        
        

        if (collision.gameObject.tag == "Suelo")
        {
            this.Colision = true;
            Animator.SetBool("Saltar", false);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * SpeedKyo, Rigidbody2D.velocity.y);
        
    }

    private void Hit()
    {
        Health = Health - 1;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
