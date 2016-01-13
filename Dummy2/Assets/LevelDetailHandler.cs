using UnityEngine;
using System.Collections;
using System.IO;

public class LevelDetailHandler 
{

	private string filePath = null;
	private string fileName = "Ragdoll_Settings.txt";

	public float[] levelScores = new float[20];

	private float[,] levelScoresTemplate = new float[,] 
	{ //levels
		{ //level 1 
		 	100,500,600 }, //no change required
		{ //level 2		 
			100,1700,1800 }, //no change required
		{ //level 3		 
			100,2000,2400 }, //no change required
		{ //level 4		 
			100,3100,3300 }, //no change required
		{ //level 5		 
			100,2900,3400 }, //no change required
		{ //level 6		 
			100,1200,2200 }, //no change required
		{ //level 7		 
			100,3000,4000 }, //no change required
		{ //level 8		 
			100,4500,5000 }, //no change required
		{ //level 9		 
			100,2500,3000 }, //no change required
		{ //level 10	    
			100,3000,4000 }, //no change required
		{ //level 11	    
			100,3000,3500 }, //no change required
		{ //level 12	    
			100,3000,4000 }, //no change required
		{ //level 13	    
			100,1000,1500 }, //no change required
		{ //level 14	    
			100,4000,5500 }, //no change required
		{ //level 15	    
			100,3000,4000 }, //no change required
		{ //level 16	    
			100,7000,8000 }, //no change required
		{ //level 17	     
			100,5000,6000 }, //no change required
		{ //level 18	    
			100,2000,3000 }, //no change required
		{ //level 19	    
			100,8500,10000 }, //no change required
		{ //level 20	    
			100,10000,11000 }
	};


	public LevelDetailHandler ()
	{
		filePath = Application.persistentDataPath + "/" + fileName;
		if(!File.Exists(filePath))
		{
			string[] linesToWrite = new string[20];
			for(int i=0;i<20;i++)
			{
				linesToWrite[i] = "0";
				levelScores[i] = 0;
			}
			File.WriteAllLines(filePath,linesToWrite);

			//Debug.Log(filePath);
		}
		else
		{
			string[] linesToRead = File.ReadAllLines(filePath);
			for(int i=0;i<20;i++)
			{
				levelScores[i] = float.Parse(linesToRead[i]);
			}
		}
	}

	public int getStarIndex (int level)
	{
		int levelIndex = level-1;
		//Debug.Log(levelIndex);
		float score = levelScores[levelIndex];

		if (score>=levelScoresTemplate[levelIndex,2])
		{
			return 3;
		}
		else if (score>=levelScoresTemplate[levelIndex,1])
		{
			return 2;
		}
		else if (score>=levelScoresTemplate[levelIndex,0])
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	public int getStarIndex (LevelDetail detail)
	{
		float score = detail.score;
		int levelIndex = detail.level-1;

		if(score>levelScores[levelIndex])
		{
			levelScores[levelIndex] = score;
			save ();
		}

		if (score>=levelScoresTemplate[levelIndex,2])
		{
			return 3;
		}
		else if (score>=levelScoresTemplate[levelIndex,1])
		{
			return 2;
		}
		else if (score>=levelScoresTemplate[levelIndex,0])
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	public void save()
	{
		string[] linesToWrite = new string[20];
		for(int i=0;i<20;i++)
		{
			linesToWrite[i] = levelScores[i]+"";
		}

		File.WriteAllLines(filePath,linesToWrite);
	}

	public void delete()
	{
		if(File.Exists(filePath))
		{
			File.Delete(filePath);
		}
	}


}
