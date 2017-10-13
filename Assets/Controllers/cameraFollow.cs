using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    public float cameraOffsetY = 20f;
    public float cameraOffsetZ = 3f;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

	void FixedUpdate () {
        transform.position = new Vector3(player.transform.position.x, cameraOffsetY, player.transform.position.z - cameraOffsetZ);
	}
}
