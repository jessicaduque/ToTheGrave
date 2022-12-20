using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaSeguir : MonoBehaviour
{

    private Rigidbody2D Corpo;
    private Animator Anim;
    public float velX = 3;
    public float posInicial;
    public float posFinal;
    private int vida = 3;
    private bool morreu = false;
    private GameObject MeuHeroi;
    public GameObject AreaAtkInimigo;
    public int contAtaque = 0;
    public Vector3 posAgoraHeroi;
    public string direcao = "direita";

    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        MeuHeroi = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        ChecarLado();
        if (morreu == true)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            float distancia = Vector3.Distance(transform.position, MeuHeroi.transform.position);
            if (distancia < 6f)
            {
                Ataque();
            }
            else
            {
                DesativaAtk(); 
                contAtaque = 0;
                Mover();
            }

        }

    }

    void ChecarLado()
    {
        if (direcao == "direita")
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }
        else
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 1);
        }
    }
    void Mover()
    {
        Corpo.velocity = new Vector2(velX, 0);
        if (transform.position.x < posInicial)
        {
            velX = 3;
            direcao = "direita";
        }
        if (transform.position.x > posFinal)
        {
            velX = -3;
            direcao = "esquerda";
        }
    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "AtaqueHeroi")
        {
            colidiu.gameObject.SetActive(false);
            Anim.SetTrigger("Dano");
        }

    }

    void Ataque()
    {
        if (contAtaque == 1)
        {
            posAgoraHeroi = MeuHeroi.transform.position;
            if (transform.position.x > MeuHeroi.transform.position.x)
            {
                direcao = "esquerda";
            }
            else
            {
                direcao = "direita";
            }
        }
        else if (contAtaque < 500)
        {
            AtivaAtk();
            if (direcao == "direita")
            {
                transform.position = Vector3.MoveTowards(transform.position, posAgoraHeroi + new Vector3(4f, 0, 0), 0.05f);
                if (Vector3.Distance(transform.position, posAgoraHeroi + new Vector3(4f, 0, 0)) < 0.005f)
                {
                    contAtaque = 500;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, posAgoraHeroi - new Vector3(4f, 0, 0), 0.05f);
                if (Vector3.Distance(transform.position, posAgoraHeroi - new Vector3(4f, 0, 0)) < 0.005f)
                {
                    contAtaque = 500;
                }
            }
            
        }
        else
        {
            DesativaAtk();
            if (contAtaque > 1000)
            {
                contAtaque = 0;
            }
            else
            {
                Corpo.velocity = new Vector2(0f, 0f);
            }

        }

        contAtaque++;
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
        Corpo.velocity = new Vector2(0, 2);
        morreu = true;

    }

    public void Desaparecer()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().InimigoMorreu();
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
