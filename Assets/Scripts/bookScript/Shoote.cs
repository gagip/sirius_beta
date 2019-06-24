using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{


    public GameObject samplePreFab;

    public float shootSpeed;
    public float shootTorque;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Button"))
            shot();
    }

    public void shot(){
        GameObject sample = (GameObject)Instantiate(
                samplePreFab,
                transform.position,
                Quaternion.identity
        );

        Rigidbody sampleRigidBody = sample.GetComponent<Rigidbody>();
        sampleRigidBody.AddForce(transform.forward * shootSpeed);
        sampleRigidBody.AddTorque(new Vector3(0, shootTorque, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sample")
        {
            Destroy(other.gameObject);
        }
    }
}
