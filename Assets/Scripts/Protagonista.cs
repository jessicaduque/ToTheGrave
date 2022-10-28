using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protagonista : MonoBehaviour
{
    private Rigidbody2D Corpo;
    private Animator Anim;
    public GameObject MeuAtk;
    public int qtdpulos = 1;
    public GameObject AtaqueDisparo;
    public GameObject PontoDeSaida;
    private string lado = "Direita";
    private int HP = 10;
    public Slider MinhaBarraDeVida;
    private int caveiras;
    public int vidas = 3;

    // Transforma��o
    private bool morcego = false;

    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            morcego = true;

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            morcego = false;
        }

        if (morcego == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Anim.SetTrigger("Ataque1");
            }
            if (Input.GetMouseButtonDown(1))
            {
                Anim.SetTrigger("Ataque2");
            }
        }
    }

    void Movimento()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Anim.SetBool("Andar", true);
        }
        else
        {
            Anim.SetBool("Andar", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-1, 1, 1);
            lado = "Esquerda";
        }
        else if (Input.GetKey(KeyCode.D)) 
        { 
        
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
            lado = "Direita";
        }

        if (morcego)
        {
            qtdpulos = 0;
            Corpo.gravityScale = 0;
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            }
        }
        else
        {
            Corpo.gravityScale = 1;
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (qtdpulos > 0)
                {
                    qtdpulos--;
                    Pular();
                }
            }
        }
    }
    void Pular()
    {
        Corpo.AddForce(Vector2.up * 460);
    }

    void Transformacao()
    {
        //GetComponent<Protagonista_Morcego>()
    }

    public void Disparo()
    {
        GameObject Dps = Instantiate(AtaqueDisparo, PontoDeSaida.transform.position, Quaternion.identity);
        Dps.GetComponent<AtkLanca>().Lado(lado);
        Destroy(Dps, 3f);
    }


    public void AtivarATK()
    {
        MeuAtk.SetActive(true);
    }

    public void DesativarATK()
    {
        MeuAtk.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Chao")
        {
            qtdpulos = 1;
        }
        if (colidiu.gameObject.tag == "AtaqueInimigo")
        {
            colidiu.gameObject.SetActive(false);

            Anim.SetTrigger("Dano");
        }
        if (colidiu.gameObject.tag == "Caveira")
        {
            caveiras++;
            GameObject.FindGameObjectWithTag("SomCaveira").GetComponent<AudioSource>().Play();
            Destroy(colidiu.gameObject);
        }
        if (colidiu.gameObject.tag == "Morte")
        {
            ChamarGameOver();
        }
    }


    public void PerdeHP()
    {
        HP--;
        MinhaBarraDeVida.value = HP;
        if (HP <= 0)
        {

            Anim.SetBool("Morto", true);
        }
    }

    //Fun��o
    public int ValorCaveiras()
    {
        return caveiras;
    }

    public void ChamarGameOver()
    {
        vidas--;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().Morreu();
    }

    //Informa Quantidade de Vidas
    public int MinhasVidas()
    {
        return vidas;
    }

    //Revivo Meu Heroi;
    //Esse metodo precisa de um parametro
    public void NovaChance(Vector3 checkPosition)
    {
        HP = 6;
        qtdpulos = 1;
        morcego = false;
        Anim.SetBool("Morto", false);
        Anim.SetTrigger("Reviver");
        transform.position = checkPosition;

    }
}
