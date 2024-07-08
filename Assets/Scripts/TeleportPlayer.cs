using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportPlayer : MonoBehaviour
{
    public Vector3 teleportLocation; // The location to which the player will be teleported
    public Button teleportButton; // Reference to the UI button

    private Transform xrRigTransform; // Reference to the XR Rig's transform

    void Start()
    {
        // Find the XR Rig in the scene by name and get its transform
        var xrRig = GameObject.Find("XR Origin (XR Rig)");
        if (xrRig != null)
        {
            xrRigTransform = xrRig.transform;
        }
        else
        {
            Debug.LogWarning("XR Rig not found. Ensure there is a GameObject named 'XR Origin (XR Rig)' in the scene.");
        }

        // Ensure the teleportButton is assigned and add the teleport function to the button click listener
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(Teleport);
        }
    }

    void Teleport()
    {
        if (xrRigTransform != null)
        {
            xrRigTransform.position = teleportLocation; // Move the XR Rig to the teleport location
        }
        else
        {
            Debug.LogWarning("XR Rig transform reference is missing.");
        }
    }
}
