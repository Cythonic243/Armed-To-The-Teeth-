using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInstance : MonoBehaviour
{
    static private SystemInstance _systemInstance;
    static public SystemInstance systemInstance {
        get
        {
            return _systemInstance;
        }
    }
    public LevelDataScriptObject levelData;
    // Start is called before the first frame update
    void Start()
    {
        if (_systemInstance != null)
        {
            Debug.LogError("systemInstance != null");
            GameObject.Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        _systemInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
