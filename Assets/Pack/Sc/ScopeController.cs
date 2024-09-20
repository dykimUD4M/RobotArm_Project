using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeController : MonoBehaviour
{
    public Camera playerCamera; // 플레이어의 카메라
    public float zoomedFOV = 30f; // 줌인 시의 Field of View
    public float normalFOV = 60f; // 기본 Field of View
    public float zoomSpeed = 10f; // 줌의 속도

    private void Update()
    {
        // 오른쪽 마우스 버튼을 누르고 있을 때 줌인
        if (Input.GetMouseButton(1)) // 오른쪽 마우스 버튼
        {
            // 줌인 상태로 부드럽게 전환
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomedFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // 기본 FOV로 부드럽게 전환
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, Time.deltaTime * zoomSpeed);
        }
    }
}


