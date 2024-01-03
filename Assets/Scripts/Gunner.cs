using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Enemy
{
    private Rigidbody playerRb;
    private Rigidbody gunnerRb;
    private Vector3 distanceDiffComps;
    private float distanceDiff;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingStartDelay = 2.0f;
    [SerializeField] private float reloadDealy = 1.0f;

    [SerializeField] private float minDistance = 10.0f;
    [SerializeField] private float maxDistance = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("player").GetComponent<Rigidbody>();
        gunnerRb = GetComponent<Rigidbody>();
        InvokeRepeating("ShootBullet", shootingStartDelay, reloadDealy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
    }


    // INHERITANCE
    protected override void MoveEnemy()
    {
        if (playerRb != null)
        {
            distanceDiffComps = transform.position - playerRb.transform.position;
            distanceDiff = distanceDiffComps.magnitude;

            if (distanceDiff < minDistance)
            {
                gunnerRb.velocity = distanceDiffComps.normalized * movementSpeed;
            }
            if (distanceDiff > minDistance && distanceDiff < maxDistance)
            {
                gunnerRb.velocity = Vector3.zero;
            }
            if (distanceDiff > maxDistance)
            {
                gunnerRb.velocity = -distanceDiffComps.normalized * movementSpeed;
            }
        }

    }

    private void ShootBullet()
    {
        if (playerRb != null)
        {
            Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        }
        
    }
}
