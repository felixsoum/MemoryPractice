using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Vector3? savedPosition = null;

    private void Start()
    {
        if (savedPosition.HasValue)
        {
            transform.position = savedPosition.Value;
        }
    }

    public void Save()
    {
        savedPosition = transform.position;
    }
}
