using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private bool _requiresFall = true;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerRespawn respawn;
            if (col.TryGetComponent<PlayerRespawn>(out respawn))
            {

                if (_requiresFall)
                {
                    if (col.GetComponent<Rigidbody2D>().velocity.y < -0.1f)
                    {
                        respawn.Respawn();
                    }
                }
                else
                {
                    respawn.Respawn();
                }
            }
        }
    }
}
