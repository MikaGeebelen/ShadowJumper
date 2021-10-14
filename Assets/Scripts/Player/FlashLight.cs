using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private Vector2 _mousePosition;
    private List<IBasePlatform> _revealedPlatforms = new List<IBasePlatform>();

    void Update()
    {
        //point light to mouse dir
         _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 currentPos = transform.position;

        Vector2 pointToDir = _mousePosition - currentPos;

        float angle = Vector2.Angle(Vector2.up, pointToDir);

        if (_mousePosition.x > currentPos.x)
        {
            angle *= -1;
        }

        transform.rotation = Quaternion.Euler(0,0,angle);

        //check if mousebuttonDown
        if (Input.GetButtonDown("ActivateFlash"))
        {
            foreach (IBasePlatform platforms in _revealedPlatforms)
            {
                platforms.Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "GhostPlatform")
        {
            _revealedPlatforms.Add(col.GetComponent<IBasePlatform>());
            _revealedPlatforms[_revealedPlatforms.Count - 1].Reveal();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "GhostPlatform")
        {
            IBasePlatform platform = col.GetComponent<IBasePlatform>();
            platform.Hide();
            _revealedPlatforms.Remove(platform);
        }
    }
}
