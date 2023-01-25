using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sphere : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private float stepChangeSpeed = 0.002f;
    [SerializeField] private float distance = 30;
    private float speed;
    private float maxSpeed = 1;
    public bool activePause = false;
    private bool endSphere = false;
    private bool removeStatus = false;
    private RandColor randColor;
    private float stepChange = 0.01f;
    private Vector3 position;
    private float startDistance;
    private float couneter = 0;
    private float minScaleSize = 0.1f;

    void Start()
    {
        randColor = gameObject.GetComponent<RandColor>();
        distanceText.text = "";
        startDistance = distance;
        position = transform.position;
        StartCoroutine(MainCorutine());
    }

    public void setPause(bool value)
    {
        activePause = value;
    }

    public float calculateDistance()
    {
        return  (startDistance - distance)/ startDistance;
    }

    public void startPauseInfo()
    {
        distanceText.text = "Dystans: " + calculateDistance()*100 + "%";
    }
    public void endPauseInfo()
    {
        distanceText.text = "";
    }

    void reducingSpiralPath()
    {
        if (!activePause)
        {
            distance -= stepChange;
            accelerationSephare();
            if (distance <= 0)
            {
                activePause = true;
            }
            couneter += stepChange * speed;
        }
        else
            speed = 0;
    }

    void checkingEndOfTheSpiralPath()
    {
        if (distance <= 0 && !endSphere)
            scaleChanegSphare();
    }

    void changeColorSphere()
    {
        randColor.changeColorSphere(calculateDistance());
    }

    void setRemoveStatus()
    {
        removeStatus = true;
    }

    IEnumerator MainCorutine()
    {
        while (true)
        {
            reducingSpiralPath();
            checkingEndOfTheSpiralPath();
            changeColorSphere();
            if (!explosion.isPlaying && endSphere)
            {
                setRemoveStatus();
            }
            
            yield return new WaitForSeconds(stepChange);
        }      
    }

    public bool GetRemoveStatus()
    {
        return removeStatus;
    }

    void scaleChanegSphare()
    {
        float scale = transform.localScale.x;
        if (scale > minScaleSize)
        {
            Vector3 scaleChange = new Vector3(-stepChange, -stepChange, -stepChange);
            transform.localScale += scaleChange;
        }
        else
        {
            explosion.Play();
            endSphere = true;
        }
    }

    void accelerationSephare()
    {
        if(speed <= maxSpeed)
            speed += stepChangeSpeed;
    }

    void moveCircle()
    {
        float x = Mathf.Cos(couneter) * distance;
        float y = 0;
        float z = Mathf.Sin(couneter) * distance;
        transform.position = new Vector3(position.x+x, position.y+y, position.z+z);
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if(!activePause)
        {
            moveCircle();
        } 
    }
}
