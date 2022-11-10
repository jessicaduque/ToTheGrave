using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaSeguir : MonoBehaviour
{

    private Rigidbody2D Corpo;
    private Animator Anim;
    public float velX = 1;
    public float posInicial;
    public float posFinal;
    private int vida = 2;
    private bool morreu = false;
    private GameObject MeuHeroi;
    public GameObject AreaAtkInimigo;

    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        MeuHeroi = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (morreu == true)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            float distancia = Vector3.Distance(transform.position, MeuHeroi.transform.position);
            if (distancia < 3)
            {
                Ataque();
            }
            else
            {
                Mover();
            }

        }

    }

    void Mover()
    {
        Corpo.velocity = new Vector2(velX, 0);
        if (velX != 0)
        {
            Anim.SetBool("Andando", true);
        }
        else
        {
            Anim.SetBool("Andando", false);
        }
        if (transform.position.x < posInicial)
        {
            velX = 3;
            transform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
        if (transform.position.x > posFinal)
        {
            velX = -3;
            transform.localScale = new Vector3(-0.8f, 0.8f, 1);
        }
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "AtaqueHeroi")
        {
            colidiu.gameObject.SetActive(false);
            Anim.SetTrigger("Dano");
        }

    }

    void Ataque()
    {
        Corpo.velocity = new Vector2(0, 0);
        Anim.SetTrigger("Ataque");
        if (transform.position.x > MeuHeroi.transform.position.x)
        {
            transform.localScale = new Vector3(-0.8f, 0.8f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
    }

    public void PerdeuHP()
    {
        vida--;
        if (vida < 0)
        {
            Anim.SetBool("Morto", true);
        }
    }

    public void Morrer()
    {
        Corpo.velocity = new Vector2(0, 0);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().InimigoMorreu();
        morreu = true;

    }

    public void Desaparecer()
    {
        Destroy(this.gameObject);
    }

    public int infoVida()
    {
        return vida;
    }

    public void AtivaAtk()
    {
        AreaAtkInimigo.SetActive(true);
    }
    public void DesativaAtk()
    {
        AreaAtkInimigo.SetActive(false);
    }
}
