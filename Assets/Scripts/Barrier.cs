using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject _sign;
    [SerializeField] GameObject[] _based;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
    public void DeleteBall() {
        Destroy(this);
    }

    public void OnDestroy()
    {
        //Debug.Log("destroy");
        Destroy(_sign);
        Destroy(GetComponent<CapsuleCollider>());
        foreach (var item in _based)
        {
            Destroy(item);
        }
    }
}
