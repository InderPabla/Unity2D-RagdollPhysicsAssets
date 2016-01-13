using UnityEngine;
using System.Collections;

/// <summary>
/// CannonBallSingle class is a script attached to a single cannon ball.
/// </summary>
public class CannonBallSingle : MonoBehaviour {

	private Transform mainCamera; //Transform of the main camera

	private const string TRACK_OBJECTS_METHOD = "trackObjects"; //Method in CameraTracker to track this object
	private const string DAMAGE_METHOD = "damage"; //Method in ObjectDamage to damage the object by some level
	private const string WOOD_NAME = "Wood"; //Wood string name of destroyable objects

	private float damageForce_1 = 400f;
	private float damageForce_2 = 200f;
	private float damageForce_3 = 100f;
	private float damageForce_4 = 50f;
	/*private float damageForce_1 = 2000f;
	private float damageForce_2 = 1000f;
	private float damageForce_3 = 500f;
	private float damageForce_4 = 250f;*/

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
		transform.position = ammoPhysics.positionOfAction; //postion set to position of action
		transform.rigidbody2D.velocity = ammoPhysics.velocity; //ridigbody2D velocity to given ammo velocity
		mainCamera.SendMessage(TRACK_OBJECTS_METHOD,new Transform[]{transform}); //inform CameraTracker to track this object
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//collision material is wood
		if(collision.collider.name.Contains("Wood"))
		{
			bool damaged = true;
			int damageLevel = 0;
			float otherMass = 0; // other object's mass
			
			if (collision.rigidbody)
				otherMass = collision.rigidbody.mass;
			else 
				otherMass = 10; // static collider means huge mass

			//float force = collision.relativeVelocity.sqrMagnitude * rigidbody2D.mass;
			float force = rigidbody2D.velocity.sqrMagnitude * rigidbody2D.mass;

			//damage forces on this object from highest to lowest
			if(force>damageForce_1)
			{
				damageLevel = 4; 
			}
			else if(force>damageForce_2)
			{
				damageLevel = 3;
			}
			else if (force>damageForce_3)
			{
				damageLevel = 2;
			}
			else if (force>damageForce_4)
			{
				damageLevel = 1;
			}
			else
			{
				damaged = false;
			}

			if(damaged == true)
			{
				collision.collider.SendMessage(DAMAGE_METHOD,damageLevel);
			}
		}
	}

	/// <summary>
	/// Method changes damage forces.
	/// </summary>
	/// <param name = 'damageForces'> New damages to set. </param>
	public void changeDamageForces(float[] damageForces)
	{
		this.damageForce_1 = damageForces[0];
		this.damageForce_2 = damageForces[1];
		this.damageForce_3 = damageForces[2];
		this.damageForce_4 = damageForces[3];
	}


}
