using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float moveSpeed = 10f;

    private Rigidbody2D rigid;
    private Vector2 velocity;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void Animate()
    {
        anim.SetFloat("Hor", Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal")<0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {


        Animate();
        Vector2 inputMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        velocity = inputMove.normalized * moveSpeed;
        
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
        
    }


}
