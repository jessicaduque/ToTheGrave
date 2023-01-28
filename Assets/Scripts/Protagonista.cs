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
    public int HP = 10;
    public Slider MinhaBarraDeVida;
    public Slider MinhaBarraDeTransformacao;
    private int caveiras;
    private int vidas = 3;
    private float vel;
    private bool morcego = false;
    private int contTempo = 0;
    public int tempoTransformacao = 3000;
    private int vidaPerdida = 0;

    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.position = new Vector3(-21.688f, -28.65f, 0);
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
            if (tempoTransformacao >= 3000 && Anim.GetBool("Morcego") == false)
            {
                Anim.SetBool("Morcego", true);
                Anim.SetTrigger("VirarMor");
            }

        }
        if (Input.GetKeyDown(KeyCode.T) && morcego)
        {
            Anim.SetBool("Morcego", false);
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


            if (tempoTransformacao < 3000)
            {
                tempoTransformacao += 25;
                MinhaBarraDeTransformacao.value = tempoTransformacao;
            }

        }
        else
        {
            GameObject.FindGameObjectWithTag("PeProtagonista").GetComponent<PeProtagonista>().NovaInicial();
            DiminuiTransformacao();
        }
    }

    void Movimento()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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
            vel = 0.21f;
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
            vel = 0.13f;
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
        Corpo.AddForce(Vector2.up * 320);
    }

    public void Disparo()
    {
        GameObject Dps = Instantiate(AtaqueDisparo, PontoDeSaida.transform.position, Quaternion.identity);
        Dps.GetComponent<AtkLanca>().Lado(lado);
        Destroy(Dps, 3f);
    }

    public bool EstadoProtagonista()
    {
        return morcego;
    }

    public void AtivarATK()
    {
        MeuAtk.SetActive(true);
    }

    public void DesativarATK()
    {
        MeuAtk.SetActive(false);
    }

    public void MorrerComoMorcego()
    {
        Anim.SetTrigger("MorreuMorcego");
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "AtaqueBoss")
        {
            colidiu.gameObject.SetActive(false);
            vidaPerdida = 1;
            Anim.SetTrigger("Dano");
        }
    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "AtaqueInimigo")
        {
            vidaPerdida = 1;
            Anim.SetTrigger("Dano");
        }

        if (colidiu.gameObject.tag == "CheckPoint")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().MudarCheckpoint();
        }

        if (colidiu.gameObject.tag == "Caveira")
        {
            caveiras++;
            //GameObject.FindGameObjectWithTag("SomCaveira").GetComponent<AudioSource>().Play();
            Destroy(colidiu.gameObject);
        }
        if (colidiu.gameObject.tag == "Sangue")
        {
            if (HP > 4)
            {
                HP = 10;
            }
            else
            {
                HP += 6;
            }
            MinhaBarraDeVida.value = HP;
            Destroy(colidiu.gameObject);
        }
        if (colidiu.gameObject.tag == "PontoVitoria")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().ProtagonistaVenceu();
        }
    }

    public void PerderVidaQueda(float tamQueda)
    {
        if (contTempo > 10)
        {
            vidaPerdida = Mathf.RoundToInt(tamQueda) / 15;
            Anim.SetTrigger("Dano");
        }
    }

    public void PerdeHP()
    {
        if (HP - vidaPerdida < 0)
        {
            HP = 0;
        }
        else
        {
            HP -= vidaPerdida;
        }
        MinhaBarraDeVida.value = HP;
        if (HP <= 0)
        {
            Anim.SetBool("Morto", true);
        }
    }

    public void DiminuiTransformacao()
    {
        tempoTransformacao -= 30;
        MinhaBarraDeTransformacao.value = tempoTransformacao;
        if (tempoTransformacao < 0)
        {
            Anim.SetBool("Morcego", false);
            morcego = false;
        }
    }
    public int ValorCaveiras()
    {
        return caveiras;
    }

    public void ChamarGameOver()
    {
        vidas--;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controlador>().ProtagonistaMorreu();
    }

    public int MinhasVidas()
    {
        return vidas;
    }

    public void DefinirPulos()
    {
        qtdpulos = 1;
    }

    public void NovaChance(Vector3 checkPosition)
    {
        HP = 10;
        MinhaBarraDeVida.value = HP;
        qtdpulos = 1;
        morcego = false;
        Anim.SetBool("Morto", false);
        Anim.SetTrigger("Reviver");
        Corpo.GetComponent<Collider2D>().enabled = true;
        transform.position = checkPosition;

    }

    public void AcabarTransformacao()
    {
        morcego = true;
        Corpo.velocity = new Vector2(0.0f, 0.0f);
    }
    public void AlmaCima()
    {
        Corpo.GetComponent<Collider2D>().enabled = false;
        Corpo.velocity = new Vector2(0, 4);
    }

    public void AlmaBaixo()
    {
        Corpo.velocity = new Vector2(0, -6);
    }

}
