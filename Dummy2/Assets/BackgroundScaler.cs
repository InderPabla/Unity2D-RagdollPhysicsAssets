using UnityEngine;
using System.Collections;

public class BackgroundScaler : MonoBehaviour {

	private const float size = 5f; //size of the object with respect to this
	
	private float nowSize; //orthographic size of camera now
	
	private Vector3 initialScale; //initial scale of object
	private Vector3 initialPosition;

	private Transform mainCamera = null; //main camera transform

	void Start () 
	{
		this.mainCamera = Camera.main.transform; //get main camera transform
		initialScale = transform.localScale; //initial scale size of object
		initialPosition = transform.localPosition; //also the offset position
	}
	
	// Update is called once per frame
	void Update () 
	{
		nowSize = Camera.main.orthographicSize; //orthographic size of camera now
		
		//scale size and position of the object
		Vector3 newScale = new Vector3((nowSize/size)*initialScale.x,(nowSize/size)*initialScale.y,initialScale.z);
		transform.localScale = newScale;

		Vector3 newPosition = mainCamera.position;
		newPosition.y = initialPosition.y;
		newPosition.z = initialPosition.z;

		transform.position = newPosition;
	}
}
