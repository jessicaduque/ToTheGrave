using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarCaveiras : MonoBehaviour
{
    private Protagonista MeuHeroi;
    private Text MeuTexto;

    // Start is called before the first frame update
    void Start()
    {
        MeuHeroi = GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>();
        MeuTexto = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MeuTexto.text = MeuHeroi.ValorCaveiras().ToString();
    }
}
