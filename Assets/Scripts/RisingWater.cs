using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour {

    [SerializeField] float waterSpeed = 1f;
	
	// Update is called once per frame
	void Update () {
        var movement = new Vector2(0, waterSpeed * Time.deltaTime);
        transform.Translate(movement);
	}
}
