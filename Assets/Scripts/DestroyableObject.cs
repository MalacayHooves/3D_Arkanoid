using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    private Light _light;
    private MeshRenderer _meshRenderer;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        _light = GetComponent<Light>();
        _meshRenderer = GetComponent<MeshRenderer>();
        Color color = GetColor(Random.Range(1, 5));
        _light.color = color;
        _meshRenderer.material.color = color;
    }

    private void OnDisable()
    {
        _gameManager.RemoveFromDestroyableObjectsList(gameObject);
    }

    private Color GetColor(int number)
    {
        Color color = Color.white;
        switch (number)
        {
            case 1: 
                color = Color.blue;
                break;
            case 2:
                color = Color.red;
                break;
            case 3:
                color = Color.green;
                break;
            case 4:
                color = Color.yellow;
                break;
            case 5:
                color = Color.cyan;
                break;
            default:
                break;
        }
        return color;
    }
}
