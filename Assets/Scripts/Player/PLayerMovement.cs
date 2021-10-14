using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PLayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisuals = null;
    [SerializeField] private BoxCollider2D _groundCollider = null;
    [SerializeField] private Rigidbody2D _rigidbody = null;


    [SerializeField] private float _maxVelocity = 5.0f;
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _jumpPower = 5.0f;

    [SerializeField] private AudioSource _jump = null;

    private float _horizontalInput;
    public bool IsMoving
    {
        get { return Mathf.Abs(_horizontalInput) > 0.1f; }
    }

    private bool _jumpInput;
    public bool IsJumping
    {
        get { return _rigidbody.velocity.y > 0.1f;}
    }
    public bool IsFalling
    {
        get { return _rigidbody.velocity.y < -0.1f; }
    }

    private bool _isStopped = false;
    public bool IsStoped
    {
        set { _isStopped = value; }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isStopped)
        {
            if (IsOnGround())
            {
                _jumpInput |= (Input.GetAxis("Jump") > 0.1f);
                _jumpInput |= (Input.GetAxis("Vertical") > 0.1f);
            }
            else
            {
                _jumpInput = false;
            }

            _horizontalInput = Input.GetAxis("Horizontal");
            //rotate player to move dir
            if (Mathf.Abs(_horizontalInput) > 0.1f)
            {
                Vector3 scale = _playerVisuals.transform.localScale;
                _playerVisuals.transform.localScale =
                    new Vector3(Mathf.Abs(scale.x) * -Mathf.Sign(_horizontalInput), scale.y, scale.z);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isStopped)
        {
            if (Mathf.Abs(_rigidbody.velocity.y) > 0.1f)
            {

                if (Mathf.Abs(_horizontalInput) > 0.1f)
                {
                    _rigidbody.AddForce(new Vector2(_horizontalInput * _moveSpeed, 0), ForceMode2D.Force);
                    if (_horizontalInput < -0.1f)
                    {
                        if (_rigidbody.velocity.x < -_maxVelocity)
                        {
                        _rigidbody.velocity = new Vector2(-_maxVelocity, _rigidbody.velocity.y);
                        }
                    }
                    else
                    {
                        if (_rigidbody.velocity.x > _maxVelocity)
                        {
                        _rigidbody.velocity = new Vector2(_maxVelocity, _rigidbody.velocity.y);
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(_rigidbody.velocity.x) > _maxVelocity/2)
                    {
                        _rigidbody.AddForce(new Vector2(-Mathf.Sign(_rigidbody.velocity.x) * _moveSpeed, 0), ForceMode2D.Force);
                    }

                 //   _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
            }
            else
            {
                if (Mathf.Abs(_horizontalInput) > 0.1f)
                {
                    if (_horizontalInput < -0.1f)
                    {
                        _rigidbody.velocity = new Vector2(-_maxVelocity/2, _rigidbody.velocity.y);
                    }
                    else
                    {
                        _rigidbody.velocity = new Vector2(_maxVelocity/2, _rigidbody.velocity.y);
                    }
                }
            }

            if (_jumpInput)
            {
                _jumpInput = false;
                _rigidbody.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
                //random pitch
                _jump.pitch = Random.Range(0.7f, 1.2f);
                _jump.Play();
            }
        }
    }

    private bool IsOnGround()
    {
        if (_rigidbody.velocity.y > -0.1f && _rigidbody.velocity.y < 0.1f)
        {
            return _groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }
        else
        {
            return false;
        }
       
    }

}
