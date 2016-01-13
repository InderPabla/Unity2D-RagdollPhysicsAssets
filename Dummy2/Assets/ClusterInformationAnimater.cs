using UnityEngine;
using System.Collections;

public class ClusterInformationAnimater : MonoBehaviour {
	private Color startColor;
	private Color endColor;
	
	private float time = 0f;
	private bool animate = false;
	private bool state = false;

	public bool display = false;
	// Use this for initialization
	void Start () 
	{
		if(display == true)
		{
			LevelDetailHandler levelDetailHander = new LevelDetailHandler();
			if(levelDetailHander.getStarIndex(Application.loadedLevel) == 0)
			{
				GetComponent<SpriteRenderer>().enabled = true;
				startColor = renderer.material.color;
				endColor = renderer.material.color;
				endColor.a = 0f;
			}
			else
			{
				display = false;
				Destroy(gameObject);
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(display == true)
		{
			if(Input.GetMouseButtonDown(0))
			{
				//Destroy(gameObject);
				display = false;
				animate = true;

			}
		}

		if(animate == true)
		{
			renderer.material.color = Color.Lerp(startColor, endColor, time);
			if (time < 1)
			{ 
				time += Time.deltaTime*2f;
			}
			else
			{
				Destroy(gameObject);
			}

		}
	}	
}
