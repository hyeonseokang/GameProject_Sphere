using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_Timer : MonoBehaviour {
    private int Time;
    int Time2 = 0;
    private void Awake()
    {
        Time = 0;
        StartCoroutine(Timego());
        for(int i=0;i<3;i++)
        {
            TimeTextNum[i] = 0;
        }
    }
    public int getTime()
    {
        return Time;
    }
    IEnumerator Timego()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Time++;
            Time2++;
            if (Time2 == 10)
            {
                TimeTextNum[2]++;
                Time2 = 0;
            }
            if(TimeTextNum[2]==6)
            {
                TimeTextNum[2] = 0;
                TimeTextNum[1]++;
            }
            if(TimeTextNum[1]==10)
            {
                TimeTextNum[1] = 0;
                TimeTextNum[0]++;
            }
            TimeText.text = TimeTextNum[0].ToString() + TimeTextNum[1].ToString() + " : " + TimeTextNum[2].ToString() + Time2.ToString();
        }
    }

    int[] TimeTextNum = new int[3];
    public Text TimeText;
}
