using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaParado : MonoBehaviour
{

    private Rigidbody2D Corpo;
    private Animator Anim;
    public float velX = 1;
    private int vida = 3;
    private bool morreu = false;
    private GameObject MeuHeroi;

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

    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "AtaqueHeroi")
        {
            colidiu.gameObject.SetActive(false);
            Anim.SetTrigger("Dano");
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

}
