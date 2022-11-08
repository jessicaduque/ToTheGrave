using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeProtagonista : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Morte")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().ChamarGameOver();
        }
    }
    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Chao")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Protagonista>().DefinirPulos();
        }
    }
}
