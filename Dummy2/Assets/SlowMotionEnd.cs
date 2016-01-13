using UnityEngine;
using System.Collections;

/// <summary>
/// SlowMotionEnd class changes colour of its object over time from white to light-cyan transparent blue.
/// Almost like a Lighting flash.
/// </summary>
public class SlowMotionEnd : MonoBehaviour {

	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		renderer.material.color  = new Color(1f,1f,1f,1f); //color white, no transparency
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		Color colour = renderer.material.color; //get color

		//animate colors
		float red = updateColor(colour.r,0f,-1);
		float green = updateColor(colour.g,0.75f,-0.5f);
		float blue = updateColor(colour.b,0.90f,-0.5f);
		float alpha = updateColor(colour.a,0.25f,-2);

		renderer.material.color  = new Color(red,green,blue,alpha);
	}

	/// <summary>
	/// Method changes a given colour over time to another color
	/// </summary>
	/// <param name = 'colour'> Current colour </param>
	/// <param name = 'stopColour'> Color to stop at </param>
	/// <param name = 'amountPerTime'> Scale to add to colour over time </param>
	/// <returns> Colour calculated </returns>
	float updateColor(float colour,float stopColour, float amountPerTime)
	{
		colour += amountPerTime*Time.deltaTime; //add amount to color over Time.deltTime

		//if out of count return stop color
		if(amountPerTime>0)
		{
			if(colour>=stopColour)
				return stopColour;
		}
		else if(amountPerTime<0)
		{
			if(colour<=stopColour)
				return stopColour;
		}

		return colour;
	}

}
