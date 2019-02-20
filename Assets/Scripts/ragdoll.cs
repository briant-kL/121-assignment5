using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll : MonoBehaviour
{

    public GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
