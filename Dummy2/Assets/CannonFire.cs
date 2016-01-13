using UnityEngine;
using System.Collections;

/// <summary>
/// CannonFire class is a script attached to the CannonFire prefab and decreases its particle emission over time.
/// </summary>
public class CannonFire : MonoBehaviour {

	private float particleDecrease = 250f; //factor to decrease over time
	private Vector3 followPosition; //position to follow
	private bool follow = false; //false = do not follow, true = follow
	private Transform followTransform = null; //Transform to follow

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () {

		//Decrease particle emission
		particleEmitter.minEmission -= particleDecrease*Time.deltaTime; 
		particleEmitter.maxEmission -= particleDecrease*Time.deltaTime;

		//if follow is true then follow position of given transform
		if(follow == true)
		{
			transform.position = followTransform.position;
		}

	}

	/// <summary>
	/// Method is called when objects postion needs to be changed occording to the given Transform
	/// </summary>
	/// <param name = 'followTransform'> Transform to follow </param>
	public void transformToFollow(Transform followTransform)
	{
		this.followTransform = followTransform;
		follow = true;
	}
}
