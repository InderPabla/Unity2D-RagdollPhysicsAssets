using UnityEngine;
using System.Collections;

public class BackgroundColorChanger : MonoBehaviour {

	public Color startColor;
	public Color endColor;
	public float duration = 7f; // duration in seconds
	private float t = 0f;
	private bool status = false;


	void Update () 
	{

		if(status == false)
		{
			renderer.material.color = Color.Lerp(startColor, endColor, t);
			if (t < 1)
			{ 
				t += Time.deltaTime/duration;
			}
			else
			{
				status = true;
				t = 0;
			}
		}
		else
		{
			renderer.material.color = Color.Lerp(endColor, startColor, t);
			if (t < 1)
			{ 
				t += Time.deltaTime/duration;
			}
			else
			{
				status = false;
				t = 0;
			}
		}

	}
}
