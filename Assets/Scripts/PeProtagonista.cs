using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeProtagonista : MonoBehaviour
{
    public float yInicial;
    public float yFinal;
    public float diferencaY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Morte")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().ProtagonistaMorreuCova();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().ChamarGameOver();
        }

        if (colidiu.gameObject.tag == "Chao")
        {
            yFinal = transform.position.y;
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().EstadoProtagonista() == false)
            {
                diferencaY = yInicial - yFinal;
                if(diferencaY > 15)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().PerderVidaQueda(diferencaY);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colidiu)
    {
        yInicial = transform.position.y;
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Chao" || colidiu.gameObject.tag == "Inimigo")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().DefinirPulos();
        }
    }

    public void NovaInicial()
    {
        yInicial = transform.position.y;
    }

}
