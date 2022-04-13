using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimerScript : MonoBehaviour
{
	public TMP_Text TimerText;
	public bool playing;
	public bool reset;
	private float Timer;

	void Update()
	{

		if (playing == true)
		{

			Timer += Time.deltaTime;
			int minutes = Mathf.FloorToInt(Timer / 60F);
			int seconds = Mathf.FloorToInt(Timer % 60F);
			int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
			TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
		}

		if (reset == true)
        {
			Timer = 0;
			TimerText.text = ("00" + ":" + "00"  + ":" + "00");
		}
		

	}



}
