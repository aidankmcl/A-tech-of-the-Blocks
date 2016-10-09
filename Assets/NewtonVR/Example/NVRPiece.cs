using UnityEngine;
using System.Collections;

namespace NewtonVR.Example
{
    public class NVRPiece : NVRInteractableItem
    {
        public Transform Piece;
        public Quaternion startingRotation;

        void Start()
        {
            //save the starting rotation
            startingRotation = this.transform.rotation;
        }

        public override void UseButtonDown()
        {
            base.UseButtonDown();
	    Quaternion rotationAmount = Quaternion.Euler(0, -90, 0);
            Quaternion postRotation = this.transform.rotation * rotationAmount;
            AttachedHand.TriggerHapticPulse(500, Valve.VR.EVRButtonId.k_EButton_Axis0);



        }
	
    }
}