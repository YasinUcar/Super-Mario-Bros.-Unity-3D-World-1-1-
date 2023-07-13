using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class ImageViewerEditor : EditorWindow {
	public static Texture pinIcon;
	public static float zoomMultiplier = .1f;
	public static float zoomMinLimit = .01f;
	public static float zoomMaxLimit = 100;
	public static bool resetTransformationsOnSwitch = true;

	public const int imageViewHeight = 130;
	public const int toolbarHeight = 30;
	public const int imageListHeight = 100;
	public const int foldoutHeight = 16;

	/// <summary>
	/// A texture was selected
	/// </summary>
	public event Action<Texture> OnSelectTexture;

	/// <summary>
	/// A texture was locked/pinned to the viewer
	/// </summary>
	public event Action<Texture> OnTextureLocked;

	/// <summary>
	/// A texture was unlocked/unpinned from the viewer
	/// </summary>
	public event Action<Texture> OnTextureUnlocked;

	public float zoom = 1;
	public ScaleType scaleType = ScaleType.Fit;

	public List<Texture> allImageViewerTextures = new List<Texture>();
	public List<Texture> lockedTextures = new List<Texture>();
	public List<Texture> projectPaneSelection = new List<Texture>();
	public List<Texture> imageViewerSelection = new List<Texture>();

	public Vector2 centeredOffset = Vector2.zero;

	public bool showToolbar = true;

	Vector2 scrollView = Vector2.zero;
	float scroll;

	Vector2 imageMoveStart;
	Vector2 previousCenteredOffset;

	Rect imageRect;
	Rect imageViewRect;

	public enum ScaleType {
		Centered = 0,
		Fit = 1
	}

	[MenuItem("Window/Image Viewer")]
	static void Init() {
		ImageViewerEditor viewer = CreateInstance<ImageViewerEditor>();
		viewer.Show();
		viewer.title = "Image Viewer";

		GetPrefs();
	}

	[PreferenceItem("Image Viewer")]
	static void PreferencesGUI() {
		GetPrefs();

		zoomMultiplier = Mathf.Clamp(EditorGUILayout.FloatField("Zoom Multiplier", zoomMultiplier), 0, float.MaxValue);
		zoomMinLimit = Mathf.Clamp(EditorGUILayout.FloatField("Zoom Minimum Limit", zoomMinLimit), 0, zoomMaxLimit);
		zoomMaxLimit = Mathf.Clamp(EditorGUILayout.FloatField("Zoom Maximum Limit", zoomMaxLimit), zoomMinLimit, float.MaxValue);
		resetTransformationsOnSwitch = EditorGUILayout.Toggle(new GUIContent("Reset On Mode Switch", "Resets Zoom and Pan Offset when switching image modes"), resetTransformationsOnSwitch);

		if(GUILayout.Button("Reset To Defaults")) {
			if(EditorPrefs.HasKey("ImageViewer_ZoomMultiplier")) EditorPrefs.SetFloat("ImageViewer_ZoomMultiplier", .1f);
			if(EditorPrefs.HasKey("ImageViewer_ZoomMinLimit")) EditorPrefs.SetFloat("ImageViewer_ZoomMinLimit", .01f);
			if(EditorPrefs.HasKey("ImageViewer_ZoomMaxLimit")) EditorPrefs.SetFloat("ImageViewer_ZoomMaxLimit", 100);
			if(EditorPrefs.HasKey("ImageViewer_ResetTransforms")) EditorPrefs.SetBool("ImageViewer_ResetTransforms", true);

			GetPrefs();
		}

		if(GUI.changed) {
			EditorPrefs.SetFloat("ImageViewer_ZoomMultiplier", zoomMultiplier);
			EditorPrefs.SetFloat("ImageViewer_ZoomMinLimit", zoomMinLimit);
			EditorPrefs.SetFloat("ImageViewer_ZoomMaxLimit", zoomMaxLimit);
			EditorPrefs.SetBool("ImageViewer_ResetTransforms", resetTransformationsOnSwitch);
		}
	}

	static void GetPrefs() {
		zoomMultiplier = EditorPrefs.GetFloat("ImageViewer_ZoomMultiplier", .1f);
		zoomMinLimit = EditorPrefs.GetFloat("ImageViewer_ZoomMinLimit", .01f);
		zoomMaxLimit = EditorPrefs.GetFloat("ImageViewer_ZoomMaxLimit", 100);
		resetTransformationsOnSwitch = EditorPrefs.GetBool("ImageViewer_ResetTransforms", true);
	}

	void OnFocus() {
		UpdateSelection();
	}

	void OnGUI() {
		if(!pinIcon)
			pinIcon = AssetDatabase.LoadAssetAtPath("Assets/ImageViewer/PinIcon.png", typeof(Texture2D)) as Texture;

		DrawSelectedTexture();
		DrawToolbarToggle();
		DrawToolbar();
	}

	void OnSelectionChange() {
		UpdateSelection();
	}

	void UpdateSelection() {
		projectPaneSelection.Clear();

		foreach(Texture a in Selection.GetFiltered(typeof(Texture), SelectionMode.Assets))
			projectPaneSelection.Add(a);

		allImageViewerTextures.Clear();

		foreach(Texture a in lockedTextures)
			allImageViewerTextures.Add(a);

		foreach(Texture a in projectPaneSelection)
			if(!lockedTextures.Contains(a))
				allImageViewerTextures.Add(a);

		Repaint();
	}

	void DrawSelectedTexture() {
		float imageViewCenterVerticalPos = position.height - (showToolbar ? imageViewHeight + foldoutHeight : foldoutHeight);
		imageRect = new Rect();
		imageViewRect = new Rect(0, 0, position.width, imageViewCenterVerticalPos);

		float ratio = imageViewRect.width / imageViewRect.height;

		if(scaleType == ScaleType.Centered) ratio = 1;

		int xSize = Mathf.Min(imageViewerSelection.Count, Mathf.CeilToInt(Mathf.Sqrt(imageViewerSelection.Count) * ratio));
		int ySize = Mathf.CeilToInt((float)(imageViewerSelection.Count / (double)xSize));

		if(xSize < 1) xSize = 1;
		if(ySize < 1) ySize = 1;

		float width = imageViewRect.width / xSize;
		float height = imageViewRect.height / ySize;

		switch(scaleType) {
			case ScaleType.Centered:
				if(imageViewerSelection.Count == 1) {
					width = imageViewerSelection[0].width;
					height = imageViewerSelection[0].height;
				}
				else {
					if(width >= height)
						height = width;
					else
						width = height;
				}

				width *= zoom;
				height *= zoom;

				imageRect = new Rect(0, 0, width, height);
				imageRect.center = imageViewRect.center;
				break;

			case ScaleType.Fit:
				imageRect = new Rect(0, 0, position.width, imageViewCenterVerticalPos);
				break;
		}

		int imageViewControlID = GUIUtility.GetControlID(imageViewRect.GetHashCode(), FocusType.Passive);

		if(scaleType == ScaleType.Centered) {
			switch(Event.current.type) {
				case EventType.ScrollWheel:
					if(imageViewRect.Contains(Event.current.mousePosition)) {
						ZoomScaled(Mathf.Sign(Event.current.delta.y) * zoomMultiplier);

						Event.current.Use();
					}

					break;

				case EventType.MouseDown:
					if(Event.current.button == 0 || Event.current.button == 2)
						if(imageViewRect.Contains(Event.current.mousePosition)) {
							imageMoveStart = Event.current.mousePosition;
							previousCenteredOffset = centeredOffset;

							GUIUtility.hotControl = imageViewControlID;
							Event.current.Use();
						}

					break;

				case EventType.MouseUp:
					if(Event.current.button == 0 || Event.current.button == 2)
						if(GUIUtility.hotControl == imageViewControlID) {
							GUIUtility.hotControl = 0;
							Event.current.Use();
						}

					break;

				case EventType.MouseDrag:
					if(Event.current.button == 0 || Event.current.button == 2)
						if(GUIUtility.hotControl == imageViewControlID) {
							Event.current.Use();
							centeredOffset = (Event.current.mousePosition - imageMoveStart) + previousCenteredOffset;
						}

					break;

				case EventType.Repaint:
					for(int i = 0; i < imageViewerSelection.Count; i++) {
						float x = imageViewRect.center.x + centeredOffset.x + (i % xSize) * width;
						float y = imageViewRect.center.y + centeredOffset.y + (i / xSize % ySize) * height;

						GUI.DrawTexture(new Rect(x - imageRect.width * xSize * .5f, y - imageRect.height * ySize * .5f, width, height), imageViewerSelection[i], ScaleMode.ScaleToFit);
					}
					break;
			}
		}
		else {
			for(int i = 0; i < imageViewerSelection.Count; i++) {
				float x = imageRect.x + (i % xSize) * width;
				float y = imageRect.y + (i / xSize % ySize) * height;

				GUI.DrawTexture(new Rect(x, y, width, height), imageViewerSelection[i], ScaleMode.ScaleToFit);
			}
		}
	}

	void DrawToolbarToggle() {
		if(showToolbar) {
			if(GUI.Button(new Rect(0, position.height - (imageViewHeight) - foldoutHeight, position.width, foldoutHeight), "v"))
				showToolbar = false;
		}
		else {
			if(GUI.Button(new Rect(0, position.height - foldoutHeight, position.width, foldoutHeight), "^"))
				showToolbar = true;
		}
	}

	void DrawToolbar() {
		if(showToolbar) {
			if(scaleType == ScaleType.Fit) GUI.enabled = false;

			Rect zoomBox = new Rect(0, position.height - imageViewHeight, position.width * .5f, toolbarHeight);
			GUI.backgroundColor = new Color(.75f, .75f, .75f, 1);

			if(Event.current.type == EventType.Repaint)
				GUI.skin.textArea.Draw(zoomBox, false, false, false, false);

			GUI.backgroundColor = Color.white;
			zoomBox.y += 8;
			ZoomSet(EditorGUI.Slider(new Rect(zoomBox.x + 5, zoomBox.y, zoomBox.width - 10, zoomBox.height - 15), zoom, zoomMinLimit, zoomMaxLimit));

			GUI.enabled = true;

			Rect controlBox = new Rect(position.width * .5f, position.height - imageViewHeight, position.width * .5f + 1, toolbarHeight);

			if(Event.current.type == EventType.Repaint)
				GUI.skin.textArea.Draw(controlBox, false, false, false, false);

			GUILayout.BeginArea(controlBox);
			GUILayout.BeginVertical();
			GUILayout.Space(13);

			GUILayout.BeginHorizontal();
			ScaleType oldScaleType = scaleType;
			scaleType = (ScaleType)System.Enum.Parse(typeof(ScaleType), GUILayout.Toolbar((int)scaleType, System.Enum.GetNames(typeof(ScaleType))).ToString());

			if(resetTransformationsOnSwitch && scaleType != oldScaleType) {
				zoom = 1;
				centeredOffset = Vector2.zero;
			}

			GUILayout.Space(40);

			if(centeredOffset == Vector2.zero)
				GUI.enabled = false;

			if(GUILayout.Button("Reset Offset"))
				centeredOffset = Vector2.zero;

			GUI.enabled = true;
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
			GUILayout.EndArea();

			Rect imageList = new Rect(0, position.height - imageListHeight, position.width, imageListHeight);

			if(Event.current.type == EventType.Repaint)
				GUI.skin.textArea.Draw(imageList, false, false, false, false);

			GUILayout.BeginArea(imageList);
			scrollView = GUILayout.BeginScrollView(scrollView);
			GUILayout.BeginHorizontal();

			foreach(Texture a in allImageViewerTextures.ToArray()) {
				GUI.backgroundColor = Color.white;

				Rect buttonRect = GUILayoutUtility.GetRect(80, 60, GUI.skin.button, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
				Rect lastRect = buttonRect;
				Rect lockRect = new Rect(lastRect.x - 6, lastRect.yMax - 12, 35, 28);

				if(imageViewerSelection.Contains(a))
					GUI.backgroundColor = new Color(.5f, .75f, 1);

				if(GUIHelper.UnderButton(buttonRect, new GUIContent(a, a.name + "\n" + a.width + "x" + a.height), lockRect)) {
					if(!Event.current.shift)
						SelectAll(false);

					ToggleViewTexture(a);
				}

				bool inLockTexture = lockedTextures.Contains(a);
				if(inLockTexture)
					GUI.backgroundColor = new Color(.5f, 1, 0);

				Color color = GUI.color;
				color.a = .8f;
				GUI.color = color;

				if(GUI.Button(lockRect, new GUIContent(pinIcon, inLockTexture ? "Unpin from viewer" : "Pin to viewer")))
					if(inLockTexture) {
						if(lockedTextures.Remove(a)) {
							if(!projectPaneSelection.Contains(a))
								allImageViewerTextures.Remove(a);

							if(OnTextureUnlocked != null)
								OnTextureUnlocked(a);
						}
					}
					else {
						lockedTextures.Add(a);

						if(OnTextureLocked != null)
							OnTextureLocked(a);
					}

				GUILayout.Space(5);
			}

			GUI.backgroundColor = Color.white;

			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}

	/// <summary>
	/// Toggles selection on a single texture/image in the toolbar
	/// </summary>
	/// <param name="texture"></param>
	public void ToggleViewTexture(Texture texture) {
		if(!imageViewerSelection.Remove(texture)) {
			imageViewerSelection.Add(texture);

			if(OnSelectTexture != null)
				OnSelectTexture(texture);
		}
	}

	/// <summary>
	/// Selects or unselects all textures/images within the list in the toolbar
	/// </summary>
	/// <param name="selectAll"></param>
	public void SelectAll(bool selectAll) {
		imageViewerSelection.Clear();

		if(selectAll) {
			imageViewerSelection.AddRange(allImageViewerTextures);

			if(OnSelectTexture != null)
				foreach(Texture a in imageViewerSelection)
					OnSelectTexture(a);
		}
	}

	/// <summary>
	/// Sets the zoom to the amount
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	public float ZoomSet(float amount) {
		float oldZoom = zoom;

		zoom = amount;
		zoom = Mathf.Clamp(zoom, zoomMinLimit, zoomMaxLimit);

		if(!Mathf.Approximately(zoom, oldZoom)) {
			Vector2 tempOffset = imageRect.center + centeredOffset;

			centeredOffset += (imageViewRect.center - tempOffset) * (1 - (zoom / oldZoom));
		}

		return zoom;
	}

	/// <summary>
	/// Adds the amount to the zoom
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	public float ZoomAdd(float amount) {
		float oldZoom = zoom;

		zoom += amount;
		zoom = Mathf.Clamp(zoom, zoomMinLimit, zoomMaxLimit);

		if(!Mathf.Approximately(zoom, oldZoom)) {
			Vector2 tempOffset = imageRect.center + centeredOffset;

			centeredOffset += (imageViewRect.center - tempOffset) * (1 - (zoom / oldZoom));
		}

		return zoom;
	}

	/// <summary>
	/// Preforms (zoom -= zoom * amount) which gradually scales the zoom the higher zoom is
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	public float ZoomScaled(float amount) {
		float oldZoom = zoom;

		zoom -= zoom * amount;
		zoom = Mathf.Clamp(zoom, zoomMinLimit, zoomMaxLimit);

		if(!Mathf.Approximately(zoom, oldZoom)) {
			Vector2 tempOffset = imageRect.center + centeredOffset;

			centeredOffset += (imageViewRect.center - tempOffset) * (1 - (zoom / oldZoom));
		}

		return zoom;
	}

	Vector2 RatioToFraction(float ratio) {
		double decimalPower = ratio;
		int decimalPlaces = (ratio - (int)ratio).ToString().Length - 2;
		int divisor = 1;

		for(int i = 0; i < decimalPlaces; i++) {
			decimalPower *= 10;
			divisor *= 10;
		}

		int dividend = (int)decimalPower;
		int greatestCommonDenominator = dividend, b = divisor;

		while(b != 0) {
			int remainder = b;
			b = greatestCommonDenominator % b;
			greatestCommonDenominator = remainder;
		}

		return new Vector2(dividend / greatestCommonDenominator, divisor / greatestCommonDenominator);
	}
}

public class GUIHelper {
	/// <summary>
	/// A button that preforms a check for rects above this button to allow for buttons-within-buttons
	/// </summary>
	/// <param name="rect"></param>
	/// <param name="content"></param>
	/// <param name="higherRects">The top-layer of rects above this rect</param>
	/// <returns></returns>
	public static bool UnderButton(Rect rect, GUIContent content, params Rect[] higherRects) {
		int imageButtonControlID = GUIUtility.GetControlID(rect.GetHashCode(), FocusType.Passive);

		switch(Event.current.GetTypeForControl(imageButtonControlID)) {
			case EventType.MouseDown:
				foreach(Rect a in higherRects)
					if(a.Contains(Event.current.mousePosition)) return false;

				if(rect.Contains(Event.current.mousePosition)) {
					GUIUtility.hotControl = imageButtonControlID;
					Event.current.Use();
				}

				break;

			case EventType.MouseUp:
				if(GUIUtility.hotControl == imageButtonControlID) {
					GUIUtility.hotControl = 0;
					Event.current.Use();

					foreach(Rect a in higherRects)
						if(a.Contains(Event.current.mousePosition)) return false;

					return rect.Contains(Event.current.mousePosition);
				}

				break;

			case EventType.MouseDrag:
				if(GUIUtility.hotControl == imageButtonControlID)
					Event.current.Use();

				break;
			case EventType.Repaint:
				GUI.skin.button.Draw(rect, content, imageButtonControlID);
				break;
		}

		return false;
	}
}