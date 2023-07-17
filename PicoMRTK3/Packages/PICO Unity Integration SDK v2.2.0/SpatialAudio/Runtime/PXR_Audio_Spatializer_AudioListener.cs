//  Copyright © 2015-2022 Pico Technology Co., Ltd. All Rights Reserved.

using System.Collections;
using PXR_Audio.Spatializer;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class PXR_Audio_Spatializer_AudioListener : MonoBehaviour
{
    private bool isActive;

    private PXR_Audio_Spatializer_Context context;
    private PXR_Audio_Spatializer_Context Context
    {
        get
        {
            if (context == null)
                context = PXR_Audio_Spatializer_Context.Instance;
            return context;
        }
    }

    private float[] positionArray = new float[3] {0.0f, 0.0f, 0.0f};
    private float[] frontArray = new float[3] {0.0f, 0.0f, 0.0f};
    private float[] upArray = new float[3] {0.0f, 0.0f, 0.0f};

    private bool isAudioDSPInProgress = false;

    public bool IsAudioDSPInProgress
    {
        get
        {
            return isAudioDSPInProgress;
        }
    }

    internal void RegisterInternal()
    {
        //  Initialize listener pose
        UpdatePose();
        isActive = true;
    }

    private void OnEnable()
    {
        //  Wait for context to be initialized
        if (Context != null && Context.Initialized)
            RegisterInternal();
    }

    void Update()
    {
        if (isActive && context != null && context.Initialized && transform.hasChanged)
        {
            UpdatePose();
        }
    }

    private void OnDisable()
    {
        isActive = false;
        isAudioDSPInProgress = false;
    }

    void UpdatePose()
    {
        positionArray[0] = transform.position.x;
        positionArray[1] = transform.position.y;
        positionArray[2] = -transform.position.z;
        frontArray[0] = transform.forward.x;
        frontArray[1] = transform.forward.y;
        frontArray[2] = -transform.forward.z;
        upArray[0] = transform.up.x;
        upArray[1] = transform.up.y;
        upArray[2] = -transform.up.z;
        Context.SetListenerPose(positionArray, frontArray, upArray);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!isActive || context == null || !context.Initialized || Context.spatializerApiImpl==SpatializerApiImpl.wwise)
            return;

        isAudioDSPInProgress = true;
        context.GetInterleavedBinauralBuffer(data, (uint) (data.Length / channels), true);
        isAudioDSPInProgress = false;
    }
}
