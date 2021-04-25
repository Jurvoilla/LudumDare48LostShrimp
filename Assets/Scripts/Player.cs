using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float speedMovement = 5f;
    [SerializeField]
    private float currentHealth = 100f;
    private float maxHealth;
    [SerializeField]
    private float currentDamage = 10f;
    [SerializeField]
    private float shootCadence = 0.5f;

    private bool isTir = false;
    private float timePasse = 0f;

    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private Color hurtColor;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private GameObject bulletInstance;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Camera camera;
    private Animator animator;

    private Vector2 movement;
    private Vector3 targetPosition;
    

    void Start()
    {
        maxHealth = currentHealth;
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerSetup();
    }

    void PlayerSetup()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Shoot();
    }



    private void Move()
    {
        movement = transform.worldToLocalMatrix.inverse * movement;
        rigidbody.MovePosition(rigidbody.position + movement.normalized * speedMovement * Time.fixedDeltaTime);
        animator.SetBool("IsMove", movement.sqrMagnitude > 0);
    }

    void Rotate()
    {
    	targetPosition.z = transform.position.z;
        if(Vector3.Distance(targetPosition, transform.position) < 0.2f){return;}
    	Vector3 aimVector = targetPosition - transform.position;
    	transform.up = aimVector;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0f)
        {
            healthBar.SetHealth(0f);
            FindObjectOfType<GameManagement>().PlayerDie();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Flash());
            healthBar.SetHealth(currentHealth);
        }
    }

    void Shoot()
    {
        if (timePasse >= shootCadence)
        {
            if (isTir)
            {
                StartCoroutine(TirCoroutine());
                timePasse = 0f;
                animator.SetTrigger("IsShoot");
            }
        }
        else
        {
            timePasse += Time.deltaTime;
        }
    }

    public void Upgrade()
    {
        PlayerSetup();
        currentDamage += 5f;
        speedMovement += 0.6f;
        shootCadence -= 0.09f;
    }


    IEnumerator TirCoroutine()
    {
        float timeWait = 0.25f;
        yield return new WaitForSeconds(timeWait);
        StartCoroutine(CameraShake.Instance.Shake(0.1f, 0.3f));
        GameObject bullet = (GameObject)Instantiate(bulletInstance, firePosition.position, transform.rotation);
        bullet.GetComponent<Bullet>().setParametre(currentDamage, this);
    }

    IEnumerator Flash()
    {
        spriteRenderer.color = hurtColor;
        StartCoroutine(CameraShake.Instance.Shake(0.5f, 0.5f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }


    private void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        targetPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        isTir = Input.GetMouseButton(0);
    }
}
