using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMove : MonoBehaviour
{
    public float velocidade = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        MoveFundo();
    }

    void MoveFundo()
    {
        float posX = transform.position.x - velocidade * Time.deltaTime * 1;
        transform.position = new Vector3(posX, 0, 0);
    }
}
