using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharMove : MonoBehaviour, IReset
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _jump = 3f;
    [SerializeField]
    private float _jumpLength = 1f;
    [SerializeField]
    private float _jumpCoolDown = 1.5f;
    [SerializeField]
    private float _gravity = -10f;

    [SerializeField]
    private AnimationCurve _gravityCurve;

    private Vector3 _moveSpeed;

    private bool _allowInput = true;


    [SerializeField]
    private bool IsJumping
    {
        get { return _isJumping; }
        set { _isJumping = value; _jumpTime = 0f; }
    }
    [SerializeField]
    private bool IsFalling
    {
        get { return _isFalling; }
        set { _isFalling = value; _fallTime = 0f; }
    }
    [SerializeField]
    private bool IsMoving
    {
        get { return _isMoving; }
        set { _isMoving = value; _moveTime = 0f; }
    }
    [SerializeField]
    private bool IsGrounded
    {
        get { return _isGrounded; }
        set { _isGrounded = value; _groundTime = 0f; }
    }

    private float dirType = 0f;
    private float decelType = 20f;
    private float _jumpCoolDownTime;

    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isMoving = false;
    private bool _isGrounded = false;

    private float _jumpTime;
    private float _fallTime;
    private float _moveTime;
    private float _groundTime;



    private CharacterController _characterController;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Reset();
        //if (_characterController.isGrounded)
        //{
        //    Ground();
        //}
        //else
        //{
        //    Fall();
        //}
    }

    private void Update()
    {

        if (_allowInput)
        {

            if (Input.GetAxisRaw("Vertical") < 0f)
            {
                Down();
            }
            if (Input.GetAxisRaw("Vertical") > 0f)
            {
                Jump();
            }

            if (IsGrounded)
            {
                if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    Left();
                }
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    Right();
                }
            }
        }


        if (IsGrounded != _characterController.isGrounded)
        {
            if (_characterController.isGrounded)
            {
                Ground();
            }
            else
            {
                Fall();
            }
        }

        JumpUpdate();
        FallUpdate();
        GroundUpdate();
        SideUpdate();

        _characterController.Move(_moveSpeed * Time.deltaTime);
    }




    private void Jump()
    {
        if (IsJumping || _jumpCoolDownTime > 0f)
        {
            return;
        }

        IsJumping = true;
        IsGrounded = false;

        _jumpCoolDownTime = _jumpCoolDown;

        _moveSpeed = new Vector3(0f, _jump * _jump, 0f);
    }

    private void JumpUpdate()
    {
        if (_jumpCoolDownTime > 0f)
        {
            _jumpCoolDownTime -= Time.deltaTime;
        }

        if (!IsJumping)
        {
            return;
        }

        _jumpTime += Time.deltaTime;

        float normal = (_jumpTime / _jumpLength);
        float normalPower = normal * normal;

        float jumpPower = (_jump * _jump) * (1f - normalPower);

        _moveSpeed = new Vector3(0f, jumpPower, 0f);

        if (_jumpTime > _jumpLength)
        {
            IsJumping = false;

            Fall();
        }
    }


    private void Fall()
    {
        if (IsJumping)
        {
            return;
        }

        IsFalling = true;
        IsMoving = false;
        IsGrounded = false;
    }

    private void FallUpdate()
    {
        if (!IsFalling)
        {
            return;
        }

        _fallTime += Time.deltaTime;

        float gravityPower = _gravityCurve.Evaluate(_fallTime) * _gravity;

        Vector3 _gravityTmp =
            new Vector3(0f, gravityPower, 0f) * Time.deltaTime;

        _moveSpeed = _moveSpeed + _gravityTmp;
    }


    private void Ground()
    {
        if (IsJumping)
        {
            return;
        }

        IsFalling = false;
        IsGrounded = true;

        GroundUpdate();
    }

    private void GroundUpdate()
    {
        if (!IsGrounded)
        {
            return;
        }

        _groundTime += Time.deltaTime;
        float downPress = -0.1f * Time.deltaTime;
        downPress += _moveSpeed.y;
        if (downPress < _gravity)
        {
            downPress = _gravity;
        }

        _moveSpeed = new Vector3(_moveSpeed.x, downPress, _moveSpeed.z);
    }


    private void Left() {
        IsMoving = true;
        dirType = -1f;
    } 
    private void Right() {
        IsMoving = true;
        dirType = 1f;
    }


    private void SideUpdate()
    {
        if (IsFalling)
        {
            SideAccel(0f, 1f);
            return;
        }

        if (!IsMoving)
        {
            SideAccel(0f, decelType);
            return;
        }

        _moveTime += Time.deltaTime;
        SideAccel(dirType, decelType);
    }

    private void SideAccel(float dir, float decelOverride)
    {
        float moveX = Mathf.Lerp(_moveSpeed.x, _speed * dir, Time.deltaTime * decelOverride);
        _moveSpeed = new Vector3(moveX, _moveSpeed.y, _moveSpeed.z);
    }

    private void Down() {
        IsMoving = false;
        _moveSpeed = new Vector3(_moveSpeed.x, (_jump * _jump) * -1f, _moveSpeed.z);
    }

    public void Reset()
    {
        IsJumping = false;
        IsFalling = true;
        IsMoving = false;
        IsGrounded = false;

        dirType = 0f;
        _jumpCoolDownTime = 0f;

        _moveSpeed = Vector3.zero;
    }
}
