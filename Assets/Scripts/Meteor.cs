using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Meteor Info")]
    [SerializeField] float health = 200f;
    [SerializeField] int scoreValue = 100;

    [Header("VFX/SFX")]
    [SerializeField] GameObject explosionParticlesPrefab;
    [SerializeField] AudioClip explosionSFX;
    
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
