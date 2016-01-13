using UnityEngine;
using System.Collections;

public class ScorePopUpAnimater : MonoBehaviour {
	private Vector3 startScale;
	private Vector3 endScale;
	private Vector3 startPosition;
	private Vector3 endPosition;

	private Color startColor;
	private Color endColor;
	
	private float time = 0f;
	private bool animate = false;
	private bool state = false;

	void Start () 
	{

		//animatePopUp();
	}

	void Update () 
	{
		if(animate == true)
		{
			if(state == false)
			{
				transform.localScale = Vector3.Lerp(startScale, endScale, time);
				transform.position = Vector3.Lerp(startPosition, endPosition, time);
				if (time < 1)
				{ 
					time += Time.deltaTime*1.25f;
				}
				else
				{
					state = true;
					time = 0f;
				}
			}
			else
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

	public void animatePopUp (ScorePopUp popUp)
	{
		GetComponent<TextMesh>().text = popUp.score;
		startScale = transform.localScale;
		endScale = startScale * 3f;
		endScale.z = 1f;
		
		startPosition = popUp.startPosition;
		endPosition = startPosition + new Vector3(0,2f,0);

		startColor = renderer.material.color;
		endColor = renderer.material.color;
		endColor.a = 0f;

		animate = true;
	}
}
