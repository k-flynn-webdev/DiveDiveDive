using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _jump = 3f;
    [SerializeField]
    public float gravity = 20.0f;


    private Vector3 _moveSpeed;
    private bool _allowInput = true;

    private bool _isGrounded = false;
    private bool _isJumping = false;
    private bool _IsFalling = false;

    private CharacterController _characterController;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;


        if (_allowInput && _isGrounded)
        {
            if(Input.GetAxisRaw("Horizontal") < 0f)
            {
                Left();
            }
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                Right();
            }
            if (Input.GetAxisRaw("Vertical") > 0f)
            {
                Jump();
            }
        }

        if (_allowInput && Input.GetAxisRaw("Vertical") < 0f)
        {
            Down();
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveSpeed.y -= gravity * Time.deltaTime;

        // Move the controller
        _characterController.Move(_moveSpeed * Time.deltaTime);
    }



    private void Left() {
        _moveSpeed = new Vector3(_speed * -1f, 0f, 0f);
    } 
    private void Right() {
        _moveSpeed = new Vector3(_speed * 1f, 0f, 0f);
    }
    private void Jump() {
        _moveSpeed = new Vector3(0f, _jump * _speed, 0f);
    }
    private void Down() {
        _moveSpeed = new Vector3(0f, (_jump * _speed) * -1f, 0f);
    }

}
