using UnityEngine;
using System.Collections;

/// <summary>
/// CannonAmmoPhysics class is used as method parameter contaning ammo physics.
/// </summary>
public class CannonAmmoPhysics 
{
	public float angleRad = 0f; 
	public Vector2 velocity = Vector2.zero;
	public Vector3 positionOfAction = Vector3.zero;

	/// <summary>
	/// CannonAmmoPhysics constructor with angle, velocity, and position
	/// </summary>
	/// <param name = 'angleRad'> Angle at which velocity is applied </param>
	/// <param name = 'velocity'> 2D velocity vector </param>
	/// <param name = 'positionOfAction'> position from where the action will originate </param>
	public CannonAmmoPhysics(float angleRad, Vector2 velocity, Vector3 positionOfAction)
	{
		this.angleRad = angleRad;
		this.velocity = velocity;
		this.positionOfAction = positionOfAction;
	}
}
