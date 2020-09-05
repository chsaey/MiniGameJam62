using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    int level = 1;
    Vector3 bounds;
    float minX,minY,maxX,maxY;
    public GameObject[] enemies;
    public GameObject enemyContainer;
    // Start is called before the first frame update
    void Start()
    {
        bounds = gameObject.GetComponent<Renderer>().bounds.size;
        minX = (bounds.x / 2) *-1;
        minY = (bounds.y / 2) *-1;
        maxX = bounds.x / 2;
        maxY = bounds.y / 2;
        Spawn();

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void Spawn()    
    {
       for(int i = 0; i < level; i++) {
            var x = Random.Range(minX, maxX);
            var y = Random.Range(minY, maxY);
            var choice = Random.Range(0, 2);
            var slime = Instantiate(enemies[0], new Vector3(x, y, 0), enemies[0].transform.rotation);
           
        }


    }
}
