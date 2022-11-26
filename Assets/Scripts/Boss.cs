using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private Rigidbody2D Corpo;
    private Animator Anim;
    public float velX = 1;
    private int vida = 5;
    private bool morreu = false;
    private GameObject MeuHeroi;
    public GameObject AreaAtkInimigo;
    public int contSumAp = 0;

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
            if (distancia < 12 && distancia >= 3.5f)
            {
                Anim.SetBool("Andar", true);
                SumirAparecer();
                Perseguir();
            }
            else if (distancia < 3.5f)
            {
                contSumAp = 0;
                Ataque();
            }
            else
            {
                contSumAp = 0;
                Anim.SetBool("Andar", false);
            }

        }

    }

    void SumirAparecer()
    {
        contSumAp++;
        if(contSumAp > 800)
        {
            Anim.SetTrigger("DesaAp");
            contSumAp = 0;
        }
    }

    void Perseguir()
    {
        transform.position = Vector2.MoveTowards(transform.position, MeuHeroi.transform.position, 0.0075f);
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "AtaqueHeroi")
        {
            colidiu.gameObject.SetActive(false);
            Anim.SetTrigger("Dano");
        }

    }

    void ChecarLado()
    {
        if (transform.position.x > MeuHeroi.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void Ataque()
    {
        Corpo.velocity = new Vector2(0, 0);
        Anim.SetTrigger("Ataque");
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

        Corpo.velocity = new Vector2(0, -2);
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

    public void Sumir()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void Aparecer()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
