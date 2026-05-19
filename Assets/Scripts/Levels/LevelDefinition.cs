using UnityEngine;

[CreateAssetMenu(fileName = "Level Definition", menuName = "Tsuper Man/Level Definition")]
public class LevelDefinition : ScriptableObject
{
    [SerializeField] private string sceneName;
    [SerializeField] private int requiredPassengersDropped;
    [SerializeField] private int requiredEnemiesKilled;

    public string SceneName => sceneName;
    public int RequiredPassengersDropped => requiredPassengersDropped;
    public int RequiredEnemiesKilled => requiredEnemiesKilled;
}
