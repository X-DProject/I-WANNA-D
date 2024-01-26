using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;

    private Vector2 inputDirection;
    private Animator anim;
    private bool isMove = false;
    private bool canControl = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameInstance.Connect("move.ban", OnMoveban);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("move.ban", OnMoveban);
    }

    private void OnMoveban(IMessage rMessage)
    {
        canControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection.x = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            inputDirection.x = 1;
        }
        else
        {
            inputDirection.x = 0;
        }
        
        if(Input.GetKey(KeyCode.W))
        {
            inputDirection.y = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            inputDirection.y = -1;
        }
        else
        {
            inputDirection.y = 0;
        }
    }

    private void FixedUpdate()
    {
        if(canControl)
            Move();
    }

    private void Move()
    {
        if(inputDirection == new Vector2(0,0))
        {
            anim.SetBool("isMove", false);
            isMove = false;
        }
        else
        {
            if(isMove == false)
                anim.SetBool("isMove", true);
            isMove = true;
        }

        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, inputDirection.y * speed * Time.deltaTime);
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
        {
            faceDir = -1;
        }
        if (inputDirection.x < 0)
        {
            faceDir = 1;
        }

        transform.localScale = new Vector3(faceDir, 1, 1);
    }
}
