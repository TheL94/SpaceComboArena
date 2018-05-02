﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Movement : MonoBehaviour
    {
        public GameObject ModelToRotate;
        Dash dash;
        public float MovementSpeed { get; set; }
        float RotationSpeed;

        Rigidbody playerRigidbody;

        public void Init(float _movementSpeed, float _rotationSpeed, DashStruct _dashData)
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerRigidbody.useGravity = true;
            dash = GetComponent<Dash>();
            dash.Init(this, _dashData);
            MovementSpeed = _movementSpeed;
            RotationSpeed = _rotationSpeed;
        }

        public void ReInit()
        {
            playerRigidbody.useGravity = false;
        }

        public void Move(Vector3 _position)
        {
            Vector3 position = _position * MovementSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + position);
        }

        public void Turn()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(mouseRay, out floorHit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("MouseRaycast")))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse, transform.up);
                playerRigidbody.MoveRotation(newRotatation);
            }
        }

        public void Dash(Vector3 _direction)
        {
            dash.ActivateDash(_direction);
        }
    }
}