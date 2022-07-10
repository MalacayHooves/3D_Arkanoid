using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablesSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Список разрушаемых объектов")] private List<GameObject> _destroyableObjectsList = null;
    [SerializeField, Tooltip("Префаб разрушаемого объекта")] private GameObject _destroyableObject = null;
    [SerializeField, Range(1, 50), Tooltip("Количество спаунящихся объектов")] private int _numberOfObjects = 1;
    [SerializeField, Range(1, 10), Tooltip("Радиус спауна")] private float _spawnRadius = 1;
    [SerializeField, Range(1, 10), Tooltip("Минимальное расстояние между объектами при спауне")] private float _spawnCollisionCheckRadius = 1;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        GameObject spawnedObject;
        Vector3 spawnCentre = new Vector3(0, 0, 15);
        Vector3 spawnPoint;
        while (_destroyableObjectsList.Count < _numberOfObjects)
        {
            spawnPoint = spawnCentre + Random.insideUnitSphere * _spawnRadius;
            if (!Physics.CheckSphere(spawnPoint, _spawnCollisionCheckRadius))
            {
                spawnedObject = Instantiate(_destroyableObject, spawnPoint, Quaternion.identity, gameObject.transform);
                _destroyableObjectsList.Add(spawnedObject);
            }
        }
        _gameManager.SetNewDestroyableObjectsList(_destroyableObjectsList);
    }
}
