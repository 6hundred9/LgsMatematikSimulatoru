using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class GateContoller : MonoBehaviour
{
    private BoxCollider _bc;
    
    [BurstCompile]
    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider>();
    }

    [BurstCompile]
    // Update is called once per frame
    void Update()
    {
        _bc.enabled = CompareTag("Wrong");
    }
}
