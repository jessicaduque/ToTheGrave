using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject Checkpoint;
    public GameObject MeuHeroi;
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

    public void Morreu()
    {
        
        //perguntando para o heroi
        int qtdVidas = MeuHeroi.GetComponent<Heroi>().MinhasVidas();
        if(qtdVidas > 0)
        {
            MeuHeroi.GetComponent<Heroi>().NovaChance(Checkpoint.transform.position);
        }else
        {
            GameOver.SetActive(true);
        }
        
    }
}
