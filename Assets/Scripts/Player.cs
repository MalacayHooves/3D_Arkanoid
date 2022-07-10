using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("Скорость перемещения платформы игрока")] private float _movementSpeed = 1;
    [SerializeField, Tooltip("Инерция перемещения платформы игрока")] private float _slowDownSpeed = 1;

    private InputActionsAsset _input;
    private InputAction _playerMovement;

    private Coroutine _movementCoroutine;

    private bool _isSlowing = false;

    private bool _isCollidedWithFloor = false;
    private bool _isCollidedWithCeiling = false;
    private bool _isCollidedWithLefWall = false;
    private bool _isCollidedWithRightWall = false;

    private void Awake()
    {
        _input = new InputActionsAsset();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        switch (name)
        {
            case "PlayerOne":
                _input.MainMap.PlayerOneMovement.started += Movement;
                _input.MainMap.PlayerOneMovement.canceled += Movement;
                _playerMovement = _input.MainMap.PlayerOneMovement;
                break;
            case "PlayerTwo":
                _input.MainMap.PlayerTwoMovement.started += Movement;
                _input.MainMap.PlayerTwoMovement.canceled += Movement;
                _playerMovement = _input.MainMap.PlayerTwoMovement;
                break;
            default:
                break;
        }
    }

    private void Movement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_movementCoroutine != null) StopCoroutine(_movementCoroutine);
            _isSlowing = false;
            Vector3 input = _playerMovement.ReadValue<Vector2>();
            _movementCoroutine = StartCoroutine(MovementCoroutine(input, _movementSpeed));
        }
        else if (context.canceled)
        {
            _isSlowing = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Floor") _isCollidedWithFloor = true;
        if (collision.collider.name == "Ceiling") _isCollidedWithCeiling = true;
        if (collision.collider.name == "LefWall") _isCollidedWithLefWall = true;
        if (collision.collider.name == "RightWall") _isCollidedWithRightWall = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Floor") _isCollidedWithFloor = false;
        if (collision.collider.name == "Ceiling") _isCollidedWithCeiling = false;
        if (collision.collider.name == "LefWall") _isCollidedWithLefWall = false;
        if (collision.collider.name == "RightWall") _isCollidedWithRightWall = false;
    }

    private IEnumerator MovementCoroutine(Vector3 input, float movementSpeed)
    {
        Vector3 newPosition = transform.position + movementSpeed * input * Time.deltaTime;
        if (_isCollidedWithFloor) 
        {
            if (newPosition.y < transform.position.y) newPosition.y = transform.position.y;
        }
        if (_isCollidedWithCeiling)
        {
            if (newPosition.y > transform.position.y) newPosition.y = transform.position.y;
        }
        if (_isCollidedWithLefWall)
        {
            if (newPosition.x < transform.position.x) newPosition.x = transform.position.x;
        }
        if (_isCollidedWithRightWall)
        {
            if (newPosition.x > transform.position.x) newPosition.x = transform.position.x;
        }
        transform.position = newPosition;
        yield return null;
        if (_isSlowing)
        {
            movementSpeed -= Time.deltaTime * _slowDownSpeed;
        }

        if (movementSpeed > 0)
        {
            _movementCoroutine = StartCoroutine(MovementCoroutine(input, movementSpeed));
        }
    }
}
