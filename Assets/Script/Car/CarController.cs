using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    [SerializeField] private PassingCar[] leftPassingCarPrefabs;
    [SerializeField] private PassingCar[] rightPassingCarPrefabs;

    [SerializeField] private float minTimeSpawn, rangeTimeSpawn;

    private PassingCar _carSpawn;
    private bool _isBlockSpawn;
    private float _timeSpawn;
    
    private void Start()
    {
        ManagerCar.instance.OnCarGo += BlockCar;
        _timeSpawn = minTimeSpawn;
        _isBlockSpawn = false;
    }

    private void FixedUpdate()
    {
        if (_timeSpawn > 0)
        {
            _timeSpawn -= Time.fixedDeltaTime;
            return;
        }
        
        if (_isBlockSpawn) return;
        _timeSpawn = GetRandomTime();
        SpawnCar();
    }

    private void BlockCar()
    {
        StartCoroutine(IEBlockSpawn());
    }

    private IEnumerator IEBlockSpawn()
    {
        _isBlockSpawn = true;
        yield return new WaitForSeconds(8f);
        _isBlockSpawn = false;
    }

    private void SpawnCar()
    {
        _carSpawn = Random.Range(0, 10) > 5 ? GetRandomRightCar() : GetRandomLeftCar();

        PoolingSystem.Instance.InstantiateAPS(_carSpawn.name);
    }

    private float GetRandomTime() => Random.Range(minTimeSpawn, minTimeSpawn + rangeTimeSpawn);

    private PassingCar GetRandomLeftCar()
    { 
        var carIndex = Random.Range(0, leftPassingCarPrefabs.Length);
        return leftPassingCarPrefabs[carIndex];
    }
    
    private PassingCar GetRandomRightCar()
    { 
        var carIndex = Random.Range(0, rightPassingCarPrefabs.Length);
        return rightPassingCarPrefabs[carIndex];
    }
}