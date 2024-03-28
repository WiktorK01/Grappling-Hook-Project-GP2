using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    Image crosshairImage; 
    public Color defaultColor = Color.white;
    public Color targetColor = Color.red;
    public float maxDistance = 10000f; 
    public Vector3 GrapplePoint; 
    public bool isGrapplePointValid = false;
    
    public GrappleHookController GrappleHookController;

    private void Start(){
        crosshairImage = GetComponentInChildren<Image>();
        GrapplePoint = Vector3.zero;
        isGrapplePointValid = false;
    }

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        
        GrapplePoint = Vector3.zero;
        isGrapplePointValid = false;
        
        if(!GrappleHookController.isGrappling){
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Grappable"))
                {
                    crosshairImage.color = targetColor;

                    GrapplePoint = hit.point;
                    isGrapplePointValid = true;
                    Debug.Log("Grapple Point: " + GrapplePoint); 
                }
                else
                {
                    crosshairImage.color = defaultColor;
                }
            }
            else
            {
                crosshairImage.color = defaultColor;
            }
        }
    }
}
