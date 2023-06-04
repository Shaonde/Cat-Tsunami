using UnityEngine;

public class EndPlateform : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlateformeStart.Instance.DeletePlat(other.gameObject);
    }
}
