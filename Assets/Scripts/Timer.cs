using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Timer : MonoBehaviour {
 
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;

	private float previoustime;

	private Text timerText;
 
	void Start () {
		minute = 0;
		seconds = 0f;
		previoustime = 0f;
		timerText = GetComponentInChildren<Text> ();
	}
 
	void Update () {
		seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}
		if((int)seconds != (int)previoustime) {
			timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
		}
		previoustime = seconds;
	}
}