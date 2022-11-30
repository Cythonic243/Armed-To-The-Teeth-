using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneChanger : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SystemInstance.systemInstance.levelIndex = sceneID;
        Debug.Log("MoveToScene " + sceneID);
        SceneManager.LoadScene(4+sceneID);
    }
}
