using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenDisable : MonoBehaviour {
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
