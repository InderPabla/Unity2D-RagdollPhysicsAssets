using UnityEngine;
using System.Collections;

public class JointStressAnalyzer : MonoBehaviour 
{
	private HingeJoint2D[] joints; 
	public float breakForce = 2000;
	// Use this for initialization
	void Start () 
	{
		joints = GetComponents<HingeJoint2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		int counter = 0;
		for(int i = 0;i<joints.Length;i++)
		{
			if(joints[i]!=null)
			{
				counter++;
				Vector2 forceOnJoint = Vector2.zero; //set force on the joint to 0
				forceOnJoint = joints[i].GetReactionForce (Time.deltaTime); //get force currently on the joint
				float forceOnJointMagnitude = forceOnJoint.magnitude;
				//Debug.Log(forceOnJointMagnitude);

				if(forceOnJointMagnitude>breakForce)
				{
					Destroy(joints[i]);
				}
			}
		}

		if(counter < joints.Length)
		{
			for(int i = 0;i<joints.Length;i++)
			{
				Destroy(joints[i]);
			}
			Destroy(GetComponent<JointStressAnalyzer>());
		}
	}
}
