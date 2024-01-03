using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    private Vector3 flightDirection;
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("player").GetComponent<Rigidbody>();
        flightDirection = (playerRb.transform.position - transform.position).normalized;
        flightDirection = new Vector3(flightDirection.x, 0, -flightDirection.y);
    }

    private void FixedUpdate()
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
        if (collision.gameObject.CompareTag("player"))
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
