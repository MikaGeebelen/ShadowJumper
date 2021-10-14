using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _backGroundPefab = null;

    public float XPos
    {
        //center position of map for prefab
        get { return -7.6f; }
    }
    public float YOffset
    {
        //height difference between each layer for continious back wall
        get { return 1.9f; }
    }


    public void SpawnBackgroundLayer(float layerHeight)
    {
        Instantiate(_backGroundPefab, new Vector3(-7.6f, layerHeight),Quaternion.identity);
    }
}
