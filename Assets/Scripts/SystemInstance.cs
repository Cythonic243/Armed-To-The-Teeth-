using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInstance : MonoBehaviour
{
    static public SystemInstance systemInstance;
    public LevelDataScriptObject levelData;
    // Start is called before the first frame update
    void Start()
    {
        if (systemInstance != null)
        {
            Debug.LogError("systemInstance != null");
            GameObject.Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        systemInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
