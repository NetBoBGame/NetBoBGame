using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float h = 0f;
    private float v = 0f;
    private float r = 0f;

    private Transform tr;
    
    private float moveSpeed = 5f;
    public float rotSpeed = 50f;

    private bool runSwitch;
    private bool jumpSwitch;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        runSwitch = false;
        
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        if(h != 0 || v !=0)
        {
            runSwitch = true;
        }
        else{
            runSwitch = false;
            animator.SetBool("run",false);
        }
        if(runSwitch)
        {
            animator.SetBool("run", true);
            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right *h);
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
            tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * h);
        }
       
    }
}
