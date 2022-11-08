using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protagonista : MonoBehaviour
{
    private Rigidbody2D Corpo;
    private Animator Anim;
    public GameObject MeuAtk;
    private int qtdpulos = 1;
    public GameObject AtaqueDisparo;
    public GameObject PontoDeSaida;
    private string lado = "Direita";
    private int HP = 10;
    public Slider MinhaBarraDeVida;
    public Slider MinhaBarraDeTransformacao;
    private int caveiras;
    private int vidas = 3;
    private float vel;
    private bool morcego = false;
    private int contTempo = 0;
    private int tempoTransformacao = 3000;

    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().ComecouJogo()) {
            contTempo++; 
        }
        Movimento();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tempoTransformacao == 3000)
            {
                Corpo.velocity = new Vector2(0.0f, 0.0f);
                morcego = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            morcego = false;
        }

        if (morcego == false)
        {
            if (contTempo > 5)
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


            if(tempoTransformacao < 3000)
            {
                tempoTransformacao++;
                MinhaBarraDeTransformacao.value = tempoTransformacao;
            }

        }
        else
        {
            DiminuiTransformacao();
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
            transform.position = new Vector3(transform.position.x - vel, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-1, 1, 1);
            lado = "Esquerda";
        }
        else if (Input.GetKey(KeyCode.D)) 
        { 
        
            transform.position = new Vector3(transform.position.x + vel, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
            lado = "Direita";
        }

        if (morcego)
        {
            vel = 0.016f;
            qtdpulos = 0;
            Corpo.gravityScale = 0;
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + vel, transform.position.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - vel, transform.position.z);
            }
        }
        else
        {
            vel = 0.01f;
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

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Morte")
        {
            ChamarGameOver();
        }
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

    public void DiminuiTransformacao()
    {
        tempoTransformacao--;
        MinhaBarraDeTransformacao.value = tempoTransformacao;
        if(tempoTransformacao == 0)
        {
            morcego = false;
        }
    }

    //Função
    public int ValorCaveiras()
    {
        return caveiras;
    }

    public void ChamarGameOver()
    {
        vidas--;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().ProtagonistaMorreu();
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
