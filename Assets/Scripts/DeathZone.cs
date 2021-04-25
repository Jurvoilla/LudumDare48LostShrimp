using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D collision)
    {
    	if(collision.CompareTag("Enemy"))
    	{
            Destroy(collision.gameObject);
    	}
    }
    public void OnCollisionEnter2D (Collision2D collision)
    {
    	if(collision.gameObject.CompareTag("Player"))
    	{
            Player target = collision.transform.GetComponent<Player>();
            target.TakeDamage(1000f);
    	}
    }
}
