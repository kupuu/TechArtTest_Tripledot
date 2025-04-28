using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private bool isSpinning = false;

    private void OnEnable()
    {
        isSpinning = true;
    }

    private void OnDisable()
    {
        isSpinning = false;
    }

    private void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }        
    }
}