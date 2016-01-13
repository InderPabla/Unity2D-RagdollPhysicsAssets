using UnityEngine;
using System.Collections;

/// <summary>
/// BodyPartPhysics class handles a single body part's physics model by checking its HingJoint2D reaction forces.
/// </summary>
public class BodyPartPhysics : MonoBehaviour 
{
	private bool broken = false; //false = not borken, true = broken
	private HingeJoint2D joint; //This body's joint

	private const string DAMAGE_METHOD = "damage"; //Method in BodyHandler to deal with damage

	public int damage = 10; //If joint breaks this body recieves this much damage 
	public float breakForce = 250000f; //Reaction force required to break joint
	public GameObject bloodSplat = null; //Blood to release after part break 
	
	/// <summary>
	/// Initialize components
	/// </summary>
	void Start () 
	{
		joint = GetComponent<HingeJoint2D>();
	}

	/// <summary>
	/// FixedUpdate is called once Time.deltaTime passed
	/// </summary>
	void FixedUpdate () 
	{
		//if part is not broken
		if(broken == false)
		{
			Vector2 forceOnJoint = Vector2.zero; //set force on the joint to 0
			forceOnJoint = joint.GetReactionForce (Time.deltaTime); //get force currently on the joint

			float forceOnJointMagnitude = forceOnJoint.sqrMagnitude; //magnitude of the 2D force

			//if force is greater then the break amount
			if(forceOnJointMagnitude>breakForce)
			{
				broken = true; //this part is now broken
				Destroy(joint); //destroy HingeJoint2D component on this object

				//Create blood where this object currently is
				GameObject blood = Instantiate(bloodSplat,transform.position,transform.rotation) as GameObject;

				transform.parent.SendMessage(DAMAGE_METHOD,damage); //send message to parent (BodyHandler) to deal damage on the body
			}
		}
	}

	public void breakPart()
	{
		if(broken == false)
		{
			broken = true; //this part is now broken
			Destroy(joint); //destroy HingeJoint2D component on this object
			//Create blood where this object currently is
			GameObject blood = Instantiate(bloodSplat,transform.position,transform.rotation) as GameObject;
			transform.parent.SendMessage(DAMAGE_METHOD,damage); //send message to parent (BodyHandler) to deal damage on the body
			rigidbody2D.velocity = Vector2.zero;
		}
	}


}
