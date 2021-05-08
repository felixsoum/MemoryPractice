using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float moveSpeed = 10f;

    private Rigidbody2D rigid;
    private Vector3 velocity;
    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("jump");
            rigid.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
        }
    }

    private void Animate()
    {
        anim.SetFloat("Hor", Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal")<0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
           
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
         
        }

    }
   


    // Update is called once per frame
    void Update()
    {


        Animate();
        Jump();
        Vector3 inputMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,0f);
        velocity = inputMove.normalized * moveSpeed;
        transform.position += velocity * Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
            
            //rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
        
        
    }


}
