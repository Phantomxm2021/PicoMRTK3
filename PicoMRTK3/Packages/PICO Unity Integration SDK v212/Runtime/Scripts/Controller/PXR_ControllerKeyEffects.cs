#if PICO_INSTALL
/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public class PXR_ControllerKeyEffects : MonoBehaviour
    {
        public Texture2D textureIdle;
        public Texture2D textureApp;
        public Texture2D textureHome;
        public Texture2D textureTouchpad;
        public Texture2D textureVolUp;
        public Texture2D textureVolDown;
        public Texture2D textureTrigger;
        public Texture2D textureA;
        public Texture2D textureB;
        public Texture2D textureX;
        public Texture2D textureY;
        public Texture2D textureGrip;

        public PXR_Input.Controller hand;
        private Renderer controllerRenderMat;
        private XRNode node;

        private bool lPrimary2DButton, rPrimary2DButton, lMenuButton, rMenuButton, lGripButton, rGripButton, lTriggerButton, rTriggerButton,x,y,a,b;

        void Start()
        {
            controllerRenderMat = GetComponent<Renderer>();
            switch (hand)
            {
                case PXR_Input.Controller.LeftController:
                    node = XRNode.LeftHand;
                    break;
                case PXR_Input.Controller.RightController:
                    node = XRNode.RightHand;
                    break;
            }
        }

        void Update()
        {
            GetButtonState(node);
            ChangeKeyEffects(node);
        }

        private void GetButtonState(XRNode node)
        {
            switch (node)
            {
                case XRNode.LeftHand:
                    {
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out lPrimary2DButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.menuButton, out lMenuButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.gripButton, out lGripButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.triggerButton, out lTriggerButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.primaryButton, out x);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.secondaryButton, out y);
                    }
                    break;
                case XRNode.RightHand:
                    {
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rPrimary2DButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.menuButton, out rMenuButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.gripButton, out rGripButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.triggerButton, out rTriggerButton);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.primaryButton, out a);
                        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.secondaryButton, out b);
                    }
                    break;
            }
        }

        private void ChangeKeyEffects(XRNode node)
        {
            switch (node)
            {
                case XRNode.LeftHand:
                    {
                        if (lPrimary2DButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureTouchpad);
                        }
                        else if (lMenuButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureApp);
                        }
                        else if (lTriggerButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureTrigger);
                        }
                        else if (x)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureX);
                        }
                        else if (y)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureY);
                        }
                        else if (lGripButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureGrip);
                        }
                        else
                        {
                            if (controllerRenderMat.material.GetTexture("_MainTex") != textureIdle)
                            {
                                controllerRenderMat.material.SetTexture("_MainTex", textureIdle);
                            }
                        }
                    }
                    break;
                case XRNode.RightHand:
                    {
                        if (rPrimary2DButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureTouchpad);
                        }
                        else if (rMenuButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureApp);
                        }
                        else if (rTriggerButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureTrigger);
                        }
                        else if (a)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureA);
                        }
                        else if (b)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureB);
                        }
                        else if (rGripButton)
                        {
                            controllerRenderMat.material.SetTexture("_MainTex", textureGrip);
                        }
                        else
                        {
                            if (controllerRenderMat.material.GetTexture("_MainTex") != textureIdle)
                            {
                                controllerRenderMat.material.SetTexture("_MainTex", textureIdle);
                            }
                        }
                    }
                    break;
            }
        }
    }
}

#endif