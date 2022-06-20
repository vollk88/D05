using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballControl : MonoBehaviour
{
    [SerializeField]private Camera _camera;
    [SerializeField] private GameObject _arrow;
    public Image _imageUIForce;
    private bool _space;
    private int freedom;
    private bool goUp;
    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody _rigidbody;

    public float upForce = 0.5f;
    public float _forceHit = 40f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        startPos = _camera.gameObject.transform.localPosition;
        startRot = _camera.gameObject.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !_space)
        {
            transform.Rotate(0,-1,0);

        }
        if (Input.GetKey(KeyCode.D) && !_space)
        {
            transform.Rotate(0,1,0);
        }

        if (Input.GetKey(KeyCode.W) && !_space)
        {
            _camera.transform.position += transform.forward * 0.01f;
        }
        if (Input.GetKey(KeyCode.S) && !_space)
        {
            _camera.transform.position += -transform.forward * 0.01f;
        }
        if (Input.GetKey(KeyCode.Q) && !_space)
        {
            _camera.transform.position += transform.up * 0.01f;
        }
        
        if (Input.GetKey(KeyCode.E) && !_space)
        {
            _camera.transform.position += -transform.up * 0.01f;
        }

        if (_rigidbody.velocity == Vector3.zero && _space && freedom == 2)
        {
            Quaternion auf = Quaternion.identity;
            auf.y = transform.rotation.y;
            
            transform.rotation = auf;
            
            _arrow.SetActive(true);
            _space = false;
        }

        if (_space &&  freedom != 2)
        {
            _camera.gameObject.transform.localPosition = startPos;
            _camera.gameObject.transform.localRotation = startRot;
            if (!goUp)
            {
                if (_imageUIForce.fillAmount == 1)
                    goUp = true;
                _imageUIForce.fillAmount += 0.005f;
            }
            else if (goUp)
            {
                if (_imageUIForce.fillAmount == 0)
                    goUp = false;
                _imageUIForce.fillAmount -= 0.005f;
            }
            // Debug.Log("asd");
            if(Input.GetKeyDown(KeyCode.Space) && freedom != 2)
            {
                freedom = 2;
                // Debug.Log("auf");
                Vector3 aaa = transform.forward;
                aaa.y = upForce;
                _rigidbody.velocity = (aaa * _forceHit * _imageUIForce.fillAmount);
                _imageUIForce.fillAmount = 0;
                _arrow.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !_space)
        {
            _space = true;
            freedom = 1;
            
            _arrow.SetActive(true);
        }
    }
}
