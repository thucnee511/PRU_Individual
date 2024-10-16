using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collison){
        if(collison.gameObject.tag == "Player"){
            Destroy(this.gameObject);
            player.GetComponent<PlayerScript>().Point += 100;
            Debug.Log(player.GetComponent<PlayerScript>().Point);
        }
    }
}
