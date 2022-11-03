using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject Checkpoint;
    public GameObject MeuHeroi;
    public int qtdInimigosMortos = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IniciarJogo()
    {
        Time.timeScale = 1;
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
            MeuHeroi.GetComponent<Protagonista>().NovaChance(Checkpoint.transform.position);
        }else
        {
            GameOver.SetActive(true);
        }
        
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
    
}
