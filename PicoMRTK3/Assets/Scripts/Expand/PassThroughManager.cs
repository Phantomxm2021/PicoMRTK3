using Unity.XR.PXR;
using UnityEngine;

public class PassThroughManager : MonoBehaviour
{
    public void SetPassThrough(bool _state)
    {
        PXR_Boundary.EnableSeeThroughManual(_state);
    }


    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            SetPassThrough(true);
        }
    }
}