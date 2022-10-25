using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkLanca : MonoBehaviour
{
    private string direcao = "";
    private float velocidade = 0.1f;
    private SpriteRenderer Sp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    public void Lado(string ladoEscolhido)
    {
        if(ladoEscolhido == "Esquerda")
        {
            direcao = "Esquerda";
            velocidade = -0.1f;
            Sp = GetComponent<SpriteRenderer>();
            Sp.flipX = true;
        }
        if(ladoEscolhido == "Direita")
        {
            direcao = "Direita";
            velocidade = 0.1f;
            Sp = GetComponent<SpriteRenderer>();
            Sp.flipX = false;
        }
    }

    void Mover()
    {
        transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);
    }


    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Inimigo")
        {
            //Destroy(colidiu.gameObject);
            //Destroy(this.gameObject);
        }

    }

}
