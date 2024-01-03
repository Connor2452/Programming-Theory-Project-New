using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    [SerializeField] protected float movementSpeed = 30.0f;
    protected GameObject player;
    private Vector3 movementDirection;

    void Start()
    {

    }

    protected virtual void MoveEnemy()
    {
        if (player != null)
        {
            movementDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(movementDirection * Time.deltaTime * movementSpeed);
        }

    }
}
