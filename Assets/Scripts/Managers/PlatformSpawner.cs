using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private List<GameObject> _platformPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _platformTrapPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _platformSpecialPrefabs = new List<GameObject>();

    [SerializeField] private ScoreTracker _scoreTracker;
    [SerializeField] private BackGroundSpawner _backGroundSpawner;

    [SerializeField] private float _heightPerLayer = 3.0f;

    [SerializeField] private List<Transform> _xTransforms = new List<Transform>();

    private int _currentLayer = 0;
    private int _backGroundLayer = 0;
    public float CurrentHeightPerLayer
    {
        get { return _heightPerLayer * _currentLayer; }
    }

    private int _minPlatforms = 3;
    private int _maxPlatforms = 5;

    private int _platformType = 0;
    private int _MaxplatformType = 1;

    private bool _activateScore = false;

    private void Start()
    {
        _MaxplatformType = _platformPrefabs.Count;
        //sawn base 3 layers
        for (int i = 0; i < 4; i++)
        {
            _backGroundSpawner.SpawnBackgroundLayer(_currentLayer * 1.9f + 1.02f);
            _backGroundLayer++;
            SpawnLayer();
        }
    }

    private void Update()
    {
        if (_player)
        {
            float playerYPos = _player.transform.position.y;

            //spawn platforms layers to fill screen
            if (playerYPos > _heightPerLayer * _currentLayer - 10)
            {
                SpawnLayer();
            }

            if (playerYPos > _backGroundLayer * 1.9f + 1.02f -10.0f)
            {
                //start at 1.02f makes layers fit better
                _backGroundSpawner.SpawnBackgroundLayer(_backGroundLayer * 1.9f + 1.02f);
                _backGroundLayer++;
            }

            if (!_activateScore)
            {
                _scoreTracker.ResetScore();
                PLayerMovement movement = FindObjectOfType<PLayerMovement>();
                if (movement.IsMoving || movement.IsJumping)
                {
                    _activateScore = true;
                }
            }

        }
    }

    private void SpawnLayer()
    {
        _scoreTracker.IncreaseScore(50);
        List<GameObject> platformPrefabs = new List<GameObject>();
        platformPrefabs.AddRange(_platformPrefabs);

        List<Transform> xPlatformLocs = new List<Transform>();
        xPlatformLocs.AddRange(_xTransforms);

        int platformAmount = Random.Range(_minPlatforms, _maxPlatforms+1);
        int spawnedPlatforms = 0;
        _currentLayer++;
        while (platformAmount >= 1)
        {
            shuffle(ref xPlatformLocs);
            for (int i = 0; i < xPlatformLocs.Count; i++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    int index = Random.Range(_platformType, platformPrefabs.Count);
                    Instantiate(platformPrefabs[index], new Vector3(xPlatformLocs[i].position.x, _currentLayer * _heightPerLayer, 0), Quaternion.identity);
                    platformAmount--;
                    spawnedPlatforms++;
                    if (index != 0)
                    {
                        platformPrefabs.RemoveAt(index);
                    }

                    xPlatformLocs.Remove(xPlatformLocs[i]);

                    if (spawnedPlatforms == 2)
                    {
                        platformPrefabs.AddRange(_platformTrapPrefabs);
                    }
                    else if (spawnedPlatforms == 3)
                    {
                        platformPrefabs.AddRange(_platformSpecialPrefabs);
                    }
                }
            }
        }
    }

    private void shuffle(ref List<Transform> xLocations)
    {
        for (int i = 0; i < xLocations.Count; i++)
        {
            Transform temp = xLocations[i];
            int randomIndex = Random.Range(i, xLocations.Count);
            xLocations[i] = xLocations[randomIndex];
            xLocations[randomIndex] = temp;
        }
    }
}
