using UnityEngine;
using System.Collections;

/// <summary>
/// CannonRicochet class is a script attached to the CannonBarrel prefab and animates the barrel kickback.
/// </summary>
public class CannonRicochet : MonoBehaviour {

	private float time = 0f; //current time
	private float timeMax = 0.25f; //time for each set of animation
	private bool animate = false; //false = do not animate, true = animate
	private bool state = false; //state of barrel

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () {

		//if animate is true then animate kickback
		if(animate==true)
		{
			//if state is false then move cannon barrel backwards (lasts for timeMax seconds)
			if(state==false)
			{
				transform.localPosition -= Time.deltaTime*transform.right;
				time+=Time.deltaTime;
				if(time>=timeMax)
				{
					time = 0;
					state = true;
				}
			}

			//if state is true then move cannon barrel forwards (lasts for timeMax seconds)
			else
			{
				transform.localPosition += Time.deltaTime*transform.right;
				time+=Time.deltaTime;
				if(time>=timeMax)
				{
					time = 0;
					state = false;
					animate = false;
				}
			}
		}
	}

	/// <summary>
	/// Method is called when barrel kickback needs to be animated
	/// </summary>
	public void cannonRicochetAnimate()
	{
		animate = true;
	}

}
