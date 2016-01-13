using UnityEngine;
using System.Collections;

/// <summary>
/// CannonBallTrio class is a script attached to an object which contains 3 smaller cannon balls.
/// </summary>
public class CannonBallTrio : MonoBehaviour {

	private bool childrenLoaded = false; //false = children not loaded, true = children loaded
	private float[] scalarQuantity = {1f,0.8f,0.9f}; //scaling velocities of the 3 child cannon balls

	private Transform mainCamera; //Transform of the current camera

	private const string TRACK_OBJECTS_METHOD = "trackObjects"; //Method in CameraTracker to track this object
	private const string CHANGE_DAMAGE_FORCES = "changeDamageForces"; //Method in CannonBallSingle to change damage forces to destroy an object

	private float damageForce_1 = 1000f;
	private float damageForce_2 = 500f;
	private float damageForce_3 = 200f;
	private float damageForce_4 = 100f;
	/*private float damageForce_1 = 2000f;
	private float damageForce_2 = 1000f;
	private float damageForce_3 = 500f;
	private float damageForce_4 = 250f;*/
	/*private float damageForce_1 = 400f;
	private float damageForce_2 = 200f;
	private float damageForce_3 = 100f;
	private float damageForce_4 = 50f;*/

	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		mainCamera = Camera.main.transform;
	}

	/// <summary>
	/// Method is called from CannonAmmoHandler to set ammo physics.
	/// </summary>
	/// <param name = 'ammoPhysics'> Physics to be set upon this object. </param>
	public void fireAmmo(CannonAmmoPhysics ammoPhysics)
	{
		Transform[] childArray = new Transform[3]; 
		transform.position = ammoPhysics.positionOfAction;

		//set physics to the 3 child objects
		for(int i=0;i<3;i++)
		{
			Vector2 velocity = ammoPhysics.velocity;
			velocity = new Vector2(velocity.x*scalarQuantity[i],velocity.y*scalarQuantity[i]);
			Transform child = transform.GetChild(i);
			child.rigidbody2D.velocity = velocity;

			child.SendMessage(CHANGE_DAMAGE_FORCES,new float[] {damageForce_1,damageForce_2,damageForce_3,damageForce_4});
			childArray[i] = child;
		}
		mainCamera.SendMessage(TRACK_OBJECTS_METHOD,childArray); //inform CameraTracker to track these 3 objects
	}
}
