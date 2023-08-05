using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockCameraY : CinemachineExtension
{
    [Tooltip("Lock the camera's Y position to this value")]
    public float m_MinYPosition;
    public float m_MaxYPosition;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.y = Mathf.Clamp(pos.y,m_MinYPosition,m_MaxYPosition);
            state.RawPosition = pos;
        }
    }
}
