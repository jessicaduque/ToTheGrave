using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarMoedas : MonoBehaviour
{
    private Heroi MeuHeroi;
    private Text MeuTexto;

    // Start is called before the first frame update
    void Start()
    {
        MeuHeroi = GameObject.FindGameObjectWithTag("Player").GetComponent<Heroi>();
        MeuTexto = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MeuTexto.text = MeuHeroi.ValorMoedas().ToString();
    }
}
