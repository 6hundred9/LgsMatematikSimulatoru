using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateContoller : MonoBehaviour
{
    private BoxCollider _bc;
    
    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _bc.enabled = CompareTag("Wrong");
    }
}
