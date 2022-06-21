using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _players;
    private Vector3 offset;
    [SerializeField] private float mouseSens = 10f;
    private float xRotation;
    private float yRotation;
    private float speed;
    private float speed2;
    private float curSpeed;
    private Vector3 startPos;
    private Quaternion startRot;
    
    public bool freedom = false;
    


    private void Start()
    {
        offset = transform.position;
        speed = 15f;
        speed2 = speed * 2;
        curSpeed = speed;
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if (!freedom)
        {
            // transform.position = startPos;
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * curSpeed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * curSpeed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * curSpeed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * curSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            curSpeed = speed2;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            curSpeed = speed;
        
    }

    public void ResetPos()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }

    public void RememberPos()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }
    
}
