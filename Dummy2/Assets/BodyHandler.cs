using UnityEngine;
using System.Collections;

/// <summary>
/// BodyHandler class handles body's damage models and informing GameMaster when this body is dead.
/// </summary>
public class BodyHandler : MonoBehaviour 
{
	public GameObject scorePopUpPrefab; 

	private int health = 10; //max health is 10
	private int deadHealth = 0; //This body dies at 0 health
	private bool dead = false; //false = alive, true = dead
	private int scorePerDestroy = 100;

	private const string ENTITY_DEATH_INFORM_METHOD = "entityDeathInform"; //method in GameMaster to inform when health = 0
	private const string ADD_ENEMY_DAMAGE_SCORE_METHOD = "addEnemyDamageScore"; //Method in ScoreTracker to increment enemy damage score
	private const string ANIMATE_POP_UP_METHOD = "animatePopUp";
	private const string STOP_AUDIO_METHOD = "stopAudio"; //stop audio method

	public GameObject gameMaster; //GameMaster object that is currently in the scene.
	
	/// <summary>
	/// Deals damage to the body and checks if body is dead.
	/// </summary>
	/// <param name = 'damageAmount'> Damage amount to subtract from health </param>
	public void damage(int damageAmount)
	{
		gameMaster.SendMessage(ADD_ENEMY_DAMAGE_SCORE_METHOD,scorePerDestroy);
		//playAudio();
		//If body is still alive
		if(dead == false)
		{
			health -= damageAmount; //deal damage

			//If body is dead
			if(health <= deadHealth)
			{
				dead = true;
				gameMaster.SendMessage(ADD_ENEMY_DAMAGE_SCORE_METHOD,scorePerDestroy*2);
				gameMaster.SendMessage(ENTITY_DEATH_INFORM_METHOD); //send message to GameMaster informing of death

				GameObject scorePopUp = Instantiate (scorePopUpPrefab) as GameObject;

				Vector3 popUpPosition = Vector3.zero;
				for(int i = 0 ;i<6;i++)
				{
					popUpPosition.x += transform.GetChild(i).position.x;
					popUpPosition.y += transform.GetChild(i).position.y;
				}
				popUpPosition.x /= 6f;
				popUpPosition.y /= 6f;

				ScorePopUp popUp = new ScorePopUp(popUpPosition,(scorePerDestroy*2)+"");
				scorePopUp.SendMessage(ANIMATE_POP_UP_METHOD,popUp);
			}
		}
	}

	public void playAudio()
	{
		if(!audio.isPlaying)
		{
			this.GetComponent<AudioSource>().Play();
			Invoke(STOP_AUDIO_METHOD,0.25f);
		}
	}
	
	public void stopAudio()
	{
		this.GetComponent<AudioSource>().Stop();
	}
}
