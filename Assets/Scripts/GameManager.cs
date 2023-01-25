using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    [SerializeField] private int minStartXPosition;
    [SerializeField] private int maxStartXPosition;
    [SerializeField] private int minStartZPosition;
    [SerializeField] private int maxStartZPosition;
    private float pouseTime = 5.0f;
    private Vector3 starPosition;
    private List<Sphere> spheres = new List<Sphere>();

    void Start()
    {
        starPosition = new Vector3(0, 0, 0);
        StartCoroutine(MainCorutine());
    }

    IEnumerator MainCorutine()
    {
        while(true)
        {
            RemovingDestroyedSphares();
            yield return new WaitForSeconds(0.1f);
        }    
    }

    private Vector3 randPosition()
    {
        float x = Random.Range(minStartXPosition, maxStartXPosition);
        float y = 0;
        float z = Random.Range(minStartZPosition, maxStartZPosition);
        Vector3 position = new Vector3(x, y, z);
        return position;
    }

    public void createNewSphare()
    {
        GameObject newSphare = Instantiate(sphere, randPosition(), Quaternion.identity);
        spheres.Add(newSphare.GetComponent<Sphere>());
    }

    void stopSphares()
    {
        foreach(Sphere i in spheres)
        {
            i.setPause(true);
            i.startPauseInfo();
        }
        goSphares();
    }

    void goSphares()
    {
        StartCoroutine(PauseCorutine());
    }

    IEnumerator PauseCorutine()
    {
        yield return new WaitForSeconds(pouseTime);
        foreach (Sphere i in spheres)
        {
            i.setPause(false);
            i.endPauseInfo();
        }
    }

    private void RemovingDestroyedSphares()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            if (spheres[i].GetRemoveStatus())
            {
                Sphere buf = spheres[i];
                spheres.RemoveAt(i);
                buf.kill();
            }
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            stopSphares();
        }
    }

}
