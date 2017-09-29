using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{

    GameObject ground;

    private void Awake()
    {
        ground = transform.gameObject;


    }

    List<GameObject> groundList = new List<GameObject>();

}

