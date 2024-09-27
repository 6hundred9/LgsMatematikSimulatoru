using System;
using System.Collections;
using System.Collections.Generic;
using _scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Dictionary<Pos, float> EnumToPos = new()
    {
        { Pos.Left, -2.5f},
        { Pos.Middle, 0},
        { Pos.Right, 2.5f}
    };

    public Pos currentState = Pos.Middle;

    private Rigidbody _rb;
    private ConstantForce _cf;

    private GameObject _finish;
    private GameObject _start;

    private GameObject _gm1;
    private GameObject _gm2;
    private TextMeshPro _pro1;
    private TextMeshPro _pro2;
    private Text _pro3;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _finish)
        {
            if (_rb.maxLinearVelocity <= 10)
            {
                _rb.maxLinearVelocity++; 
                _cf.force = new(-_rb.maxLinearVelocity, 0, 0);
            }
        }

        if (other.gameObject.CompareTag("Wrong"))
        {
            _rb.maxLinearVelocity = 3;
            _cf.force = new(-_rb.maxLinearVelocity, 0, 0);
        }

        if (other.gameObject == _finish || other.gameObject.CompareTag("Wrong"))
        {
            transform.position = _start.transform.position;
            (int, int, string, int) a = MathGen.GenerateNew(); // result 1, result 2, operation text, wrong answer
            _pro1.text = a.Item1.ToString();
            _pro2.text = a.Item2.ToString();
            if (a.Item1 == a.Item4) GameObject.Find("Secenek1").tag = "Wrong";
            else if (a.Item2 == a.Item4) GameObject.Find("Secenek2").tag = "Wrong";
            if (GameObject.Find("Secenek1").CompareTag("Wrong"))
            {
                GameObject.Find("Secenek1").TryGetComponent(typeof(BoxCollider), out Component component1);
                ((BoxCollider)component1).enabled = true;
                GameObject.Find("Secenek2").TryGetComponent(typeof(BoxCollider), out Component component);
                ((BoxCollider)component).enabled = false;
            }
            else
            {
                GameObject.Find("Secenek1").TryGetComponent(typeof(BoxCollider), out Component component1);
                ((BoxCollider)component1).enabled = false;
                GameObject.Find("Secenek2").TryGetComponent(typeof(BoxCollider), out Component component);
                ((BoxCollider)component).enabled = true;
            }
            _pro3.text = a.Item3;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cf = GetComponent<ConstantForce>();
        _finish = GameObject.Find("Finish");
        _start = GameObject.Find("Start");


        _gm1 = GameObject.FindGameObjectWithTag("1");
        _gm2 = GameObject.FindGameObjectWithTag("2");
        _pro1 = _gm1.GetComponent<TextMeshPro>();
        _pro2 = _gm2.GetComponent<TextMeshPro>();
        _pro3 = GameObject.FindGameObjectWithTag("3").GetComponent<Text>();
        
        _rb.maxLinearVelocity = 3;
        
        (int, int, string, int) a = MathGen.GenerateNew(); // result 1, result 2, operation text, wrong answer
        _pro1.text = a.Item1.ToString();
        _pro2.text = a.Item2.ToString();
        if (a.Item1 == a.Item4) GameObject.Find("Secenek1").tag = "Wrong";
        else if (a.Item2 == a.Item4) GameObject.Find("Secenek2").tag = "Wrong";
        if (GameObject.Find("Secenek1").CompareTag("Wrong"))
        {
            GameObject.Find("Secenek1").TryGetComponent(typeof(BoxCollider), out Component component1);
            ((BoxCollider)component1).enabled = false;
            GameObject.Find("Secenek2").TryGetComponent(typeof(BoxCollider), out Component component);
            ((BoxCollider)component).enabled = true;
        }
        else
        {
            GameObject.Find("Secenek1").TryGetComponent(typeof(BoxCollider), out Component component1);
            ((BoxCollider)component1).enabled = true;
            GameObject.Find("Secenek2").TryGetComponent(typeof(BoxCollider), out Component component);
            ((BoxCollider)component).enabled = false;
        }
        _pro3.text = a.Item3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentState == Pos.Left) 
            {
                return;
            }
            if (currentState == Pos.Middle) 
            {
                currentState = Pos.Left;
            }
            else if (currentState == Pos.Right) 
            {
                currentState = Pos.Middle;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentState == Pos.Right) 
            {
                return;
            }
            if (currentState == Pos.Middle) 
            {
                currentState = Pos.Right;
            }
            else if (currentState == Pos.Left) 
            {
                currentState = Pos.Middle;
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, EnumToPos[currentState]);
    }
}