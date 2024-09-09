using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 6f;
    Vector3 movement;
    Vector3 jump;
     public float jumpForce = 2f;

    [SerializeField] private Rigidbody _rigidbody;

    private bool _isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0, 2.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0f, moveY) * walkSpeed * Time.deltaTime;

        transform.position += movement;

        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            Jump();
        }


    }

    void Jump(){

       
        _rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return _rigidbody.velocity.y == 0;
    }
}
