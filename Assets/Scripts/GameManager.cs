using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    int round = 0;
    int next = 0;
    Vector3 bounds;
    float minX, minY, maxX, maxY;

    public GameObject[] enemies;
    public GameObject[] upgrades;
    public Text[] UI;
    public GameObject enemyContainer;

    // Start is called before the first frame update
    void Start()
    {
        bounds = gameObject.GetComponent<Renderer>().bounds.size;
        minX = (bounds.x / 2) * -1;
        minY = (bounds.y / 2) * -1;
        maxX = bounds.x / 2;
        maxY = bounds.y / 2;
        StartCoroutine(NextRound());
        StartCoroutine(SpawnUpgrade());


    }

    // Update is called once per frame
    void Update()
    {
        if (enemyContainer.transform.childCount == 0 && next == 1)
        {
            next = 0;
            StartCoroutine(NextRound());

        }
     

    }

    void SpawnEnemies()
    {
        
        for (int i = 0; i < round; i++)
        {
            var x = Random.Range(minX, maxX);
            var y = Random.Range(minY, maxY);
            var choice = Random.Range(0, 3);
            var slime = Instantiate(enemies[choice], new Vector3(x, y, 0), enemies[choice].transform.rotation);
            slime.transform.parent = enemyContainer.transform; 
        }
        next = 1;


    }

    IEnumerator NextRound()
    {
        round += 1;
 
        UI[0].text = UI[1].text = "Round: " + round;
        UI[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        UI[1].gameObject.SetActive(false);
        SpawnEnemies();

    }

    IEnumerator SpawnUpgrade()
    {
        var time = Random.Range(30, 45);
        yield return new WaitForSeconds(time);
        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);
        var up = Random.Range(0, 3);      
        Instantiate(upgrades[up], new Vector3(x, y, 0), upgrades[up].transform.rotation);
        StartCoroutine(SpawnUpgrade());
    }
}
