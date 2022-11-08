using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    float segundos = 1f;
    int minutos = 15;
    bool acabouTempo = false;
    bool comecouTimer = false;
    public GameObject GameOver;

    public Text countdown;

    void Start()
    {
        countdown.text = "15:00";
    }

    private void Update()
    {
        if (acabouTempo)
        {
            segundos = 0;
            countdown.text = "00:00";
            GameOver.SetActive(true);
        }
        else
        {
            if (comecouTimer)
            {
                if (minutos == 0 && segundos <= 0)
                {
                    acabouTempo = true;
                }
                else if (segundos <= 0)
                {
                    segundos = 59f;
                    minutos--;
                }

                segundos -= 1 * Time.deltaTime;

                if (segundos < 10 && minutos < 10)
                {
                    countdown.text = "0" + minutos.ToString() + ":0" + segundos.ToString("0");
                }
                else if (segundos < 10)
                {
                    countdown.text = minutos.ToString() + ":0" + segundos.ToString("0");
                }
                else if (minutos < 10)
                {
                    countdown.text = "0" + minutos.ToString() + ":" + segundos.ToString("0");
                }
                else
                {
                    countdown.text = minutos.ToString() + ":" + segundos.ToString("0");
                }
            }
            else
            {
                segundos -= 1 * Time.deltaTime;
                if(segundos < 0)
                {
                    segundos = 59;
                    minutos--;
                    comecouTimer = true;
                }
            }
        }
    }
}