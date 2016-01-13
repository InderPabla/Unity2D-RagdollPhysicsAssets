using UnityEngine;
using System.Collections;

public class LineSizeScaler : MonoBehaviour {
	private const float size = 5f; //size of the object with respect to this
	
	private float nowSize; //orthographic size of camera now

	private LineRenderer line = null;
	private float initialWidth;
	private Transform mainCamera = null; //main camera transform

	void Start () 
	{
		this.mainCamera = Camera.main.transform; //get main camera transform
		line = GetComponent<LineRenderer>();

	}
	

	void Update () 
	{
	
	}
}
