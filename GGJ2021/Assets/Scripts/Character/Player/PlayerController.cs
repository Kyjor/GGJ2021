using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using JetBrains.Annotations;
using UnityEngine.EventSystems;




public class PlayerController : CharacterController
	{
		
	
		public override void Start()
		{
			base.Start();
		}

		public override void Update()
		{
			//Movement input
			movementAxes = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			axesDelegate?.Invoke(movementAxes);
			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}
			rotationX = 0;
			rotationY = 0;
			if(Input.GetButton("Fire2"))
			{
				rotationX = Input.GetAxis("Mouse X") * sensitivityX;
				rotationY = Input.GetAxis("Mouse Y") * sensitivityY;
			}
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
			base.Update();
		}
	}

