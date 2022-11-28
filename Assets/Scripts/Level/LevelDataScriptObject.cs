using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataScriptObject", menuName = "ScriptableObjects/LevelDataScriptObject", order = 1)]
public class LevelDataScriptObject : ScriptableObject
{
    public int numTeethInfected = 1;
    public int numTeethVulnerable = 1;
    public int infectedTeethSpawnSec = 5;
    public int tongueSpawnSec = 10;
}