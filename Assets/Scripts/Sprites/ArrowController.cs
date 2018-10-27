using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public float speed;
    public Direction direc;
    private Animator anim;

    public GameObject partSys;
    public GameObject trailEffect;
    public GameObject currentTrail;

	void Start () {
        //currentTrail = Instantiate(trailEffect, transform.position, Quaternion.identity);
        //currentTrail.transform.parent = transform;
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
        GameObject effect = Instantiate(partSys, transform.position, Quaternion.identity);
        //currentTrail.transform.parent = transform.parent.parent;
        var main = effect.GetComponent<ParticleSystem>().main;
        main.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color ;
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
