using UnityEngine;
using System.Collections;

/// <summary>
/// WorldPhysics class handles initializations and changes in world information.
/// </summary>
public class WorldPhysics : MonoBehaviour 
{
	public float gravity = -9.81f; //gravity of the world (-9.81f  = Earth)
	public float timeScale = 1; //time scale of the world (1 = normal, 0.5 = 2x slower, 0.25 = 4x slower)
	public float deltaTime = 0.02f;
	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		Time.timeScale = this.timeScale; //set time scale
		Time.fixedDeltaTime = deltaTime;
		Physics2D.gravity = new Vector2(0,gravity); //set gravity (x-axis will always be 0)

	}

	/// <summary>
	/// Method changes world gravity (y-axis) with new gravity.
	/// </summary>
	/// <param name = 'newGravity'> New gravity value. </param>
	public void changeGravity(float newGravity)
	{
		Physics2D.gravity = new Vector2(0,newGravity); //change gravity
	}

	/// <summary>
	/// Method changes world time scale with new time scale.
	/// </summary>
	/// <param name = 'newTimeScale'> New time scale value. </param>
	public void changeTimeScale(float newTimeScale)
	{
		Time.timeScale = newTimeScale; //change time scale
		Time.fixedDeltaTime = 0.015f;
	}
}
