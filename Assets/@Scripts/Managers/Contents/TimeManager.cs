using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private int minute;
    private int second;

    [SerializeField] float time;
    [SerializeField] float currentTime;
    [SerializeField] Text timeText;    

    void Start()
    {
        currentTime = time;
        StartCoroutine(Timer());
    }


    public IEnumerator Timer()
    {
        while(currentTime > 0)
        { 
            currentTime -= Time.deltaTime;

            minute = (int)currentTime / 60;
            second = (int)currentTime % 60;

            timeText.text = minute.ToString("00") + ":" + second.ToString("00");            

            yield return null;

            if(currentTime <= 0)
            {
                currentTime = 0;
                timeText.text = "--:--";
                yield break;
            }
        }
    }

    
}
