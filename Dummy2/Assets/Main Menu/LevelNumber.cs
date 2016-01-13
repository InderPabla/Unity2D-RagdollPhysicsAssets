using UnityEngine;
using System.Collections;

public class LevelNumber : MonoBehaviour {
	private const string SET_STAR_METHOD = "setStar"; 
	TextMesh nameMesh;

	// Use this for initialization
	void Start () 
	{
		//nameMesh = GetComponent<TextMesh>();
		//nameMesh.name = "99";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void setName (string name)
	{	
		gameObject.name = name;
		transform.GetChild(0).GetComponent<TextMesh>().text = name;
	}

	public void setStar (int index)
	{
		transform.GetChild(1).SendMessage(SET_STAR_METHOD,index);
	}
}
