using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour 
{
	public Sprite[] stars = new Sprite[4];
	public int index = 0;
	private SpriteRenderer spriteRenderer;
	private bool loaded = false;
	public bool load = true;
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		loaded = true;
		if(load == true)
		{
			spriteRenderer.sprite = stars[index];
		}
	}

	void Update () 
	{
	
	}

	public void setStar(int index)
	{
		//Debug.Log(index);
		this.index = index;
		if(loaded == true)
			spriteRenderer.sprite = stars[index];
		else
			Invoke("setStar",0.1f);
	}

	public void setStar()
	{
		if(loaded == true)
			spriteRenderer.sprite = stars[index];
		else
			Invoke("setStar",0.1f);
	}
}
