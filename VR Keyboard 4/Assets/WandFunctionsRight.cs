using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WandFunctionsRight : MonoBehaviour {

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); }}
	private SteamVR_TrackedObject trackedObj;

	private GameObject selectedThing = null;
	private GameObject selectedThing2 = null;

	private int intersectingThisMany =0;
	//private bool alreadyPickedUp = false;
    private bool haveSomething = false;
    private bool triggerDown = false;
    private bool inSomething = false;

    public TextMeshPro inThingStatus;
    public TextMeshPro triggerStatus;
    public TextMeshPro debugTextRight;
    public TextMeshPro hasSomethingStatus;
    public TextMeshPro selectedStatus;

    private Transform origionalParentTransform;
	public GameObject centralControlHere;
	private centralControl centralControlScriptHere;
	private GameObject origionalParentObject;

	private Renderer pickedUpRenderer;
	public Shader highLightShader;
	public Material highLightMaterial;
	private Material OldMaterial;

    void Start () {
        statusUpdate();
        trackedObj = GetComponent<SteamVR_TrackedObject> ();
		centralControlScriptHere = centralControlHere.GetComponent<centralControl>();
	}

	void Update () {
		if (controller == null) {
			Debug.Log ("controller not initialized");
			return;
		}
        statusUpdate();

        if (controller.GetPressDown (gripButton)) {
            triggerDown = true;
            statusUpdate();
            if (selectedThing && inSomething && !haveSomething) {
				origionalParentTransform = selectedThing.transform.parent;
				debugTextRight.text = "\n origionalParentTransform: " +origionalParentTransform ;
				origionalParentObject = selectedThing.transform.parent.gameObject;
				debugTextRight.text = "\n origionalParentObject: " +origionalParentObject ;
				debugTextRight.text = "\n this.transform: " +this.transform ;
                selectedThing.transform.parent = this.transform;
                selectedThing.GetComponent<Rigidbody> ().isKinematic = true;
				triggerDown = true;
                haveSomething = true;
                inSomething = true;
                statusUpdate();
            }
            statusUpdate();
        }

		if (controller.GetPressUp (gripButton)) {
            //selectedThing = null;
            triggerDown = false;
            inSomething = false;
            statusUpdate();

            if (selectedThing && haveSomething) {
                //selected.GetComponent<Rigidbody>().isKinematic = false;
                selectedThing.transform.parent = origionalParentObject.transform;
				triggerDown = false;
                selectedThing = null;
                inSomething = false;
                haveSomething = false;
                statusUpdate();
            }
            if (selectedThing) selectedThing = null;
            if (haveSomething) haveSomething = false;
            if (inSomething) inSomething = false;

            statusUpdate();

        }
	}

	private void OnTriggerEnter(Collider collider){
        inSomething = true;
        statusUpdate();
        {
            if (!haveSomething && !triggerDown)
            {
                selectedThing = collider.gameObject;
                //make it bright
                pickedUpRenderer = selectedThing.GetComponent<Renderer>();
                OldMaterial = pickedUpRenderer.material;
                pickedUpRenderer.material = highLightMaterial;
                if (selectedThing.transform.parent == null)
                {
                    debugTextRight.text += "\n parent is Null: ";
                }
            }
			//debugTextRight.text += "\n collider.gameObject: " +collider.gameObject ;
			debugTextRight.text = "\n selected: " + selectedThing;
			//debugTextRight.text += "\n selected transform parent: " + selectedThing.transform.parent;

            statusUpdate();

		}
        inSomething = true;
        statusUpdate();
    }

	private void OnTriggerExit(Collider collider)
    {
        if (!triggerDown)
        {
            inSomething = false;
            // if it was a different color put it back
            pickedUpRenderer.material = OldMaterial;
            //if nothing is grabbed 
            selectedThing = null;
        }
        else { hasSomethingStatus.text = "might have something in hand or empty triggerring";
            pickedUpRenderer.material = OldMaterial;
        }

        statusUpdate();
    }

    void statusUpdate()
    {
        if (inSomething) { inThingStatus.text = "in"; }
        else { inThingStatus.text = "out"; }
        if (selectedThing) { selectedStatus.text = "selected"; }
        else { selectedStatus.text = "no selectedThing"; }
        if (haveSomething) { hasSomethingStatus.text = "got something"; }
        else { hasSomethingStatus.text = "got nothing"; }
        if (triggerDown) { triggerStatus.text = "Trigger down"; }
        else { triggerStatus.text = "trigger up"; }
    }

}
