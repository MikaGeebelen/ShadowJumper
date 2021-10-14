using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    [SerializeField] private float _moveSpeed = 2.0f;

    [SerializeField] private GameObject _cleanup = null;

    private bool _HasStarted = false;

    public float MoveSpeed
    {
        set { _moveSpeed = value; }
    }

    private void FixedUpdate()
    {
        if (!_HasStarted)
        {
            Vector2 pos = transform.position;
            Vector2 moveDir = new Vector2(0, _player.transform.position.y) - pos;
            _HasStarted |= (moveDir.y > 1.0f);
            if (_HasStarted)
            {
                _cleanup.transform.Translate(new Vector3(0,5));
            }
        }
        else
        {
           
            transform.Translate(0, _moveSpeed * Time.fixedDeltaTime , 0);
        }
    }
}
