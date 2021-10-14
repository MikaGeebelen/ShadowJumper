using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FakePlatform : MonoBehaviour,IBasePlatform
{
    [SerializeField]private SpriteRenderer _Platform;
    private float _opacity = 1.0f;
    private bool _isRevealed = false;
    private void Update()
    {
        if (_isRevealed && _opacity > 0.0f)
        {
            _opacity -= Time.deltaTime;
            Color startingColor = _Platform.color;
            _Platform.color = new Color(startingColor.r, startingColor.g, startingColor.b, _opacity);
        }
    }

    public void Reveal()
    {
        _isRevealed = true;
    }

    public void Hide()
    {
    }

    public void Activate()
    {
    }
}
