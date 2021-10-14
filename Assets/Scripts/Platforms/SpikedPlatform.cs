using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedPlatform : MonoBehaviour , IBasePlatform
{
    [SerializeField] private GameObject _spikeVisuals;
    [SerializeField] private ParticleSystem _particle;
    public void Reveal()
    {
        _spikeVisuals.SetActive(true);
        _particle.Play();
    }

    public void Hide()
    {
        //_spikeVisuals.SetActive(false);
    }

    public void Activate()
    {
    
    }
}
