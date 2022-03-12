using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int enemyKillPoint;

    [Header("Shooting")]
    [SerializeField] float min_time_between_shoot = 0.2f;
    [SerializeField] float max_time_between_shoot = 1f;
    [SerializeField] float shoot_counter;
    [SerializeField] GameObject laser_enemy;
    [SerializeField] float laser_enemy_speed;

    [Header("VFX")]
    [SerializeField] GameObject explosion;

    [Header("Sound")]
    [SerializeField] AudioClip deathSoundEffect;
    [SerializeField] AudioClip fireSoundEffect;
    [SerializeField] [Range(0, 1)] float fireVolume = 1f;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;

    private void Start()
    {
        shoot_counter = Random.Range(min_time_between_shoot, max_time_between_shoot);
    }

    private void Update()
    {
        CountAndShoot();       
    }

    void CountAndShoot()
    {
        shoot_counter -= Time.deltaTime;
        if (shoot_counter <= 0)
        {
            Fire();
            shoot_counter = Random.Range(min_time_between_shoot, max_time_between_shoot);
        }              
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laser_enemy, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * laser_enemy_speed);
        AudioSource.PlayClipAtPoint(fireSoundEffect, Camera.main.transform.position, fireVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health > 0) { damageDealer.Hit(); }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 1);
        AudioSource.PlayClipAtPoint(deathSoundEffect,Camera.main.transform.position, deathVolume);
        FindObjectOfType<GameSession>().UpdateScore(enemyKillPoint);
    }
}
