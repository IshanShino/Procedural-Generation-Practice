using UnityEngine;

public class RoadGenerationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(roadPrefab, new Vector3(-5.8f, 0, 0), Quaternion.identity);
        }
    }
}
