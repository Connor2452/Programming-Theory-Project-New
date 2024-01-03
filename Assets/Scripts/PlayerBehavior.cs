using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private GameObject bullet;
    private Rigidbody playerRb;

    private Vector3 maxWantedVelocityX;
    private Vector3 maxWantedVelocityY;
    private Vector3 flightDirection;
    private Vector3 bulletSpawnLocation;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("player").GetComponent<Rigidbody>();
        maxWantedVelocityX = new Vector3(movementSpeed, 0, 0);
        maxWantedVelocityY = new Vector3(0, movementSpeed, 0);

    }

    private void Update()
    {
        FireBullet();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    // ABSTRACTION
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (maxWantedVelocityX.x > playerRb.velocity.x && horizontalInput > 0)
        {
            playerRb.AddForce(maxWantedVelocityX, ForceMode.VelocityChange);
        }
        if (maxWantedVelocityY.y > playerRb.velocity.y && verticalInput > 0)
        {
            playerRb.AddForce(maxWantedVelocityY, ForceMode.VelocityChange);
        }
        if (-maxWantedVelocityX.x < playerRb.velocity.x && horizontalInput < 0)
        {
            playerRb.AddForce(-maxWantedVelocityX, ForceMode.VelocityChange);
        }
        if (-maxWantedVelocityY.y < playerRb.velocity.y && verticalInput < 0)
        {
            playerRb.AddForce(-maxWantedVelocityY, ForceMode.VelocityChange);
        }
        if (horizontalInput == 0)
        {
            playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
        }
        if (verticalInput == 0)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, 0);
        }
    }

    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 trueMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 49.0f);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(trueMousePosition);

            // Calculating the flight direction of the bullet before instantiating it
            flightDirection = (mousePosition - transform.position).normalized;
            
            // Calculating the creation position of the bullet
            bulletSpawnLocation = transform.position + (flightDirection * 2);

            // Making sure it flies in the right direction, need to figure out how to use global coords for this to not be like this
            flightDirection = new Vector3(flightDirection.x, 0, -flightDirection.y);

            // Instantiate the bullet and feed it its flight direction
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnLocation, bullet.transform.rotation);
            bulletInstance.GetComponent<GoodBulletBehavior>().Setup(flightDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            playerRb.velocity = Vector3.zero;
        }
    }
}
