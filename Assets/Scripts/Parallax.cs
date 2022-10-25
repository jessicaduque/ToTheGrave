using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float velocidade = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveFundo();
    }

    void MoveFundo()
    {
        float posX = transform.position.x - velocidade * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.position = new Vector3(posX, 0, 0);
    }
}
