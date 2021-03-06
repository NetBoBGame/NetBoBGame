﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviourPun
{
    private int health = 3;
    private float h = 0f;
    private float v = 0f;
    private float r = 0f;

    private Animator animator;
    private Transform tr;
    private Rigidbody rigidbody;
    
    private float moveSpeed = 5f;
    public float rotSpeed = 50f;
    public float jumpPower = 30f;

    private bool runSwitch;
    private bool jumpSwitch;

    private Vector3 movement;

    
    private int[] playerScores;
    private Camera m_Camera;
    
    private bool AttackSwitch;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        rigidbody  = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        
        
        if(photonView.IsMine)
        {
            
            //this.name  = "Player1";
        }
        else{
            
            //this.name = "Player2";
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }
        runSwitch = false;
        
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        if(Input.GetButtonDown("Jump"))
        {
            //if jump is false  , I can jump
            if(!jumpSwitch)
            {
                Jump();
                StartCoroutine("WaitForJump");
            }
        }
        if(h != 0 || v !=0)
        {
            runSwitch = true;
            Move(h,v);
        }
        else{
            runSwitch = false;
            animator.SetBool("run",false);
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Attack1();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Attack2();
        }
        if(Input.GetButtonDown("Fire3"))
        {
            Attack3();
        }
        Dead();
    }
    private void Move(float h, float v) {
        animator.SetBool("run", true);
        movement.Set (h,0,v );
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition (transform.position + movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    }
    private void Jump()
    {
        if(jumpSwitch)
        {
            return;
        }
        animator.SetTrigger("jump");
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        jumpSwitch  = true;

    }
    
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.name == "Terrain")
        {
            animator.SetTrigger("fly");
        }
    }
    private void OnCollisionEnter(Collision other) {
        // if(other.gameObject.name == "Terrain")
        // {
        //     animator.SetTrigger("land");
        // }
        if(other.gameObject.name == "DeathPlane")
        {
            Debug.Log("Death");
        }
    
    }
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(1f);
        jumpSwitch = false;
    }
    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.3f);
        AttackSwitch = false;
    }
    public void Attack1()
    {
        AttackSwitch = true;
        Debug.Log("click Z attack 1");
        animator.SetTrigger("attack1");
        StartCoroutine("WaitForAttack");
    }   
    public void Attack2()
    {
        AttackSwitch = true;
        Debug.Log("click X attack 2");
        animator.SetTrigger("attack2");
        StartCoroutine("WaitForAttack");
    }
    public void Attack3()
    {
        AttackSwitch = true;
        Debug.Log("click C pilsal 3");
        animator.SetTrigger("attack3");
        StartCoroutine("WaitForAttack");
    }

    public void Dead() {
        if(health <= 0)
        {
            Debug.Log("Add Dead Activation");
            animator.SetTrigger("dead");
        }
    }
}
