using UnityEngine;
using System.Collections;

/// <summary>
/// CannonAmmoHandler class handles ammo indexs and ammo firing.
/// </summary>
public class CannonAmmoHandler : MonoBehaviour 
{
	private int maxAmmoCount = 10; //max 10 ammo in one scene
	private int ammoCount = 0; //ammo given to this scene
	private int index = 0; //index of ammo to fire next

	private const string FIRE_AMMO_METHOD = "fireAmmo"; //Method in the obecjt to fire to deal with rigidbody2D physics

	public Transform[] cannonAmmos = new Transform[10]; //ammo objects

	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		//find number of ammo(s) given to this scene
		for(int i=0;i<maxAmmoCount;i++)
		{
			if(cannonAmmos[i] != null)
			{
				ammoCount++;
			}
			else
			{
				break;
			}
		}
	}

	/// <summary>
	/// Method updates ammo index and calls fire message in the current ammo.
	/// </summary>
	/// <param name = 'ammoPhysics'> Physics that the ammo is going follow. </param>
	public void fireCannonAmmo(CannonAmmoPhysics ammoPhysics)
	{
		if(ammoCount>0)
		{
			if(index<ammoCount)
			{
				cannonAmmos[index].SendMessage(FIRE_AMMO_METHOD,ammoPhysics);
				index++;
			}
		}
	}

	/// <summary>
	/// Method returns the number of ammos not fired.
	/// </summary>
	/// <returns>Total ammo count - index number of ammo that was fired</returns>
	public int getAmmoNotUsed()
	{
		return (ammoCount-index);
	}
}
