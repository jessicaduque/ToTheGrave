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
    private bool cct = false;
    public Image ConquistaEficienciaMaxima;
    private bool cem = false;
    public Image ConquistaRecomecandoOCiclo;
    private bool croc = false;
    public Image ConquistaAcostumadaAoCemiterio;
    private bool caac = false;
    public Image ConquistaPeNaCova;
    private bool cpnc = false;
    private bool peNaCova = false;

    public float segundos = 0;

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
        Conquistas();
    }

    public void Conquistas()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().ValorCaveiras() == 8 && cct == false)
        {
            ConquistaColecionadoraTotal.gameObject.SetActive(true);
            segundos += 1 * Time.deltaTime;
            if (segundos > 5)
            {
                ConquistaColecionadoraTotal.gameObject.SetActive(false);
                cct = true;
                segundos = 0;
            }
        }

        if (peNaCova && cpnc == false)
        {
            ConquistaPeNaCova.gameObject.SetActive(true);
            segundos += 1 * Time.deltaTime;
            if (segundos > 5)
            {
                ConquistaPeNaCova.gameObject.SetActive(false);
                cpnc = true;
                segundos = 0;
            }
        }

        if (protagonistaVenceu && croc == false)
        {
            ConquistaRecomecandoOCiclo.gameObject.SetActive(true);
            segundos += 1 * Time.deltaTime;
            if (segundos > 2)
            {
                ConquistaRecomecandoOCiclo.gameObject.SetActive(false);
                croc = true;
            }
        }

        if (protagonistaVenceu && GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().TempoGasto() < 8 && cem == false)
        {
            ConquistaEficienciaMaxima.gameObject.SetActive(true);
            segundos += 1 * Time.deltaTime;
            if (segundos > 4)
            {
                ConquistaEficienciaMaxima.gameObject.SetActive(false);
                cem = true;
                segundos = 0;
            }
        }

        if (protagonistaVenceu && GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().MinhasVidas() == 3 && caac == false)
        {
            ConquistaAcostumadaAoCemiterio.gameObject.SetActive(true);
            segundos += 1 * Time.deltaTime;
            if (segundos > 6)
            {
                ConquistaAcostumadaAoCemiterio.gameObject.SetActive(false);
                caac = true;
                segundos = 0;
            }
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
        if(qtdInimigosMortos == 16)
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
            if(contBossApareceuDialogo < 500)
            {
                contBossApareceuDialogo++;
                caixaDialogo.gameObject.SetActive(true);
                bossApareceu.gameObject.SetActive(true);
            }
            else if(contBossApareceuDialogo == 500)
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

    public void SairDoJogo()
    {
        Application.Quit();
    }
    
}
