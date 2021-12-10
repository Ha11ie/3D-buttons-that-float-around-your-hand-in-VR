using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeKeysType : MonoBehaviour {

	//connection to key make it a trigger
	//maybe make wand not a trigger?
	//connection to wand
	public GameObject RightWand;
	public GameObject AKey;
	private bool KeyOnSwitch;
	private bool WandOffSwitch;

	void Start () {
		//get box collider component of AKey
		KeyOnSwitch = AKey.GetComponent<BoxCollider>().isTrigger;
		WandOffSwitch = RightWand.GetComponent<SphereCollider>().isTrigger;
		//get sphere collider component of wand

	}
	
	private void OnTriggerEnter(Collider collider){

		KeyOnSwitch = true;

		WandOffSwitch = false;
	}
}
