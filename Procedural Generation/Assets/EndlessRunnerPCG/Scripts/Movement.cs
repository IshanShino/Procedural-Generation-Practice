using UnityEngine;

public class Movement : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += new Vector3(5, 0, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject, 4);
        }
    }
}
