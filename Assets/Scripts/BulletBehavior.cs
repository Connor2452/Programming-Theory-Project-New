using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    private Vector3 flightDirection;

    public void Setup(Vector3 mousePosition, Vector3 playerPosition)
    {
        flightDirection = (mousePosition - playerPosition).normalized;
        flightDirection = new Vector3(flightDirection.x, 0, -flightDirection.y);
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.Translate(flightDirection * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy destroy both this will later jsut reduce health
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        // Destroys the bullet when it hits a wall
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
