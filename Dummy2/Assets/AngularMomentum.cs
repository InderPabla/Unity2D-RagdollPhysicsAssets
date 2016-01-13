using UnityEngine;
using System.Collections;

public class AngularMomentum : MonoBehaviour {
	public float velocity = 1f;
	// Use this for initialization
	void Start () {
		rigidbody2D.angularVelocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
