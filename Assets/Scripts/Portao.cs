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
    public Image caixaDialogo;
    public bool inPortao = false;
    private bool bossMorto = false;

    // Start is called before the first frame update
    void Start()
    {
        mostradorPortao = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inPortao)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (bossMorto)
                {
                    AbrirPortao();
                }
                else
                {
                    apertarE.gameObject.SetActive(false);
                    caixaDialogo.gameObject.SetActive(true);
                    mortesInsuficientes.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            apertarE.gameObject.SetActive(false);
            caixaDialogo.gameObject.SetActive(false);
            mortesInsuficientes.gameObject.SetActive(false);
        }
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
            inPortao = true;
            apertarE.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Player")
        {
            inPortao = false;
        }
    }

    public void BossMorto()
    {
        bossMorto = true;
    }
   
}
