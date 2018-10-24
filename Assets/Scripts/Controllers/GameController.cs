using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

	void Start () {
		if(instance == null)
        {
            instance = this;
        }
	}
	
	void Update () {
		
	}
}
