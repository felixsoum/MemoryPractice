using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Move : MonoBehaviour
{
    Image image;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Vector2 dir = new Vector2();
    public float speed = 300.0f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Animate()
    {
        anim.SetFloat("Magnitude", rigid.velocity.magnitude);
        anim.SetFloat("Hor", dir.x);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        dir = context.ReadValue<Vector2>();
    }

    private void Movement()
    {
        Vector2 pos = new Vector2();
        pos += (dir.normalized * speed) * Time.fixedDeltaTime;
        rigid.velocity = pos;
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = spriteRenderer.sprite;
        //OnMove();
    }

    private void FixedUpdate()
    {
        Animate();
        Movement();
    }
}
