using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    public Rigidbody2D rig;

    // Start is called before the first frame update - é chamado uma vez ao inicializar o jogo
    void Start() {
        
    }

    // Update is called once per frame - é chamado a cada frame
    void Update() {
        
    }

    // Called when Physics 2D is altered. - é chamado pela física do jogo
    void FixedUpdate(){
        float direction = Input.GetAxis("Horizontal"); //horizontal input

        rig.velocity = new Vector2(direction * speed, rig.velocity.y); // y with no speed - y sem velocidade

        if(direction > 0) {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction < 0) {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
