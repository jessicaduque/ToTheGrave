using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    public GameObject GameOver;
    private Vector3 Checkpoint;
    public GameObject MeuHeroi;
    public GameObject Boss;
    public int qtdInimigosMortos = 0;
    private bool comecouJogo = false;

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
        if(qtdInimigosMortos == 1)
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
            Boss.SetActive(true);
        }
    }

    public bool ComecouJogo()
    {
        return comecouJogo;
    }
    
}
