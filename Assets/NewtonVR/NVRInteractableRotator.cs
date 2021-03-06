﻿using UnityEngine;
using System.Collections;

namespace NewtonVR
{
    public class NVRInteractableRotator : NVRInteractable
    {
        public float CurrentAngle;

        protected virtual float DeltaMagic { get { return 1f; } }
        protected Transform InitialAttachPoint;

        protected override void Awake()
        {
            base.Awake();
            this.Rigidbody.maxAngularVelocity = 100f;
        }

        protected override void Update()
        {
            base.Update();
            CurrentAngle = Quaternion.Angle(Quaternion.identity, this.transform.rotation);
        }

        public override void OnNewPosesApplied()
        {
            base.OnNewPosesApplied();

            if (IsAttached == true)
            {
                Vector3 PositionDelta = (AttachedHand.transform.position - InitialAttachPoint.position) * DeltaMagic;

                this.Rigidbody.AddForceAtPosition(PositionDelta, InitialAttachPoint.position, ForceMode.VelocityChange);
            }

            CurrentAngle = Quaternion.Angle(Quaternion.identity, this.transform.rotation);
        }

        public override void BeginInteraction(NVRHand hand)
        {
            base.BeginInteraction(hand);

            InitialAttachPoint = new GameObject(string.Format("[{0}] InitialAttachPoint", this.gameObject.name)).transform;
            //InitialAttachPoint = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            InitialAttachPoint.position = hand.transform.position;
            InitialAttachPoint.rotation = hand.transform.rotation;
            InitialAttachPoint.localScale = Vector3.one * 0.25f;
            InitialAttachPoint.parent = this.transform;

            ClosestHeldPoint = (InitialAttachPoint.position - this.transform.position);
        }

        public override void EndInteraction()
        {
            base.EndInteraction();

            if (InitialAttachPoint != null)
                Destroy(InitialAttachPoint.gameObject);
        }

        protected override void DropIfTooFar()
        {
            float distance = Vector3.Distance(AttachedHand.transform.position, (this.transform.position + ClosestHeldPoint));
            if (distance > DropDistance)
            {
                DroppedBecauseOfDistance();
            }
        }

    }
}