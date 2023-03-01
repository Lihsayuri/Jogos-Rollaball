using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private Rigidbody rb; // isso vai criar uma variável rigidbody para aplicar forças (private, não acessível ao inspector)
    private float movementX;
    private float movementY;
    private int count;
    private bool stop_timer;
    public float timeRemaining = 10;
    public TextMeshProUGUI timeText;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Setando o count pra 0 aqui, pois ele é private
        count = 0;
        stop_timer = false;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    { 
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && !stop_timer)
        {
            // Apenas desativa o Gameobject, mas não o destroi. Só que pra desativar tem que escolher qual, porque
            // a bolinha é um rigidbody que vai ter interação e colisao com tudo : ground, walls, por isso a tag.
            // by the way, precisa transformar em trigger e não continuar em normal colliders.
            // Ainda colocar kinematic - standar rigid bodies movem por forças fisica. 
            // Os kinematic movem por transform.
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            // Set the text value of your 'winText'
            stop_timer = true;
            winTextObject.SetActive(true);
        }
    }

    void Update() 
    {
        if (timeRemaining > 0 && !stop_timer)
        {
            timeRemaining -= Time.deltaTime;
            ShowTime(timeRemaining);
        }
        else if (timeRemaining <= 0 && !stop_timer) 
        {
            stop_timer = true;
            loseTextObject.SetActive(true);
            
        }
    }

    void ShowTime(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("Tempo restante : {0:00}:{1:00}", minutes, seconds);

    }
}
