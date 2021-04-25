using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health;
    private bool isLive = true;
    [SerializeField]
    private Color hurtColor;
    [SerializeField]
    private GameObject dieEffect;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool TakeDammage(float _damage)
    {
        if(!isLive)return false;
    	this.health -= _damage;
        if(this.health <= 0)
        {
            isLive = false;
            this.Die();
        }
        else
        {
            StartCoroutine(Flash());
        }

        return isLive;
    }

    IEnumerator Flash()
    {
        spriteRenderer.color = hurtColor;
        StartCoroutine(CameraShake.Instance.Shake(0.5f, 0.5f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        spriteRenderer.enabled = false;
        GameObject gfx = Instantiate(dieEffect, transform.position, Quaternion.identity);
        StartCoroutine(CameraShake.Instance.Shake(0.5f, 0.5f));
        Destroy(gfx, 5f);
        Destroy(gameObject);
    }


}
