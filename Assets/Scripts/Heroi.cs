using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heroi : MonoBehaviour
{
    private Rigidbody2D Corpo;
    private Animator Anim;
    public GameObject MeuAtk;
    public int qtdpulos = 2;
    public GameObject AtaqueDisparo;
    public GameObject PontoDeSaida;
    private string lado = "Direita";
    private int HP = 10;
    public Slider MinhaBarraDeVida;
    private int moedas;
    public int vidas = 3;

    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //Movimento
        float velX = Input.GetAxis("Horizontal")*4;
        float minhaG = Corpo.velocity.y;
        
        Corpo.velocity = new Vector2(velX, minhaG);

        if(velX == 0)
        {
            Anim.SetBool("Andar", false);
        }
        else
        {
            Anim.SetBool("Andar", true);
            if(velX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                lado = "Esquerda";
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                lado = "Direita";


            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Anim.SetTrigger("Ataque");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Anim.SetTrigger("AtaqueL");
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
           if(qtdpulos > 0)
            {
                qtdpulos--;
                Pular();
            }
                

           
            
        }

    }



    void Pular()
    {
        Corpo.AddForce(Vector2.up * 330);
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
            qtdpulos = 2;
        }
        if (colidiu.gameObject.tag == "AtaqueInimigo")
        {
            colidiu.gameObject.SetActive(false);

            Anim.SetTrigger("Dano");
        }
        if (colidiu.gameObject.tag == "Moeda")
        {
            moedas++;
            GameObject.FindGameObjectWithTag("SomMoeda").GetComponent<AudioSource>().Play();
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

    //Função
    public int ValorMoedas()
    {
        return moedas;
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
        HP = 10;
        qtdpulos = 2;
        Anim.SetBool("Morto", false);
        Anim.SetTrigger("Reviver");
        transform.position = checkPosition;

    }
}
