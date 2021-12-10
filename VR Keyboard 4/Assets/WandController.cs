using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WandController : MonoBehaviour {

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); }}
	private SteamVR_TrackedObject trackedObj;

	private GameObject pickup;
	private GameObject pickup2;

	private int intersectingThisMany =0;
	private bool alreadyPickedUp = false;

	public TextMesh debugText;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		debugText.text = "wow it worked";

	}

	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log ("controller not initialized");
			return;
		}

		if (controller.GetPressDown(triggerButton)) {
			if(pickup)
			{
				pickup.transform.parent = this.transform;
				pickup.GetComponent<Rigidbody>().isKinematic = true;
			}
			debugText.text +="\n1: in update Controller.GetPressDown gripbutton \n   pickup is: " + pickup;
		}

		if (controller.GetPressUp(triggerButton)) {
			pickup.transform.parent = null;
			debugText.text +="\n2 GetPressUp gripButton \n   pickup is: " + pickup;
		}

	}





	private void OnTriggerEnter(Collider collider){
		intersectingThisMany++;
		if (intersectingThisMany == 1) {
			debugText.text +="\n3 Trigger entered and pickup null. \nPickup = collider.gameObject: " + collider.gameObject ;
			pickup = collider.gameObject;
		} else if (intersectingThisMany == 2){
			debugText.text +="\n3b Trigger entered 2nd object. \nWhat's collider.gameObject?: " + collider.gameObject + "\n Pickup Is: " + pickup;
			pickup2 = collider.gameObject;
		}
	}

	private void OnTriggerExit(Collider collider){
		intersectingThisMany--;
		if (intersectingThisMany<0){debugText.text += "\n3.9 OOps intersecting less than 0 !!!!!";}
		//are you in another pickup still?
		if (intersectingThisMany == 0) 
		{
			pickup.transform.parent = null;
			pickup = null;
		}
		else if (intersectingThisMany==1){
			//which object did i exit?
			if (pickup == collider.gameObject) 
			{
				//switch pickups
				pickup = pickup2;
			}

			else if (pickup2 == collider.gameObject)
			{
				pickup2 = null;
			}
			debugText.text +="\n4 Trigger Exited \n intersecting How many? " + intersectingThisMany +"  \n What's collider.gameObject exited?: " + collider.gameObject;
		}
		debugText.text += "\n4b Trigger Exited end code \nWhat's collider.gameObject?: " + collider.gameObject;
	}


}
