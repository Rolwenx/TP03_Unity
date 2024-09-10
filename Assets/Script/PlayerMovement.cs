using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 2f;
    public float runSpeed = 7f;
    public float jumpForce = 2f;
    Vector3 movement;
    Vector3 jump;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] Animator _playerAnim;
    
    private bool isGrounded;
    private bool wasGrounded;
    private bool isJumping;

    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnim = GetComponentInChildren<Animator>();
        jump = new Vector3(0, 1f, 0);
        isGrounded = true;
        wasGrounded = true;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

    
        if(isJumping){
            wasGrounded = true;
        }
        else{
            wasGrounded = false;
        }
        isGrounded = IsGrounded();

        _playerAnim.SetBool("IsGrounded", isGrounded);

    
        if (wasGrounded && !isGrounded)
        {
            Debug.Log("hi");
            _playerAnim.SetBool("IsFalling", true);
        }
        else
        {
            _playerAnim.SetBool("IsFalling", false);
        }

    
        // if this, it means the user is pressing the moving button
        if (moveX != 0 || moveY != 0)
        {
            _playerAnim.SetBool("IsWalking", true);

        }
        else
        {
            _playerAnim.SetBool("IsWalking", false);

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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
            _playerAnim.SetBool("IsJumping", true);
            isJumping = true;
        }
        else{
            _playerAnim.SetBool("IsJumping", false);
        }

    }

    void Jump(){

        _rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        _playerAnim.SetBool("IsGrounded", true);
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
