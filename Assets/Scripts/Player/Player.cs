using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // library import images

public class Player : MonoBehaviour {
    public int health; 
    public float speed;
    public float jumpForce;
    public float atkRadius;
    public float recoveryTime; 
    

    bool isJumping;
    bool isAttacking;
    bool isDead;

    float recoveryCount; 

    public Rigidbody2D rig;
    public Animator anim;
    public Transform firePoint;
    public LayerMask enemyLayer; // damage is applied just when hitting the enemy
    public Image healthBar;

    // Start is called before the first frame update - é chamado uma vez ao inicializar o jogo
    void Start() {
        
    }

    // Update is called once per frame - é chamado a cada frame
    void Update() {
        if(isDead == false) {
            Jump();
            OnAttack();
        } 
        
    }

    // For flying monster put Rigidbody2D type to kinematic or gravity Scale to 0
    void Jump() {
        if(Input.GetButtonDown("Jump")) { 

            if (isJumping == false) { //(!isJumping)
                anim.SetInteger("transition", 2);

                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);       //rig is the body, addForce is what give "strenght" to jump
                // or ...(new Vector2(0,1) * jumpForce....)

                isJumping = true; // can't jump again mid air
            }
            
        }
    }

    void OnAttack() {
        if(Input.GetButtonDown("Fire1")) {
            isAttacking = true;
            anim.SetInteger("transition", 3);

            Collider2D hit = Physics2D.OverlapCircle(firePoint.position, atkRadius, enemyLayer);

            if(hit != null) {
                hit.GetComponent<FlightEnemy>().OnHit();
            }

            StartCoroutine(OnAttacking());
        }
    }

    // coroutine
    IEnumerator OnAttacking() {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    // visualize hit box 
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(firePoint.position, atkRadius);
    }

    public void OnHit(int damage) {
        recoveryCount += Time.deltaTime;

        if(recoveryCount >= recoveryTime && isDead == false) {
            anim.SetTrigger("hit");
            health--;

            healthBar.fillAmount -= damage/health;

            GameOver();

            recoveryCount = 0;
        }
    }

    void GameOver() {
        if(health <= 0) {
            anim.SetTrigger("death");
            isDead = true; 
        }
    }

    // Called when Physics 2D is altered. - é chamado pela física do jogo
    void FixedUpdate(){
        if(isDead == false) {
            OnMove();
        }
    }

    void OnMove() {
        float direction = Input.GetAxis("Horizontal"); //horizontal input

        rig.velocity = new Vector2(direction * speed, rig.velocity.y); // y with no speed - y sem velocidade

        if(direction > 0 && !isJumping && !isAttacking) {
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetInteger("transition", 1);
        }

        if (direction < 0 && !isJumping && !isAttacking) {
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetInteger("transition", 1);
        }

        if (direction == 0 && !isJumping && !isAttacking) {
            anim.SetInteger("transition", 0);
        }    
    }


    void OnCollisionEnter2D(Collision2D collision) {
        //if = true, player is touching the ground
        if (collision.gameObject.layer == 8) {
            isJumping = false;
        }
    }
    
}
