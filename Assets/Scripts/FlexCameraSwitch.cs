using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class FlexCameraSwitch : MonoBehaviour
{

    public GameObject[] cameraList;
    private int currentCamera;
    private float timeMinimum = 0.5f;
    private GameObject mainCamera;

    [SerializeField] private float speed = 1.0f;
    void Start()
    {
        currentCamera = 0;
        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Priority = 1 - i;
        }

        if (cameraList.Length > 0)
        {
            cameraList[0].transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }

        mainCamera = transform.Find("Main Camera").gameObject;
    }

    void Update()
    {
        
    }

    public void CamSwap()
    {
        currentCamera++;
        if (currentCamera < cameraList.Length)
        {
            float distance = Vector3.Distance(cameraList[currentCamera].transform.Find("Virtual Camera").position, cameraList[currentCamera - 1].transform.Find("Virtual Camera").position);
            float time = distance / speed;

            Debug.Log(time);

            time = Mathf.Max(timeMinimum, time);

            mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = time;

            cameraList[currentCamera - 1].transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Priority = 0;
            cameraList[currentCamera].transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
        else
        {
            float distance = Vector3.Distance(cameraList[0].transform.Find("Virtual Camera").position, cameraList[currentCamera - 1].transform.Find("Virtual Camera").position);
            float time = distance / speed;

            Debug.Log(time);

            time = Mathf.Max(timeMinimum, time);

            mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = time;

            cameraList[currentCamera - 1].transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Priority = 0;
            currentCamera = 0;
            cameraList[currentCamera].transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
    }
}
