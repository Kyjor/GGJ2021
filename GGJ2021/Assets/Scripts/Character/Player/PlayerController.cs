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
			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}

			if (Input.GetKey(KeyCode.LeftShift))
			{
				sprint = true;
			}			
			axesDelegate?.Invoke(movementAxes, sprint);

			base.Update();
		}
	}

