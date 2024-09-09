using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 2f;
    public float runSpeed = 12f;
    public float jumpForce = 2f;
    Vector3 movement;
    Vector3 jump;

    [SerializeField] private Rigidbody _rigidbody;

    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0, 1f, 0);
    }

    // Update is called once per frame
    void Update()
    {


        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (IsRunning() && IsGrounded())
        {
            movement = new Vector3(moveX, 0f, moveY) * runSpeed * Time.deltaTime;
        }
        else{
            movement = new Vector3(moveX, 0f, moveY) * walkSpeed * Time.deltaTime;
        }

        // we take current position of the player + add the movement
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

    bool IsRunning()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            return true;
        }
        else{
            return false;
        }
    }
}
