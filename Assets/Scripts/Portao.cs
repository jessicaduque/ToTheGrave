using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portao : MonoBehaviour
{

    private SpriteRenderer mostradorPortao;
    public Sprite imgPortaoAberto;
    public GameObject barreira;
    private Collider2D collider;
    public Text apertarE;

    // Start is called before the first frame update
    void Start()
    {
        mostradorPortao = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AbrirPortao()
    {
        barreira.SetActive(false);
        mostradorPortao.sprite = imgPortaoAberto;
        collider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Player")
        {
            apertarE.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().CheckInimigosMortos())
                {
                    AbrirPortao();
                }
                else
                {
                    Debug.Log("SEM MORTES SUFICIENTES");
                }
            }
        }
        else
        {
            apertarE.gameObject.SetActive(false);
        }
    }

}
