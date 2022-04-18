using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public float timer;
    private float cuentaAtras;

    public TextMeshProUGUI countText;// puntuacion
    public GameObject winTextObject;//ventana final 
    public GameObject PanelInGame;//ventana durante el juego 
    public GameObject Inicio;// Mostrar/Ocultar Inicio

    public TextMeshProUGUI FinalText;// texto de si ha ganado o perdido
    public TextMeshProUGUI TimerText;// Texto del timer
    public TextMeshProUGUI Puntuacion;// Texto de la puntuacion
    public TextMeshProUGUI MejorPuntuacion;// Texto de la puntuacion
    public TextMeshProUGUI InicioText;// Cuentra atras Inicio


    private float movementX;
    private float movementY;

    private Rigidbody rb;
    private int count;

    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        PlayerPrefs.GetInt("MejorPuntaje", 0).ToString();


        // Set the count to zero 
        count = 0;
        cuentaAtras = 3;
        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    void FixedUpdate()
    {
        cuentaAtras -= Time.deltaTime;
        InicioText.text = cuentaAtras.ToString("f0");

        if (cuentaAtras <= 0)
        {
            Inicio.SetActive(false);


            if (timer > 0.0)
            {
                timer -= Time.deltaTime;
                TimerText.text = "" + timer.ToString("f2");

                // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
                Vector3 movement = new Vector3(movementX, 0.0f, movementY);

                rb.AddForce(movement * speed);
            }
            else
            {
                SetCountText();

                if (count >= 8)
                {
                    FinalText.text = "Has Ganado!!!";
                }
                else
                {
                    FinalText.text = "Has Perdido!!!";
                }

                if (PlayerPrefs.GetInt("MejorPuntaje", 0) < count)
                {
                    PlayerPrefs.SetInt("MejorPuntaje", count);
                }

                MejorPuntuacion.text = "Mejor Puntuación\n"
                            + PlayerPrefs.GetInt("MejorPuntaje", 0).ToString();

                PanelInGame.SetActive(false);

                // Set the text value of your 'winText'
                winTextObject.SetActive(true);
            }

        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();


            if (count >= 8)
            {
                //compruebo si la puntuacion guardada es menor que la nueva
                if (PlayerPrefs.GetInt("MejorPuntaje", 0) < count)
                {
                    // si se cumple la condicion
                    // pongo como mejor puntuacion el nuevo
                    PlayerPrefs.SetInt("MejorPuntaje", count);
                    MejorPuntuacion.text = "Mejor Puntuación\n"
                        + PlayerPrefs.GetInt("MejorPuntaje", 0).ToString();
                }

                PanelInGame.SetActive(false);

                FinalText.text = "Has Ganado!!!";
                // Set the text value of your 'winText'
                winTextObject.SetActive(true);

            }

        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        Puntuacion.text = "Tu Puntuación\n" + count.ToString();
    }



}
