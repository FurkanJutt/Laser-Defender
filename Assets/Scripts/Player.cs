using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Configuration Paramaters
    [Header("Player")]
    [SerializeField] bool inverablity = false;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] int health = 500;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] private float playerProjectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.3f;

    [Header("VFX/SFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0f, 1f)] float laserVolume = 0.4f;

    Coroutine fireCoroutine;

    float xMax, yMax;
    float xMin, yMin;
    float padding = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SetupBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    private void PlayerMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void PlayerShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinously());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject; // Quaternion.identity means no rotation
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerProjectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetupBoundaries()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth() 
    {
        if (health < 0)
            health = 0;
        return health; 
    }

    void DestroyPlayer()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!inverablity)
        {
            DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) return;
            ProcessHit(damageDealer);
        }
    }

        private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.DestroyHit();
        if (health <= 0)
        {
            DestroyPlayer();
        }
    }
}
