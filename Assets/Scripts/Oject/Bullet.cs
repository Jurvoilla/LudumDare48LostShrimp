using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 10f;
    public float inscreaseSpeed = 1f;
	private float timeVie = 10f;


	private Player player;
	private float timePasse = 0;
	private float damage;
	private bool canDamage = true;

    private ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;

    
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        this.move();
        this.death();
    }

    public void move()
    {
    	transform.position += transform.up * Time.deltaTime * speed;
        speed -= inscreaseSpeed * Time.deltaTime;
    }

    public void death()
    {
    	timePasse += Time.deltaTime;
    	if(timePasse >= timeVie || speed <= 0f)
    	{
            particleSystem.Play();
    		Destroy(gameObject, 1f);
            spriteRenderer.enabled = false;
            this.enabled = false;
    	}
    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
    	if(collision.CompareTag("Enemy"))
    	{
            if(canDamage)
            {
                canDamage = false;
                Target target = collision.transform.GetComponent<Target>();
                bool isDeath = !target.TakeDammage(this.damage);
                if(isDeath)
                {
                    //player.SetKill();
                }
                Destroy(gameObject);
            }
    	}
    }

    public void setParametre(float _damage, Player _player)
    {
    	this.damage = _damage;
		player = _player;
    }
}
