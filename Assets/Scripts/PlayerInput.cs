using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour{

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Input input;
    public float speed = 10;
    public float cooldownTime = 5;
    private float nextFireTime = 0;
    

    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        //New refrence to the input class
        input = new Input();
        input.Player.Enable();
    }

    // Update is called once per frame
    void Update(){
        //addes force based on input vector
        Vector2 moveVector = input.Player.Fly.ReadValue<Vector2>();
        rb.AddForce(moveVector * speed, ForceMode2D.Force);

        //checks for cooldown and break button to break the ship
        float breakVal = input.Player.Break.ReadValue<float>();
        if(breakVal == 1 && Time.time > nextFireTime){
            Vector2 currVector = rb.velocity;
            Vector2 breakVector = new Vector2(currVector.x * -1, currVector.y * -1);
            rb.AddForce(breakVector, ForceMode2D.Impulse);
            nextFireTime = Time.time + cooldownTime;
        }
        
    }
}
