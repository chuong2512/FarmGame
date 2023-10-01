using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    [SerializeField] private PassingCar passingCarMoneyPrefabs;
    [SerializeField] private PassingCar passingCarTruckPrefabs;

    [SerializeField] private float minTimeSpawn, rangeTimeSpawn;

    private PassingCar _carSpawn;
    private CarState _carState, _currentCarState;
    private bool _isBlockSpawn;
    private float _timeSpawn;
    
    private void Start()
    {
        ManagerCar.instance.OnCarGo += SetMainRoad;
        _carState = CarState.Empty;
        _currentCarState = _carState;
        _carSpawn = passingCarMoneyPrefabs;
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

    private void SetMainRoad(CarState carState)
    {
        _currentCarState = _carState;
        _carState = carState;
        _carSpawn = carState switch
        {
            CarState.Money => passingCarMoneyPrefabs,
            CarState.Truck => passingCarTruckPrefabs,
            _ => passingCarMoneyPrefabs
        };
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
        var passingCar = Instantiate(_carSpawn);
    }

    private float GetRandomTime() => Random.Range(minTimeSpawn, minTimeSpawn + rangeTimeSpawn);

}