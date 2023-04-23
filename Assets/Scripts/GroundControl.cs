using UnityEngine;

public class GroundControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChild>())
            Player.Instance.limitX = Mathf.Abs(other.transform.parent.transform.position.x);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChild>())
            Player.Instance.limitX = 10;
    }
}
