using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    /// <summary>
    /// Array of all the back and forgrounds to be parallaxed
    /// </summary>
    public Transform[] Backgrounds;

    /// <summary>
    /// Propotion of the camera's movement to move the backgrounds by
    /// </summary>
    private float[] ParallaxScales;

    /// <summary>
    /// How smooth the parallax is going to be. Make sure to set this above 0.
    /// </summary>
    public float Smoothing = 1;

    /// <summary>
    /// A reference to the main camera's transform
    /// </summary>
    private Transform MainCameraTransform;

    /// <summary>
    /// The position of the camera in the previous frame
    /// </summary>
    private Vector3 PreviousCameraPosition;

    /// <summary>
    /// Called before Start()
    /// </summary>
    private void Awake ()
    {
        // set up camera reference
        MainCameraTransform = Camera.main.transform;
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start ()
    {
	    // the previous frame had the current frame's camera position
        PreviousCameraPosition = MainCameraTransform.position;

        ParallaxScales = new float[Backgrounds.Length];

        // assigning coresponding parallax scales
        for (var i = 0; i < Backgrounds.Length; i++)
        {
            ParallaxScales[i] = Backgrounds[i].position.z * -1;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update ()
    {
        for (var i = 0; i < Backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement because the previous frame multipliex by the scale
            var parallax = (PreviousCameraPosition.x - MainCameraTransform.position.x) * ParallaxScales[i];

            // set a target x position which is the current position plus the parallax
            var backgroundTargetPosX = Backgrounds[i].position.x + parallax;

            // create a target position which is the background's current position with it's target's x position
            var backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            // fade between current position and the target position using lerp
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
        }	

        // set the previous camera position and the end of the frame
        PreviousCameraPosition = MainCameraTransform.position;
    }
}
