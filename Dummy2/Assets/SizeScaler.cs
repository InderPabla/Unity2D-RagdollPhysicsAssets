using UnityEngine;
using System.Collections;

/// <summary>
/// SizeScaler class scales objects to render on to screen with the same apparent size dispite changes in the camera's orthographic size.
/// </summary>
public class SizeScaler : MonoBehaviour 
{

	private const float size = 5f; //size of the object with respect to this

	private float nowSize; //orthographic size of camera now

	private Vector3 initialScale; //initial scale of object
	private Vector3 initialPosition; //initial position of object

	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		initialScale = transform.localScale; //initial scale size of object
		initialPosition = transform.localPosition; //also the offset position
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		nowSize = Camera.main.orthographicSize; //orthographic size of camera now

		//scale size and position of the object
		Vector3 newScale = new Vector3((nowSize/size)*initialScale.x,(nowSize/size)*initialScale.y,initialScale.z);
		transform.localScale = newScale;
			
		Vector3 newPosition = new Vector3((nowSize/size)*initialPosition.x,(nowSize/size)*initialPosition.y,initialPosition.z);
		transform.localPosition = newPosition;
	}
}
