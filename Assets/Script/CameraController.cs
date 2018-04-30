
using UnityEngine;

public class CameraController : MonoBehaviour {
    private bool doMovment = true;

    public float panSpeed = 30f;
    public float scrollSpeed = 5f;
    public float minZ = 10f;
    public float maxZ = 80f;


    void Update () {
/*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovment = !doMovment;
        }

        if (!doMovment)
        {
            return;
        }
        */

        if (Input.GetKey("w")) 
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d")) 
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;


        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.z += scroll * 1000 * scrollSpeed * Time.deltaTime;
        

        transform.position = pos;



    }
}
