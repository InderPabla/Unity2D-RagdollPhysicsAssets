using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	private float particleDecrease = 20f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Decrease particle emission
		particleEmitter.minEmission -= particleDecrease*Time.deltaTime; 
		particleEmitter.maxEmission -= particleDecrease*Time.deltaTime;

	}
}
