using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Nave : MonoBehaviour
{
    //VARIABLE DE LA VELOCIDAD
    public float speed;

    //VARIABLES DE LOS CHECKPOINTS Y DE LA VIDA
    public int contador = 0;
    public int vida = 4;

    //TEXTOS DEL CANVAS
    public Text textContador;
    public Text textVida;

    //IMAGEN FINAL
    public GameObject imagen;
    public bool activo = true;

    // Start is called before the first frame update
    void Start()
    {
        vida = 4;
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //LA NAVE SE MUEVE AUTOMÁTICAMENTE
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime, Space.Self);

        //SE LLAMA AL MÉTODO PARA HACER LOS MOVIMIENTOS DE LA NAVE
        movimientoNave();
    }

    //SE ASIGNA UN MÉTODO PARA LOS MOVIMIENTOS DE LA NAVE
    void movimientoNave()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Rotate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //SE VAN CONTANDO LOS CHECKPOINTS QUE VA RECOGIENDO EL JUGADOR

        if (other.gameObject.tag == "Checkpoint")
        {
            contador++;
            textContador.text = "" + contador;
            Debug.Log("Checkpoints: " + contador);
            Destroy(other.transform.gameObject);
            if (contador == 4)
            {
                //SALE UNA IMAGEN INDICANDO QUE HEMOS GANADO 
                imagen.SetActive(activo);
            }
        }

        //SI EL JUGADOR CHOCA CON ALGÚN OBSTÁCULO SE LE RESTARA VIDA

        if (other.transform.tag == "Obstaculo")
        {
            vida--;
            textVida.text = "" + vida;
            Debug.Log("Vida: " + vida);
            Destroy(other.transform.gameObject);

            //SI LA VIDA LLEGA A 0, AUTOMÁTICAMENTE TERMINA LA PARTIDA LLEVÁNDOLO A OTRA ESCENA
            //INDICANDO QUE HA TERMINADO

            if (vida == 0)
            {
                Debug.Log("Derrota");
                Destroy(other.transform.gameObject);
                SceneManager.LoadSceneAsync(1);
            }
        }
    }
}
