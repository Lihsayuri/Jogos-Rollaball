using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControllerHard : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI ganhou_perdeu;

    private Rigidbody rb; // isso vai criar uma vari�vel rigidbody para aplicar for�as (private, n�o acess�vel ao inspector)
    private float movementX;
    private float movementY;
    private int count;
    private bool stop_timer;

    private bool perdeu;
    private bool ganhou;
    public float timeRemaining = 120;
    public TextMeshProUGUI timeText; 
    public int vida = 5;

    [SerializeField]
    private Sprite [] _livesSprites;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private GameObject endGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Setando o count pra 0 aqui, pois ele � private
        count = 0;
        stop_timer = false;
        perdeu = false;
        ganhou = false;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        // winTextObject.SetActive(false);
        // loseTextObject.SetActive(false);
        endGamePanel.SetActive(false);

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
            other.gameObject.SetActive(false);
            count = count + 1;
            if (count >= 3 && vida < 5){
                count -= 3;
                vida =  vida + 1;
                _livesImage.sprite = _livesSprites[vida];
            }
            SetCountText();
        } if (other.gameObject.CompareTag("Win") && !stop_timer)
        {
            stop_timer = true;
            ganhou = true;
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "WallDamage" && !stop_timer)
        {
            vida = vida - 1;
            _livesImage.sprite = _livesSprites[vida];
            // Debug.Log("Vida restante: " + vida);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

    }

    void SetGanhouPerdeuText(){
        if (ganhou){
            ganhou_perdeu.text = "Congrats, you win!";
        }
        else if (perdeu){
            ganhou_perdeu.text = "Game over! You lose";
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
            // loseTextObject.SetActive(true);
            perdeu = true;
            
        } 
        if (vida <= 0 && !stop_timer) 
        {
            stop_timer = true;
            // loseTextObject.SetActive(true);
            perdeu = true;
        }

        CheckEndGame();
    }

    void ShowTime(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("Time remaining : {0:00}:{1:00}", minutes, seconds);

    }



    private void CheckEndGame()
    {
        if (perdeu || ganhou)
        {
            SetGanhouPerdeuText();
            // Esconde o contador de pontos
            countText.gameObject.SetActive(false);
            // Esconde o tempo
            timeText.gameObject.SetActive(false);
            // Esconde a vida
            _livesImage.gameObject.SetActive(false);
            endGamePanel.SetActive(true);
            // SceneManager.LoadScene(2);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(3);
    }


    public void Menu()
    {
        SceneManager.LoadScene(0);
    }


}
