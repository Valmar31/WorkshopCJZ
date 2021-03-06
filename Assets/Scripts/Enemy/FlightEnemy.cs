﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightEnemy : MonoBehaviour {

    public float health;
    public float damage; 

    public float speed; 
    float initialSpeed;

    public float stopDistance;
    public bool isRight;

    public Rigidbody2D rig;
    public Animator anim;

    bool isDead; 

    Transform player;

    // Start is called before the first frame update
    void Start() {
        // transform is every object in scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector2.Distance(transform.position, player.position);
        // Debug.Log(transform.position.x - player.position.x);
        float playerPos = transform.position.x - player.position.x;

        if(playerPos > 0) {
            isRight = false;
        }
        else {
            isRight = true;
        }

        // executed when enemy spot the player
        if(distance <= stopDistance) {
            speed = 0f;
            player.GetComponent<Player>().OnHit(damage); 
        }
        else {
            speed = initialSpeed;
        }

    }
 
    void FixedUpdate() {

        if (isDead == false) {
            if(isRight) {
                rig.velocity = new Vector2(speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 0);
            }
            else{
                rig.velocity = new Vector2(-speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        else {
            speed = 0f;
        }        
    }

    // need to be public, so player can access it
    public void OnHit() {

        health--;

        if(health <= 0) {
            isDead = true;
            speed = 0f; 
            anim.SetTrigger("death");
            Destroy(gameObject, 1f);
        }
        else {
            anim.SetTrigger("hit"); 
        }

    }

}
