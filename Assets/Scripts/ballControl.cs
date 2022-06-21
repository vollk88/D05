using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballControl : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject bash;
    [SerializeField] private GameObject winScreen;
    public Image _imageUIForce;
    private bool _space;
    private int freedom;
    private bool goUp;
    private Vector3 startPos;
    private Vector3 oldPos;
    private Quaternion startRot;
    private Rigidbody _rigidbody;
    private FollowPlayer camScript;

    public float upForce = 0.5f;
    public float _forceHit = 40f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        camScript = _camera.GetComponent<FollowPlayer>();
        startPos = _camera.gameObject.transform.position;
        startRot = _camera.gameObject.transform.rotation;
        oldPos = transform.position;
        bash.transform.position = transform.position;
        freedom = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_camera.transform.parent);
        _camera.transform.DetachChildren();
        InputManager();
        if (_rigidbody.velocity.magnitude < 0.09f && freedom != 1)
        {
            // Debug.Log(_space);
            bash.transform.position = transform.position;
            if (_rigidbody.velocity.magnitude == 0 && _arrow.activeSelf && !_space)
            {
                // transform.rotation = Quaternion.identity;
                bash.transform.rotation = transform.rotation;
            }
        }
    }

    private void InputManager()
    {
        Debug.Log("freedom: " + freedom + " spacebool: " + _space);
        if (Input.GetKey(KeyCode.A) && !_space && freedom == 2)
        {
            transform.Rotate(0,-1,0);
            bash.transform.Rotate(0,-1,0);
        }
        if (Input.GetKey(KeyCode.D) && !_space && freedom == 2)
        {
            transform.Rotate(0,1,0);
            bash.transform.Rotate(0,1,0);
        }
        
        if (Input.GetKey(KeyCode.Q) && !_space)
        {
            camScript.freedom = true;
            // camScript.RememberPos(); 
            _camera.transform.position += transform.up * 0.01f;
            freedom = 0;
        }
        
        if (Input.GetKey(KeyCode.E) && !_space)
        {
            camScript.freedom = true;
            _camera.transform.position += -transform.up * 0.01f;
            freedom = 0;
        }

        if (_rigidbody.velocity.magnitude < 0.05 && _space && freedom == 2)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.rotation = Quaternion.identity;
            Quaternion auf = Quaternion.identity;
            auf.y = transform.rotation.y;
            transform.rotation = auf;
            
            if (_rigidbody.velocity == Vector3.zero)
                _arrow.SetActive(true);
            _space = false;
        }

        if (_space &&  freedom != 2)
        {
            if (!goUp)
            {
                if (_imageUIForce.fillAmount == 1)
                    goUp = true;
                _imageUIForce.fillAmount += 0.005f;
            }
            else
            {
                if (_imageUIForce.fillAmount == 0)
                    goUp = false;
                _imageUIForce.fillAmount -= 0.005f;
            }
            if(Input.GetKeyDown(KeyCode.Space) && freedom != 2)
            {
                freedom = 2;
                Vector3 aaa = transform.forward;
                aaa.y = upForce;
                _rigidbody.velocity = (aaa * _forceHit * _imageUIForce.fillAmount);
                _imageUIForce.fillAmount = 0;
                _arrow.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape) && freedom != 2)
                _space = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !_space)
        {
            if (freedom == 0)
            {
                freedom = 2;
                return;
            }
            _space = true;
            freedom = 1;
            camScript.freedom = false;
            _arrow.SetActive(true);
            // camScript.ResetPos();
            // _camera.gameObject.transform.position = startPos;
            // _camera.gameObject.transform.rotation = startRot;
        }
        ChooseClub();
    }

    private void ChooseClub()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            upForce = 0.4f;
            _forceHit = 30f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            upForce = 0.9f;
            _forceHit = 17f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            upForce = 0.06f;
            _forceHit = 60f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            this.enabled = false;
            winScreen.SetActive(true);
        }
    }
}
