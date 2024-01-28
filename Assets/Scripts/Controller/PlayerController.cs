using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behav;
using Tool.Module.Message;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public GameObject bound;

    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private AnimationPlayer anim;
    private bool isMove = false;
    private bool canControl = true;

    [SerializeField]
    private UnityEvent _onWin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationPlayer>();
        anim.SetEmoji(0.5f);
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
            // anim.SetBool("isMove", false);
            if(isMove == true)
            {
                anim.ChangeAnimParamDirectly("is_move bool false");
            }
            isMove = false;
        }
        else
        {
            if(isMove == false)
            {    
                anim.ChangeAnimParamDirectly("is_move bool true");
            }
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Trigger"))
        {
            // ban input 
            canControl = false;
            inputDirection = new Vector2(0,0);
            bound.SetActive(false);
            Destroy(rb);
            // anim
            StartCoroutine(PlayAnim());

            _onWin?.Invoke();
        }
    }

    private IEnumerator PlayAnim()
    {
        anim.ChangeAnimParamDirectly("is_move bool true");
        transform.DOMove(new Vector3(-8f, -4.5f, 0), 1f);
        transform.localScale = new Vector3(-1, 1, 1);
        anim.ChangeAnimParamDirectly("is_move bool false");
        anim.SetEmoji(1f, 1f);
        yield return new WaitForSeconds(1f);
    }
}
