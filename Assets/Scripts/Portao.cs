using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portao : MonoBehaviour
{

    private SpriteRenderer mostradorPortao;
    public Sprite imgPortaoAberto;
    public GameObject barreira;
    private Collider2D col;
    public Text apertarE;
    public Text mortesInsuficientes;

    // Start is called before the first frame update
    void Start()
    {
        mostradorPortao = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AbrirPortao()
    {
        barreira.SetActive(false);
        mostradorPortao.sprite = imgPortaoAberto;
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Player")
        {
            apertarE.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Player")
        {
            apertarE.gameObject.SetActive(false);
            mortesInsuficientes.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().CheckInimigosMortos())
                {
                    AbrirPortao();
                }
                else
                {
                    apertarE.gameObject.SetActive(false);
                    mortesInsuficientes.gameObject.SetActive(true);
                }
            }
        }
    }
}
