using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private Quaternion lastLook;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        anim = GetComponent<Animator>();
        lastLook = transform.rotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(horizontal, 0, vertical).normalized;
        
        if (movementVector.magnitude != 0)
        {
            lastLook = Quaternion.LookRotation(movementVector);
        }
        transform.rotation = lastLook;

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed / 100;
        rb.MovePosition(transform.position + movement);

        anim.SetFloat("horizontalVector", movementVector.magnitude);
        anim.SetFloat("verticalVector", 0);
    }

}
