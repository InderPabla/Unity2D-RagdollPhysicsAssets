using UnityEngine;
using System.Collections;

public class CannonBallCluster : MonoBehaviour {

	public Transform[] cannonBallNormalArray = new Transform[7];

	private Transform mainCamera; //Transform of the main camera
	
	private const string TRACK_OBJECTS_METHOD = "trackObjects"; //Method in CameraTracker to track this object
	private const string DAMAGE_METHOD = "damage"; //Method in ObjectDamage to damage the object by some level
	private const string WOOD_NAME = "Wood"; //Wood string name of destroyable objects
	
	private float damageForce_1 = 2000f;
	private float damageForce_2 = 1000f;
	private float damageForce_3 = 500f;
	private float damageForce_4 = 250f; 

	
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
		Transform[] childArray = new Transform[7]; 
		float angleStepDeg = 70f/7f;

		float startDeg = transform.eulerAngles.z - 35f;
		float velocitySqrMag = 15f;

		for(int i=6;i>=0;i--)
		{
			Transform child = cannonBallNormalArray[i];
			float angleRad = Mathf.Deg2Rad*startDeg;
			Vector2 velocity = new Vector2(Mathf.Cos(angleRad)*velocitySqrMag,Mathf.Sin(angleRad)*velocitySqrMag);
			child.rigidbody2D.velocity = velocity;
			startDeg += angleStepDeg;
		}

		Camera.main.SendMessage(TRACK_OBJECTS_METHOD,cannonBallNormalArray); //inform CameraTracker to track these 7 objects
	}
	
}

