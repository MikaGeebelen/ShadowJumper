using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlatform : MonoBehaviour, IBasePlatform
{
    [SerializeField] private Transform _telportLocation;
    [SerializeField] private GameObject _particle;
    [SerializeField] private AudioSource _teleportSound;
    private GameObject _player = null;
    public void Reveal()
    {
    }

    public void Hide()
    {
    }

    public void Activate()
    {
        Rigidbody2D rigidbody = FindObjectOfType<Rigidbody2D>();
        rigidbody.velocity = new Vector2();
        _player = rigidbody.gameObject;
        _teleportSound.pitch = Random.Range(0.3f, 0.4f);
        _teleportSound.Play();
        Instantiate(_particle, _player.transform.position, Quaternion.identity);
        Instantiate(_particle, _telportLocation.position, Quaternion.identity);
        _player.transform.position = _telportLocation.position;
    }
}
