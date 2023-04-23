using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildStair : Singleton<BuildStair>
{
    [SerializeField] int perRowMaxHumanCount;
    [SerializeField] float distanceBetweenHumans;
    private List<int> stairCountList;
    private List<GameObject> stairList;

    private int stairID = 0;
    public int StairID
    {
        get => stairID;
        set => stairID = value;
    }

    public bool StairFinished()
    {
        stairID--;
        return StairID <= 0;
    }

    private void Start()
    {
        stairCountList = new List<int>();
        stairList = new List<GameObject>();
        EventManager.OnFinishArea += Build;
    }
    
    private void Build()
    {
        FillStairList();
        StartCoroutine(BuildStairCoroutine());
    }

    private void FillStairList()
    {
        int playerChildCount = StackController.playerChildCount;

        for (int i = 1; i <= perRowMaxHumanCount; i++)
        {
            if (playerChildCount < i)
                break;

            playerChildCount -= i;
            stairCountList.Add(i);
        }
        for (int i = perRowMaxHumanCount; i > 0; i--)
        {
            if (playerChildCount >= i)
            {
                playerChildCount -= i;
                stairCountList.Add(i);
                i++;
            }
        }
        stairCountList.Sort();
    }


    private IEnumerator BuildStairCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 sum;
        GameObject stairStep;

        int tempTowerStairCount;
        int childCount = transform.childCount;

        foreach (int inStairPlayerCount in stairCountList)
        {
            foreach (GameObject child in stairList)
            {
                child.transform.localPosition += Vector3.up * 3;
            }

            stairStep = new GameObject("StairStep" + stairID);

            AddComponentAndTransform(stairStep);

            stairList.Add(stairStep);

            sum = Vector3.zero;
            tempTowerStairCount = 0;
            

            for (int i = 0; i < childCount; i++)
            {
                Transform child = transform.GetChild(i);
                

                if (child.GetComponent<PlayerChild>())
                {
                    if (!child.gameObject.activeSelf)
                        continue;
                    
                    child.GetComponent<Collider>().enabled = false;
                    child.GetComponent<NavMeshAgent>().enabled = false;
                    child.GetComponent<NavMeshFollow>().enabled = false;

                    child.transform.parent = stairStep.transform;
                    child.transform.localPosition = Vector3.right * tempTowerStairCount * distanceBetweenHumans;
                    
                    sum += child.transform.position;
                    
                    tempTowerStairCount++;
                    i--;

                    if (tempTowerStairCount >= inStairPlayerCount)
                        break;
                }
            }

            stairStep.transform.position = new Vector3(-sum.x / inStairPlayerCount, stairStep.transform.position.y, stairStep.transform.position.z);
            sum = Vector3.zero;
            stairID++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void AddComponentAndTransform(GameObject go)
    {
        go.AddComponent<BoxCollider>().center = Vector3.up + Vector3.forward;
        go.GetComponent<BoxCollider>().isTrigger = true;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.AddComponent<StairCollider>();
    }

    private void OnDisable() => EventManager.OnFinishArea -= Build;
}