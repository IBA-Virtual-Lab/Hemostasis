using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

/*
    Used by PlayerCapsule to interact with Interactable objects.
    It uses a raycast to detect if the player is looking at an Interactable object.
    If the player is looking at an Interactable object, 
    the player can press the interact button to interact with the object.

    It's important to note that this script is attached to the PlayerCapsule object,
    and not the Player object. PlayerCapsule has the camera attached to it,
    which is what we use to raycast.
*/

public class Playerinteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 6f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private StarterAssetsInputs inputManager;

    private bool keyin;

    private StarterAssetsInputs starterAssetsInputs;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        playerUI = GetComponent<PlayerUI>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if(interactable!=null)
            {
                playerUI.UpdateText(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                if(starterAssetsInputs.interact)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
