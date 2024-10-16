using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public GameObject Player { get; set; }
    public float Start { get; set; }
    public float End { get; set; }
    private bool facingLeft = true;

    private bool isDead = false;
    // Update is called once per frame
    void Update()
    {
        //normal movement
        if (!isDead)
        {
            var slimePosition = transform.position.x;
            if (slimePosition < Start)
            {
                facingLeft = false;
            }
            if (slimePosition > End)
            {
                facingLeft = true;
            }
            if (facingLeft)
            {
                transform.localScale = new Vector3(2, 2, 1);
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 1.5f;
            }
            else
            {
                transform.localScale = new Vector3(-2, 2, 1);
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 1.5f;
            }

            //chase player
            if (Player != null)
            {
                var playerX = Player.transform.position.x;
                if (playerX >= Start && playerX <= End)
                {
                    if (playerX > slimePosition)
                    {
                        facingLeft = false;
                    }
                    if (playerX < slimePosition)
                    {
                        facingLeft = true;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Left") || collider.gameObject.CompareTag("Right"))
        {
            facingLeft = !facingLeft;
            transform.localScale = new Vector3(transform.localScale.x * -1, 2, 1);
        }
    }

    public void DestroySlime()
    {
        Destroy(gameObject);
    }

    public void SetDieTrigger()
    {
        GetComponent<Animator>().SetTrigger("die");
        isDead = true;
    }
}
