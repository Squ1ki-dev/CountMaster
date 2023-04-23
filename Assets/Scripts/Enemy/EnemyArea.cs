using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyArea : MonoBehaviour
{
    private Collider boxCollider;

    private int enemyCount;
    [SerializeField] private TextMeshProUGUI enemyCountText;

    [SerializeField] private Transform canvas;
    [SerializeField] private Animator circleAnimator;

    private List<Enemy> children = new List<Enemy>();

    private void Awake()
    {
        boxCollider = GetComponent<Collider>();
        enemyCount = transform.childCount - 1;
        foreach (Enemy child in GetComponentsInChildren<Enemy>())
        {
            children.Add(child);
        }
    }

    private void Start() => enemyCountText.text = enemyCount.ToString();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChild>())
        {
            Player.Instance.speed = 3;
            Player.Instance.xSpeed = 2f;
            boxCollider.enabled = false;
            ChildrenMove();
        }
    }

    public void AllEnemiesDied()
    {
        enemyCount--;
        enemyCountText.text = enemyCount.ToString();

        if (enemyCount <= 0)
        {
            EventManager.Fire_OnStartMovement();
            circleAnimator.SetBool(AnimConst.enemyFinish, true);
            Destroy(gameObject, 0.5f);
            Player.Instance.speed = 15;
            Player.Instance.xSpeed = 15f;
        }
    }

    private void ChildrenMove()
    {
        foreach (Enemy child in children)
        {
            child.StartMove();
        }
    }
    
    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }
}