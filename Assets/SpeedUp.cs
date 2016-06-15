using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {

	private bool IsSpeedy;
	
	// Update is called once per frame
	public void SpeedUpTime () 
	{
		if (IsSpeedy) {
			Time.timeScale = 1;
			IsSpeedy = false;
		}
	
		else{
			Time.timeScale = 5;
			IsSpeedy = true;
		}
	}
}
