using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lateral : MonoBehaviour
{

    public GameObject MeuHeroi;
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
        Debug.Log("AA");
        if (colidiu.gameObject.tag == "Chao")
        {
            MeuHeroi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, MeuHeroi.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}