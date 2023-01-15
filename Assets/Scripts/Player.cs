using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // creacion de varibles para hacer referencias en el desarrollo del programa
    //creacion de variable para la velocidad del jugador en el eje vertical
    [SerializeField] private float verticalForce = 400f;
    //variables para asignar los colores
    [SerializeField] private Color orangeColor;
    [SerializeField] private Color violetColor;
    [SerializeField] private Color cyanColor;
    [SerializeField] private Color pinkColor;
    //variable para saber que color tiene el player actualmente
    private string currentColor;

    Rigidbody2D playerrb;
    SpriteRenderer playerSr;
    // tiempo de espera tras chocar con otro color
    [SerializeField] private float restartDelay = 1f;

    //referenciar el efecto de las particulas
    [SerializeField] private ParticleSystem playerParticles; 
    void Start()
    {
        // agregaccion de fuerza un componente para poder resetear la gravedad por medio de un vetor 2 en el eje x y en el eje y
        playerrb = GetComponent<Rigidbody2D>();
        

        // playerrb = GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 400));

        //Se cambia la prodiedad del color del jugador es decir del sprite cicle 
        // Misma linea de codigo pero de forma referenciada con la variable que se creo para el SpriteRenderer
        playerSr = GetComponent<SpriteRenderer>();
        // GetComponent<SpriteRenderer>().color = Color.blue;

        //llamando la funcion
        CambioColor();
    }

    // Update is called once per frame
    void Update()
    {
        //creacion de fuerza cuando el jugador teclea espacio se creara un movimiento de fuerza en el eje y
        //Primero se detendra el objeto y luego se le aplicara la fuerza para que los saltos sean iguales
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerrb.velocity = Vector2.zero;
            playerrb.AddForce(new Vector2(0, verticalForce));
        }
    }
    //creacion de colicion en el objeto jugador
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colisiono con: " + collision.gameObject.name);
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cambio de color
        if (collision.gameObject.CompareTag("ColorChange"))
        {
            CambioColor();
            Destroy(collision.gameObject);
            return;
        }
        //finalizacion de juego
        if (collision.gameObject.CompareTag("LineaFinal"))
        {
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);
            Invoke("LoadNextScene", restartDelay);
            return;
        }
        //reseteo de escena
        if(!collision.gameObject.CompareTag(currentColor))
        {
            gameObject.SetActive(false);
            Instantiate(playerParticles,transform.position,Quaternion.identity);
            Invoke("RestartScene", restartDelay);
        }
     
    }

    //creacion de la funcion para asignar un color de forma aleatoria
    void CambioColor()
    {
        int randomNumber = Random.Range(0, 4);

        if(randomNumber == 0)
        {
            playerSr.color = orangeColor;
            currentColor = "Orange";
        }else if (randomNumber == 1)
        {
            playerSr.color = violetColor;
            currentColor = "Violet";
        }
        else if (randomNumber == 2)
        {
            playerSr.color = cyanColor;
            currentColor = "Cyan";
        }
        else if (randomNumber == 3)
        {
            playerSr.color = pinkColor;
            currentColor = "Pink";
        }
    }
    //creacion de funcion de cambio de escena
    void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }

    //reseteo de pantalla
    void RestartScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

}
