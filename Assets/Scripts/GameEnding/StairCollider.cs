using UnityEngine;

public class StairCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Stair)) return;
        
        StopChildMovement(other);

        if (BuildStair.Instance.StairFinished())
        {
            EventManager.Fire_OnStopMovement();
            GameStateEvent.Fire_OnChangeGameState(GameState.Win);
        }
    }

    private void StopChildMovement(Collider obj)
    {
        transform.parent = obj.transform;

        foreach (PlayerChild item in GetComponentsInChildren<PlayerChild>())
        {
            item.StopMoveChild();
        }
    }

}