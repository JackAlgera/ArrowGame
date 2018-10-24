using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public float speed;
    public Direction direc;
    private Animator anim;

	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void Move()
    {
        transform.Translate(0f, -speed * Time.deltaTime, 0f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnClickDestroy()
    {
        speed = 0;
        anim.SetTrigger("Destroy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ArrowCollector")
        {
            GameController.instance.EndGame();
        }
    }
}
