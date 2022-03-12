using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float playerMoveSpeed = 10f;
    [SerializeField] float padding_player = 1f;

    [Header("Laser Componets")]
    [SerializeField] GameObject laser_player;
    [SerializeField] float player_laser_speed = 20;
    [SerializeField] float projectile_firing_period = 0.1f;

    [Header("Player Informations")]
    [SerializeField] float playerHealth = 500;
    Coroutine firing_coroutine;

    [Header("Sound")]
    [SerializeField] AudioClip deathSoundEffect;
    [SerializeField] AudioClip fireSoundEffect;
    [SerializeField] [Range(0,1)] float fireVolume = 1f;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;

    [Header("VFX")]
    [SerializeField] GameObject playerDeathEffect;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    
    // Start is called before the first frame update
    void Start()
    {       
        PlayerBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Fire();       
    }

    public float GetHealth() { return playerHealth; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageDealer damageDealer)
    {       
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSoundEffect, Camera.main.transform.position, deathVolume);
        Destroy(Instantiate(playerDeathEffect, transform.position, transform.rotation),2);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void Fire()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            firing_coroutine = StartCoroutine(ContinuesFire());
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firing_coroutine);
        }
  
    }

    IEnumerator ContinuesFire()
    {
        while (true)
        {
            GameObject laser = Instantiate(laser_player, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireSoundEffect, Camera.main.transform.position, fireVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player_laser_speed);
            yield return new WaitForSeconds(projectile_firing_period);            
        }
    }

    private void MovePlayer()
    {
        float x_move_value = Input.GetAxis("Horizontal") * Time.deltaTime * playerMoveSpeed;
        float y_move_value = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;

        float x_move_add_player = transform.position.x + x_move_value;
        float y_move_add_player = transform.position.y + y_move_value;

        Vector2 vector2 = new Vector2(Mathf.Clamp(x_move_add_player,xMin,xMax), Mathf.Clamp(y_move_add_player,yMin,yMax));
        transform.position = vector2;
    }
    public void PlayerBoundries()
    {
        Camera game_camera = Camera.main;
        xMin = game_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding_player;
        xMax = game_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding_player;
        yMin = game_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding_player;
        yMax = game_camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding_player;
    }


}
