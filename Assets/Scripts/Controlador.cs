using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject Vitoria;
    private Vector3 Checkpoint;
    public GameObject MeuHeroi;
    public GameObject Boss;
    public int qtdInimigosMortos = 0;
    private bool comecouJogo = false;
    public Text bossApareceu;
    public Image caixaDialogo;
    private int contBossApareceuDialogo = 0;
    private bool protagonistaVenceu = false;

    public Image ConquistaColecionadoraTotal;
    public Image ConqusitaEficienciaMaxima;
    public Image ConquistaRecomecandoOCiclo;
    public Image ConquistaAcostumadaAoCemiterio;
    public Image ConquistaPeNaCova;
    private bool peNaCova = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Checkpoint = new Vector3(-20.688f, -28.65f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBoss();
    }

    public void Conqusitas()
    {
        if (protagonistaVenceu)
        {
            //aaa
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().ValorCaveiras() == 8)
        {
            //aaa
        }

        if(protagonistaVenceu && GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().TempoGasto() < 8) { }
        {
           // aaa
        }

        if(protagonistaVenceu &&  GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().MinhasVidas() == 3)
        {
            //aaa
        }

        if (peNaCova)
        {
            //aaa
        }
    }

    public void IniciarJogo()
    {
        Time.timeScale = 1;
        comecouJogo = true;
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(0);
    }

    public void ProtagonistaMorreu()
    {
        
        //perguntando para o heroi
        int qtdVidas = MeuHeroi.GetComponent<Protagonista>().MinhasVidas();
        if(qtdVidas > 0)
        {
            MeuHeroi.GetComponent<Protagonista>().NovaChance(Checkpoint);
        }else
        {
            GameOver.SetActive(true);
        }
        
    }
    
    public void ProtagonistaMorreuCova()
    {
        peNaCova = true;
    }
    public void ProtagonistaVenceu()
    {
        protagonistaVenceu = true;
        Vitoria.SetActive(true);
    }
    public void MudarCheckpoint()
    {
        Checkpoint = MeuHeroi.transform.position;
    }

    public void InimigoMorreu()
    {
        qtdInimigosMortos++;
    }

    public bool CheckInimigosMortos()
    {
        if(qtdInimigosMortos == 11)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnBoss()
    {
        if (CheckInimigosMortos())
        {
            if(contBossApareceuDialogo < 3000)
            {
                contBossApareceuDialogo++;
                caixaDialogo.gameObject.SetActive(true);
                bossApareceu.gameObject.SetActive(true);
            }
            else
            {
                caixaDialogo.gameObject.SetActive(false);
                bossApareceu.gameObject.SetActive(false);
            }
            Boss.SetActive(true);
        }
    }

    public bool ComecouJogo()
    {
        return comecouJogo;
    }
    
}
