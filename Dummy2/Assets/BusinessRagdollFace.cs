using UnityEngine;
using System.Collections;

public class BusinessRagdollFace : MonoBehaviour {
	public Sprite[] faceSprites = new Sprite[9];
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () 
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		int index = Random.Range(0,8);
		spriteRenderer.sprite = faceSprites[index];
		//this.transform.localScale = new Vector3(0.62f,0.62f,0);
	}

	
	// Update is called once per frame
	void Update () 
	{

	}
}
