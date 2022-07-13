using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    [SerializeField] float health = 200f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 2f;
    [SerializeField] float maxTimeBetweenShots = 7f;
    [SerializeField] float enemyProjectileSpeed = 7f;
    [SerializeField] int scoreValue = 100;

    [Header("VFX/SFX")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionParticlesPrefab;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0f,1f)] float laserVolume = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
    }

    void DestroyEnemy()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position);
        Destroy(gameObject);
        GameObject explosionParticles = Instantiate(explosionParticlesPrefab, transform.position, transform.rotation);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(explosionParticles, 1f); // 1f = duration of explosion
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {

        health -= damageDealer.GetDamage();
        damageDealer.DestroyHit();
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }
}
