using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField, Tooltip("Максимальная скорость движения шарика")] private float _maxMovementSpeed = 1;
    [SerializeField, Tooltip("Начальная скорость движения шарика")] private float _defaultMovementSpeed = 1;
    private float _movementSpeed = 1;

    private void OnEnable()
    {
        _movementSpeed = _defaultMovementSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        transform.rotation = Quaternion.LookRotation(newDirection);
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.SetActive(false);
            if (_movementSpeed < _maxMovementSpeed) _movementSpeed++;
        }
    }
}
