using System;
using System.Collections;
using System.Collections.Generic;
using _scripts;
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
    private Text _pro4;

    private GameObject _game;
    private GameObject _gameOver;

    private int _points;

    private bool _gamePaused;

    float _lastVelocity;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _finish)
        {
            if (_rb.maxLinearVelocity <= 25)
            {
                _rb.maxLinearVelocity++; 
                _cf.force = new(-_rb.maxLinearVelocity, 0, 0);
            }
            _points++;
        }

        if (other.gameObject.CompareTag("Wrong"))
        {
            _rb.maxLinearVelocity = 10;
            _cf.force = new(-_rb.maxLinearVelocity, 0, 0);
            _points = 0;
            Pause();
        }

        if (other.gameObject == _finish || other.gameObject.CompareTag("Wrong"))
        {
            transform.position = _start.transform.position;
            (int, int, string, int) a = MathGen.GenerateNew(); // result 1, result 2, operation text, wrong answer
            _pro1.text = a.Item1.ToString();
            _pro2.text = a.Item2.ToString();
            GameObject.Find("Secenek1").tag = a.Item1 == a.Item4 ? "Wrong" : "Untagged";
            GameObject.Find("Secenek2").tag = a.Item2 == a.Item4 ? "Wrong" : "Untagged";
            _pro3.text = a.Item3;
            currentState = Pos.Middle;
            _pro4.text = $"{_points} puan";
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
        _pro3 = GameObject.Find("question").GetComponent<Text>();
        _pro4 = GameObject.Find("points").GetComponent<Text>();

        _game = GameObject.Find("Game");
        _gameOver = GameObject.Find("GameOver");

        _gameOver.SetActive(false);
        
        _rb.maxLinearVelocity = 10;
        
        (int, int, string, int) a = MathGen.GenerateNew(); // result 1, result 2, operation text, wrong answer
        _pro1.text = a.Item1.ToString();
        _pro2.text = a.Item2.ToString();
        GameObject.Find("Secenek1").tag = a.Item1 == a.Item4 ? "Wrong" : "Untagged";
        GameObject.Find("Secenek2").tag = a.Item2 == a.Item4 ? "Wrong" : "Untagged";
        _pro3.text = a.Item3;
        
        _pro4.text = $"{_points} puan";
    }

    public void ForceMove(string code)
    {
        if (code.ToLower() == "a")
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
        
        if (code.ToLower() == "d")
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
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (_gamePaused) return;
        
        _lastVelocity = _rb.maxLinearVelocity;
        _rb.maxLinearVelocity = 0;
        _game.SetActive(false);
        _gameOver.SetActive(true);
        
        _gamePaused = true;
    }

    public void Unpause()
    {
        if (!_gamePaused) return;
        
        _rb.maxLinearVelocity = _lastVelocity;
        _game.SetActive(true);
        _gameOver.SetActive(false);
        
        _gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) ForceMove("a");
        if (Input.GetKeyDown(KeyCode.D)) ForceMove("d");
        
        transform.position = new Vector3(transform.position.x, transform.position.y, EnumToPos[currentState]);
    }
}