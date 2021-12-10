using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeALetter : MonoBehaviour {
	
	public TextMeshPro CurrentPaperText;
	public TextMeshPro LetterOfThisKey;
	public int number;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void OnTriggerEnter (Collider collider){
		CurrentPaperText.text += LetterOfThisKey.text;
	}


}
