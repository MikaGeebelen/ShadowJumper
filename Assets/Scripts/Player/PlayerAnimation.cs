using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PLayerMovement _movement = null;
    [SerializeField] private Animator _animator = null;

    void Update()
    {
        _animator.SetBool("IsJumping", _movement.IsJumping);
        _animator.SetBool("IsFalling", _movement.IsFalling);
        _animator.SetBool("IsMoving", _movement.IsMoving);
    }
}
