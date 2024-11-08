using UnityEngine;

/// <summary>
/// This script either hides or displays the mouse cursor based on game state
/// </summary>
public class CursorFocus : MonoBehaviour
{
    void Update()
    {
        if(GameManager.instance.IsPaused()) //enable cursor if paused, takes priority over gameplay state
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }else{
            //Switches on current gamestate.
            switch (GameManager.instance.GetGameState())
            {
                //Cursor Hidden
                case GameState.GamePlay: 
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;

                //Cursor Visible. Defaults to visible
                case GameState.GameOver:
                case GameState.Setup:
                case GameState.GameWin:
                
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;

                default: 
                    Debug.LogWarning("Missing cursor focus state. Defaulting to visible cursor");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }
        
    }
}