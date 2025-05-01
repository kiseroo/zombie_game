using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = "score:"+score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Player.getCoin += CoinListener;
    }

    private void OnDisable()
    {
        Player.getCoin -= CoinListener;
    }

    void CoinListener()
    {
        score++;
        gameObject.GetComponent<Text>().text = "score:" + score.ToString();
    }
}
