using UnityEngine;
using UnityEditor;
using Player;

public class EditorTestingTools : EditorWindow
{
    private bool isGamePlay = false;
    private Vector2 contentScrollPos;
    private PlayerInteractionCollision playerInteraction;

    [MenuItem("Testing/UtilsTools")]
    public static void ShowGameDebugUtilsWindow()
    {
        EditorWindow.GetWindow<EditorTestingTools>();   
    }

    private void OnGUI()
    {
        isGamePlay = EditorTestingTools.CheckIfGameIsPlay();

        GUILayout.Space(10);

        EditorStyles.label.fontStyle = FontStyle.Bold;
        if (isGamePlay)
        {
            EditorGUILayout.LabelField("TESTING - game is running");

            if (playerInteraction == null)
            {
                playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractionCollision>();
                EditorGUILayout.LabelField("Please add tag Player to playable plane");
            }
        }
        else
        {
            EditorGUILayout.LabelField("game is not running");
        }
        EditorStyles.label.fontStyle = FontStyle.Normal;

        GUILayout.Space(10);

        contentScrollPos = GUILayout.BeginScrollView(contentScrollPos);

        if (isGamePlay && playerInteraction !=null)
        {
            if (GUILayout.Button("POWERUP SHIELD", GUILayout.Height(30)))
            {
                playerInteraction.EditorTestPowerup(TypeOfPowerup.Shield);
            }

            if (GUILayout.Button("POWERUP SPEEDUP", GUILayout.Height(30)))
            {
                playerInteraction.EditorTestPowerup(TypeOfPowerup.SpeedUp);
            }

            if (GUILayout.Button("END GAME", GUILayout.Height(30)))
            {
                playerInteraction.EditorTestGameOver();
            }
        }

        GUILayout.EndScrollView();
    }


    public static bool CheckIfGameIsPlay()
    {
        return Application.isPlaying && Application.isEditor;
    }
}