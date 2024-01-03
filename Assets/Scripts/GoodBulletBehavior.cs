using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBulletBehavior : MonoBehaviour
{
    [SerializeField] protected float speed = 40.0f;
    protected Vector3 flightDirectionBullet;

    private void FixedUpdate()
    {
        MoveBullet();
    }

    protected void MoveBullet()
    {
        transform.Translate(flightDirectionBullet * Time.deltaTime * speed);
    }

    protected void OnCollisionEnter(Collision collision)
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

    public void Setup(Vector3 flightDirection)
    {
        flightDirectionBullet = flightDirection;
    }

}
