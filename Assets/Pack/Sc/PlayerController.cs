using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintMultiplier = 2f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public Transform playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 80f;

    // 새로운 변수들
    public float walkBobbingAmount = 0.1f; // 걸을 때 상하 흔들림의 강도
    public float bobbingSpeed = 4f;    // 상하 흔들림의 속도
    public float bobbingDamping = 0.5f; // 흔들림의 부드럽게 멈추는 정도

    private float yRotation = 0f;
    private Vector3 velocity;
    private float defaultY;
    private float timer = 0f;

    private void Start()
    {
        defaultY = transform.localPosition.y; // 기본 y 위치 저장

        // 마우스 고정 및 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Move
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Check if the player is sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Look around
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -lookXLimit, lookXLimit);

        playerCamera.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Bobbing effect
        float currentBobbingAmount = isSprinting ? bobbingSpeed * ( sprintMultiplier / 2 ) : bobbingSpeed;

        if (controller.isGrounded && (x != 0 || z != 0))
        {
            timer += Time.deltaTime * currentBobbingAmount;
            float sin = Mathf.Sin(timer);
            float bobbingOffset = sin * walkBobbingAmount;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultY + bobbingOffset, transform.localPosition.z);
        }
        else
        {
            timer -= Time.deltaTime * bobbingDamping;
            timer = Mathf.Max(0, timer); // 타이머가 음수가 되지 않도록

            float sin = Mathf.Sin(timer);
            float bobbingOffset = sin * walkBobbingAmount;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultY + bobbingOffset, transform.localPosition.z);
        }
    }
}
