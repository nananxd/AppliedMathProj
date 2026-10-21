using UnityEngine;

public class VectorMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public Transform target;
    public Vector3 offset;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //multiplication 
            transform.position +=  transform.forward * speed;
            //subraction
           // offset = target.position - transform.position;

            ////addition
            //Vector3 right = new Vector3(3f,0,0);
            //Vector3 forward = new Vector3(0,0,5f);

            //transform.position = right + forward;
        }
    }
}
