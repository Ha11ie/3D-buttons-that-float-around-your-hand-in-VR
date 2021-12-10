using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WandFunctionsLeft : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject selectedThingL = null;
    private bool haveSomethingL = false;
    private bool triggerDownL = false;
    private bool inSomethingL = false;

    public TextMeshPro inThingStatusL;
    public TextMeshPro triggerStatusL;
    public TextMeshPro debugLeft;
    public TextMeshPro hasSomethingStatusL;
    public TextMeshPro selectedStatusL;

    private Transform origionalParentTransformL;
    public GameObject centralControlHere;
    private centralControl centralControlScriptHere;
    private GameObject origionalParentObjectL;

    private Renderer pickedUpRendererL;
    public Shader highLightShaderL;
    public Material highLightMaterialL;
    private Material OldMaterialL;


    void Start()
    {
        statusUpdate();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        centralControlScriptHere = centralControlHere.GetComponent<centralControl>();
    }



    void Update()
    {
        if (controller == null)
        {
            Debug.Log("controller not initialized");
            return;
        }
        statusUpdate();

        /*if (controller.GetPressDown(gripButton))
        {
            debugLeft.text += "\n GRIPPPPPIIIIINNNNNGGG! ";
            triggerDown = true;
            statusUpdate();
            if (selectedThing && inSomething && !haveSomething)
            {
                origionalParentTransform = selectedThing.transform.parent;
                debugLeft.text += "\n origionalParentTransform: " + origionalParentTransform;
                origionalParentObject = selectedThing.transform.parent.gameObject;
                debugLeft.text += "\n origionalParentObject: " + origionalParentObject;
                debugLeft.text += "\n this.transform: " + this.transform;
                selectedThing.transform.parent = this.transform;
                selectedThing.GetComponent<Rigidbody>().isKinematic = true;
                triggerDown = true;
                haveSomething = true;
                inSomething = true;
                statusUpdate();
            }
            statusUpdate();
        }*/

        /*if (controller.GetPressUp(gripButton))
        {
            //selectedThing = null;
            triggerDown = false;
            inSomething = false;
            statusUpdate();

            if (selectedThing && haveSomething)
            {
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

        }*/
    }


    private void OnTriggerEnter(Collider collider)
    {
        inSomethingL = true;
        statusUpdate();
        {
            if (!haveSomethingL && !triggerDownL)
            {
                selectedThingL = collider.gameObject;
                //make it bright
                pickedUpRendererL = selectedThingL.GetComponent<Renderer>();
                OldMaterialL = pickedUpRendererL.material;
                pickedUpRendererL.material = highLightMaterialL;
                if (selectedThingL.transform.parent == null)
                {
                    debugLeft.text = "\n parent is Null: ";
                }
            }
            //debugTextRight.text += "\n collider.gameObject: " +collider.gameObject ;
            debugLeft.text = "\n selected: " + selectedThingL;
            //debugTextRight.text += "\n selected transform parent: " + selectedThing.transform.parent;

            statusUpdate();

        }
        inSomethingL = true;
        statusUpdate();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!triggerDownL)
        {
            inSomethingL = false;
            // if it was a different color put it back
            pickedUpRendererL.material = OldMaterialL;
            //if nothing is grabbed 
            selectedThingL = null;
        }
        else
        {
            hasSomethingStatusL.text = "might have something in hand or empty triggerring";
            pickedUpRendererL.material = OldMaterialL;
        }

        statusUpdate();
    }

    void statusUpdate()
    {
        if (inSomethingL) { inThingStatusL.text = "in"; }
        else { inThingStatusL.text = "out"; }
        if (selectedThingL) { selectedStatusL.text = "selected"; }
        else { selectedStatusL.text = "no selectedThing"; }
        if (haveSomethingL) { hasSomethingStatusL.text = "got something"; }
        else { hasSomethingStatusL.text = "got nothing"; }
        if (triggerDownL) { triggerStatusL.text = "Trigger down"; }
        else { triggerStatusL.text = "trigger up"; }
    }

}
