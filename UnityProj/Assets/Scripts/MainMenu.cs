using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	
	[System.Serializable]
	public class LevelDefinition {
		public string levelName;
		public string sceneName;
	}
	
	public LevelDefinition[] levelDefinitions;
	
	
	public Rect firstButtonRect = new Rect(100, 600, 300, 80);
	public GUIStyle buttonStyle;
	public Vector2 buttonRectOffsets = new Vector2(350, 120);
	public int numLevelsPerRow = 2;
	
	void OnGUI() {
		for (int i=0; i<levelDefinitions.Length; i++) {
			int row = i / numLevelsPerRow;
			int col = i % numLevelsPerRow;
			Rect buttonRect = new Rect(firstButtonRect);
			buttonRect.y += row * buttonRectOffsets.y;
			buttonRect.x += col * buttonRectOffsets.x;
			if (GUI.Button(buttonRect, levelDefinitions[i].levelName)) {
				Application.LoadLevel(levelDefinitions[i].sceneName);
			}
		}
	}
	
	[ContextMenu("Defaultbuttonstyle")]
	void SetDefaultGuiSkin()
	{
		buttonStyle = GUI.skin.button;
	}
}
