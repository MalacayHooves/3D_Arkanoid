using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Ball")
        {
            _gameManager.BallHittedGates();
        }
    }
}
