using UnityEngine;

public class oscillating : MonoBehaviour
{
     Vector3 startPosition;
     Vector3 endPosition;
    [SerializeField] Vector3 momentVector;
    [SerializeField] float speed;
     float momentFactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        endPosition=startPosition + momentVector;
    }

    // Update is called once per frame
    void Update()
    {
        momentFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position= Vector3.Lerp(startPosition, endPosition, momentFactor);
    }
}
