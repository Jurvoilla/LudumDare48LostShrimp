using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    internal float speedMovement = 4f;
    [SerializeField]
    protected float damage = 50f;


    protected Animator animator;
    protected Transform playerTransform;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsMove", true);

        if(GameObject.FindWithTag("Player") != null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        else
        {
            playerTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        Move();    
    }


    virtual protected void Move()
    {
        transform.Translate(transform.up * speedMovement * Time.fixedDeltaTime, Space.World);
    }



    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }    
    }

    
}
