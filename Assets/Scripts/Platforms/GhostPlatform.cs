using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour, IBasePlatform
{
    [SerializeField] private ParticleSystem _particle = null;
    [SerializeField] private ParticleSystem _deathParticle = null;

    [SerializeField] private GameObject _visuals = null;

    [SerializeField] private float _lifeTime = 5.0f;

    private bool _isrevealed = false;

    private bool _isBeingDestroyed = false;
    // Update is called once per frame
    void Update()
    {
        if (_isrevealed)
        {
            if (_isBeingDestroyed && !_deathParticle.isPlaying)
            {
                _deathParticle.Play();
            }
            if (_particle.isStopped)
            {
                _particle.Play();
            }
        }
        else
        {
            if (_particle.isPlaying)
            {
                _particle.Stop(false,ParticleSystemStopBehavior.StopEmitting);
            }
            if (_isBeingDestroyed)
            {
                _deathParticle.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }


    }

    public void Reveal()
    {
        _isrevealed = true;
    }

    public void Hide()
    {
        _isrevealed = false;
    }

    public void Activate()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if ((col.gameObject.transform.position.y > transform.position.y))
            {
                _isrevealed = true;
                _isBeingDestroyed = true;
                StartCoroutine(DestroyPlatform());
            }
        }
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(_visuals);
        _isrevealed = false;
        yield return new WaitForSeconds(_lifeTime);
        Destroy(this);
    }
}
