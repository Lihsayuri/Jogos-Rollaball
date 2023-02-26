using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb; // isso vai criar uma vari�vel rigidbody para aplicar for�as (private, n�o acess�vel ao inspector)
    private float movementX;
    private float movementY;
    private int count;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Setando o count pra 0 aqui, pois ele � private
        count = 0;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);

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
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Apenas desativa o Gameobject, mas n�o o destroi. S� que pra desativar tem que escolher qual, porque
            // a bolinha � um rigidbody que vai ter intera��o e colisao com tudo : ground, walls, por isso a tag.
            // by the way, precisa transformar em trigger e n�o continuar em normal colliders.
            // Ainda colocar kinematic - standar rigid bodies movem por for�as fisica. 
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
            winTextObject.SetActive(true);
        }
    }
}
