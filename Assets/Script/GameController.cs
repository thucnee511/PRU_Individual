using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private int slimeSpawned = 0;
    private float[] slimePosition = new float[] { 1f, 4f };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnSlime(2);
    }

    private void SpawnSlime(int number){
        if(slimeSpawned >= number) return;
        float randomX = slimePosition[slimeSpawned % 2];
        GameObject slime = (GameObject) Instantiate(Resources.Load("Prefabs/Slime"), new Vector3(randomX, -3.5f, 0), Quaternion.identity);
        slime.GetComponent<SlimeScript>().Start = randomX - 5;
        slime.GetComponent<SlimeScript>().End = randomX + 5;
        slime.GetComponent<SlimeScript>().Player = player;
        slimeSpawned++;
    }

    
}
