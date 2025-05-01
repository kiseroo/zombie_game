using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] monsterReference;

    [SerializeField]
    private Transform leftPos, rightPos;

    private GameObject monster;

    private int randomIndex;
    private int randomSide;

    private int newSpeed = 0;
    private int moreMonsters = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());    
    }

    // Update is called once per frame
    private void OnEnable()
    {
        Player.gameOver += IncreaseSpeedListenerS;
    }

    private void OnDisable()
    {
        Player.gameOver -= IncreaseSpeedListenerS;
    }
    void IncreaseSpeedListenerS()
    {
        newSpeed += 20;
        moreMonsters = 2;
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, moreMonsters));

            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            monster = Instantiate(monsterReference[randomIndex]);

            if (randomSide == 0)
            {
                monster.transform.position = leftPos.position;
                monster.GetComponent<Monster>().speed = Random.Range(2, 10)+newSpeed;
            }
            else
            {
                monster.transform.position = rightPos.position;
                monster.GetComponent<Monster>().speed = -Random.Range(2, 10)-newSpeed;
                monster.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }







}
