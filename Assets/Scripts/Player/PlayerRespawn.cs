using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject _deathParticle = null;
    [SerializeField] private PLayerMovement _movement = null;
    public void Respawn()
    {
        StartCoroutine(EndGame());
    }

    //reveal spike when player dies
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GhostPlatform"))
        {
            col.GetComponent<IBasePlatform>().Reveal();
        }
    }

    IEnumerator EndGame()
    {
        _movement.IsStoped = true;
        Instantiate(_deathParticle, transform.position,Quaternion.identity);
        FindObjectOfType<CameraBehaviour>().MoveSpeed = 0.0f;
        yield return new WaitForSecondsRealtime(2.0f); 
        SceneManager.LoadScene(2);
    }
}
