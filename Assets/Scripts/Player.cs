using UnityEngine;
/* 
 * Inicializacion clase "Player" que hereda de "MonoBehaviour"
 * Esta clase representa un personaje que puede moverse y saltar
 * En un entorno 3D 
 */
public class Player : MonoBehaviour
{
    // Variables ---------------------------------------------------------------------------------------
    // "character" variable la cual nos permite usar el componente "Character Controller" de Unity. 
    private CharacterController character;
    // "direction" variable la cual nos permite usar la estructura "Vector3" de Unity.
    private Vector3 direction;
    /* 
     * "gravity" variable la cual define la fuerza de gravedad en nuesta escena
     *  En 9.81f * 2f, que vendria siendo dos veces mas fuerte que la gravedad en la tierra.
     *  ¡ Tener en cuenta que esta variable esta correlacionada con la variable "jumpForce"
     *    Por lo que cualquier cambio que se realice en sus valores afectara a la otra variable tambien!
     */
    public float gravity = 9.81f * 2f;
    // "jumpForce" variable la cual define la fuerza de salto de nuestro jugador.
    public float jumpForce = 8f;

    // Metodos y Funciones ---------------------------------------------------------------------------------------
    /* 
     * Metodo "Awake", se encarga de inicializar la variable "character" 
     * Con el componente "CharacterController" del objeto actual
     */
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    /*
     * Metodo "OnEnable", reinicia la variable "direction" cuando el objeto se habilita 
     * Para asegurar que el personaje no tenga ninguna velocida acumulada 
     * al momento de reiniciar o iniciar el juego.
     */

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    /*
     * Metodo "Update", actualiza la variable "direction" con la fuerza de gravedad "gravity"
     * Multiplicada por el tiempo "Time.deltaTime", posteriormente comprueba si el personaje 
     * Esta en contacto con el suelo y en base a esto establece la direccion vertical en cero
     * Y en el caso que el jugador presione el boton de salto, se actualiza la 
     * Direccion vertical a la  fuerza de salto. Por ultimo mueve al personaje usando el
     * Metodo "Move" del componente "CharacterController".
     */

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    /*
     * El Metodo "OnTriggerEnter" se encarga de comprobar las colisiones de nuestro 
     * Personaje con los obstaculos mediante la etiqueta de objeto colisionado.
     * ¡ Como en este juego no hay distintos tipos de obstaculos no es necesario
     *   usar un sistema de etiquedo o un componente de identificación 
     *   mas especifico para los obstaculos!
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")){
            GameManager.Instance.GameOver();
        }
    }
}
