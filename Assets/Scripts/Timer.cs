using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    float segundos = 15f;
    int minutes = 0;
    bool acabouTempo = false;

    public Text countdown;

    void Start()
    {
    }

    private void Update()
    {
        if (acabouTempo)
        {
            segundos = 0;
            countdown.text = "00:00";
            // Chamar Gameover
        }
        else
        {
            if (minutes == 0 && segundos <= 0)
            {
                acabouTempo = true;
            }
            else if (segundos == 0)
            {
                segundos = 60f;
                minutes--;
            }

            segundos -= 1 * Time.deltaTime;
            if (segundos < 10 && minutes < 10)
            {
                countdown.text = "0" + minutes.ToString() + ":0" + segundos.ToString("0");
            }
            else if (segundos < 10)
            {
                countdown.text = minutes.ToString() + ":0" + segundos.ToString("0");
            }
            else if (minutes < 10)
            {
                countdown.text = "0" + minutes.ToString() + ":" + segundos.ToString("0");
            }
            else
            {
                countdown.text = minutes.ToString() + ":" + segundos.ToString("0");
            }
        }
    }
}