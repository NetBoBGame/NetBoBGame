using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float h = 0f;
    private float v = 0f;
    private float r = 0f;

    private Animator animator;
    private Transform tr;
    private Rigidbody rigidbody;
    
    private float moveSpeed = 5f;
    public float rotSpeed = 50f;
    public float jumpPower = 10f;

    private bool runSwitch;
    private bool jumpSwitch;

    private Vector3 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody  = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        runSwitch = false;
        
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        if(Input.GetButtonDown("Jump"))
        {
            jumpSwitch = true;
            Jump();
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
        if(!jumpSwitch)
        {
            return;
        }
        animator.SetBool("jump",true);
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        
        jumpSwitch = false;
    }
    
}
