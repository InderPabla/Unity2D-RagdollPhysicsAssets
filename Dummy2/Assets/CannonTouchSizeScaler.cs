using UnityEngine;
using System.Collections;

public class CannonTouchSizeScaler : MonoBehaviour {

	private BoxCollider2D boxCollider;
	private Vector2 initialSize = Vector2.zero;
	private const float size = 5f; //size of the object with respect to this
	
	private float nowSize; //orthographic size of camera now

	void Start () 
	{
		boxCollider = collider2D.GetComponent<BoxCollider2D>();
		initialSize = boxCollider.size;

	}

	void Update () 
	{
		nowSize = Camera.main.orthographicSize; //orthographic size of camera now

		Vector2 newSize = new Vector2((nowSize/size)*initialSize.x,(nowSize/size)*initialSize.y);
		boxCollider.size = newSize;
	}
}
