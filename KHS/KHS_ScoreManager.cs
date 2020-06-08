using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KHS_ScoreManager : MonoBehaviour {
    public static KHS_ScoreManager instance;
    private void Awake()
    {
        instance = this;
        Score = 0;
    }
    private int _score;
    public Text ScoreText;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            ScoreText.text = _score.ToString();
        }
    }
}
