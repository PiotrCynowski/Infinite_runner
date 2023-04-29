using UnityEngine;
using UnityEditor;
using SpawnedCollObjects;

#if UNITY_EDITOR
[CustomEditor(typeof(SpawnCollObjManager))]
public class SpawnCollObjManagerTesting : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnCollObjManager SpawnerManager = (SpawnCollObjManager)target;

        //if (GUILayout.Button("TEST prepare spawners"))
        //{
            //SpawnerManager.PrepareSpawnPoints();
        //}
    }
}
#endif