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
    [SerializeField] Animator _playerAnim;

    void Awake()
    {
        _playerAnim = GetComponentInChildren<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0, 1f, 0);
        //_playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerAnim.SetBool("Walk",false);
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // if this, it means the user is pressing the moving button
        if (moveX != 0 || moveY != 0)
        {
            _playerAnim.SetBool("Walk", true); 
        }
        else
        {
            _playerAnim.SetBool("Walk", false);
        }

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
            _playerAnim.SetBool("Walk",true);
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
