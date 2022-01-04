﻿using UnityEngine;
using UnityEngine.Events;
#if !VRCONCEPT_OCULUS
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
#endif
using System.Collections;

namespace Epibyte.ConceptVR
{
    public class ToggleController : MonoBehaviour
    {
#if VRCONCEPT_OCULUS
        public OVRInput.Controller controller;
        public OVRInput.Button toggleButton = OVRInput.Button.Two;
#else
        public XRController controller;
        public InputHelpers.Button toggleButton;
        bool isTogglePreviousPressed = false;
#endif

        public GameObject targetObject;
        public UnityEvent closeEvents;
        public UnityEvent openEvents;

        void LateUpdate()
        {

#if VRCONCEPT_OCULUS
            if (OVRInput.GetDown(toggleButton, controller))
#else
            if (getToggleButtonState() == ButtonState.Pressed)
#endif
            {
                if (targetObject.activeSelf)
                {

                    closeEvents.Invoke();
                    targetObject.SetActive(false);
                }
                else
                {
                    openEvents.Invoke();
                    targetObject.SetActive(true);
                }
            }
        }


#if !VRCONCEPT_OCULUS
        ButtonState getToggleButtonState()
        {
            if (controller.inputDevice.IsPressed(toggleButton, out bool pressed))
            {
                if (isTogglePreviousPressed != pressed)
                {
                    isTogglePreviousPressed = pressed;

                    if (pressed)
                    {
                        return ButtonState.Pressed;
                    }
                    else
                    {
                        return ButtonState.Released;
                    }
                }
            }
            return ButtonState.Others;
        }
#endif
    }
}
