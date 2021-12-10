using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class backspace : MonoBehaviour {

	public TextMeshPro CurrentPaperText;

	public int number;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	private void OnTriggerEnter (Collider collider){
		if (CurrentPaperText.text.Length > 0){
			CurrentPaperText.text = CurrentPaperText.text.Substring(0, CurrentPaperText.text.Length -1);
		}
	}


}
