#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Networking;

public class ToggleHUDEditor : ShaderGUI
{
    #region Declare/Download Icons

    private static Texture2D _uiGridTexture;
    private static Texture2D _iconJSLogo;
    private static Texture2D _iconDiscord;
    private static Texture2D _iconGithub;
    private static Texture2D _iconStore;
    private static TextureDownloader _uiGridTextureDownloader;
    private static TextureDownloader _iconJSLogoDownloader;
    private static TextureDownloader _iconDiscordDownloader;
    private static TextureDownloader _iconGithubDownloader;
    private static TextureDownloader _iconStoreDownloader;
    private AnimBool testValueConverter = new AnimBool();
    private AnimBool errorValueTypes = new AnimBool();
    private string testWidth = string.Empty;
    private string testHeight = string.Empty;
    private string testXPos = string.Empty;
    private string testYPos = string.Empty;
    private string testDistance = string.Empty;

    public ToggleHUDEditor()
    {
        if (_uiGridTextureDownloader == null)
            _uiGridTextureDownloader = new TextureDownloader("https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Sample/Textures/UI%20Grid%20Numbered.png");

        if (_iconJSLogoDownloader == null)
            _iconJSLogoDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/JSLogo.png");

        if (_iconDiscordDownloader == null)
            _iconDiscordDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png");

        if (_iconGithubDownloader == null)
            _iconGithubDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/GitHub.png");

        if (_iconStoreDownloader == null)
            _iconStoreDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/Store.png");
    }

    #endregion

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        InitializeTextures();

        #region Declarations

        var uiColor = FindProperty("_UIColor", properties);
        var mainTexture2D = FindProperty("_MainTex", properties);
        var xPos = FindProperty("_RotX", properties);
        var yPos = FindProperty("_RotY", properties);
        var distance = FindProperty("_Dist", properties);
        var width = FindProperty("_Width", properties);
        var height = FindProperty("_Height", properties);
        var rows = FindProperty("_Rows", properties);
        var columns = FindProperty("_Columns", properties);
        var flipHor = FindProperty("_FlipHorizontal", properties);
        var flipVer = FindProperty("_FlipVertical", properties);
        var flipOrder = FindProperty("_OrderVertical", properties);

        var togglesMaterial = new[]
        {
            FindProperty("_Toggle_0", properties),
            FindProperty("_Toggle_1", properties),
            FindProperty("_Toggle_2", properties),
            FindProperty("_Toggle_3", properties),
            FindProperty("_Toggle_4", properties),
            FindProperty("_Toggle_5", properties),
            FindProperty("_Toggle_6", properties),
            FindProperty("_Toggle_7", properties),
            FindProperty("_Toggle_8", properties),
            FindProperty("_Toggle_9", properties),
            FindProperty("_Toggle_10", properties),
            FindProperty("_Toggle_11", properties),
            FindProperty("_Toggle_12", properties),
            FindProperty("_Toggle_13", properties),
            FindProperty("_Toggle_14", properties),
            FindProperty("_Toggle_15", properties)
        };

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        #region Sample Grid Texture

        if (_uiGridTexture != null)
        {
            EditorGUILayout.LabelField("Please configure your texture sheet to be a 4 x 4 grid with the following numbering scheme:", (GUIStyle)"WordWrappedLabel");

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                var buttonUIGridTexture = new GUIContent(_uiGridTexture) { tooltip = "Sample UI Texture Sheet" };

                if (GUILayout.Button(buttonUIGridTexture, "label", GUILayout.Width(150), GUILayout.Height(150)))
                    Application.OpenURL("https://github.com/JustSleightly/ToggleHUD/tree/main/Sample/Textures");

                GUILayout.FlexibleSpace();
            }

            EditorGUILayout.Space();
        }
        else
        {
            EditorGUILayout.LabelField("Please configure your texture sheet to be a 4 x 4 grid with the first icon starting in the bottom left, and continuing right:", (GUIStyle)"WordWrappedLabel");

            EditorGUILayout.Space();
        }

        #endregion

        using (new EditorGUILayout.VerticalScope("ObjectPickerPreviewBackground"))
        {
            #region UI Color/Texture

            using (new EditorGUILayout.VerticalScope("HelpBox"))
            {
                materialEditor.ShaderProperty(uiColor, new GUIContent("UI Color", "Select an HDR color to multiply with the UI. The alpha should not be maxed to prevent OLED burn-in"));
                materialEditor.ShaderProperty(mainTexture2D, new GUIContent("UI Texture", "Provide an appropriate texture sheet/grid for your desired icons"));
            }

            #endregion

            #region Rows/Columns/Width/Height

            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope("HelpBox"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("Rows", "Define how many rows you want to be displayed"), GUILayout.Width((float)Screen.width / 4));

                        EditorGUI.BeginChangeCheck();

                        rows.floatValue = EditorGUILayout.IntField((int)rows.floatValue);

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (rows.floatValue < 1)
                            {
                                rows.floatValue = 1;
                                Debug.LogWarning("[ToggleHUD] Rows cannot be less than 1");
                            }
                            else if (rows.floatValue * columns.floatValue > 16)
                            {
                                rows.floatValue = 16 / (int)columns.floatValue;
                                Debug.LogWarning("[ToggleHUD] Rows multiplied by Columns cannot be greater than 16");
                            }
                            else
                            {
                                rows.floatValue = Mathf.RoundToInt(rows.floatValue);
                            }
                        }
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("Columns", "Define how many rows you want to be displayed"), GUILayout.Width((float)Screen.width / 4));

                        EditorGUI.BeginChangeCheck();

                        columns.floatValue = EditorGUILayout.IntField((int)columns.floatValue);

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (columns.floatValue < 1)
                            {
                                columns.floatValue = 1;
                                Debug.LogWarning("[ToggleHUD] Columns cannot be less than 1");
                            }
                            else if (rows.floatValue * columns.floatValue > 16)
                            {
                                columns.floatValue = 16 / (int)rows.floatValue;
                                Debug.LogWarning("[ToggleHUD] Rows multiplied by Columns cannot be greater than 16");
                            }
                            else
                            {
                                columns.floatValue = Mathf.RoundToInt(columns.floatValue);
                            }
                        }
                    }
                }

                using (new EditorGUILayout.VerticalScope("HelpBox"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("Width", "Define the X scale of the UI"), GUILayout.Width((float)Screen.width / 4));
                        width.floatValue = EditorGUILayout.FloatField(width.floatValue);
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("Height", "Define the Y scale of the UI"), GUILayout.Width((float)Screen.width / 4));
                        height.floatValue = EditorGUILayout.FloatField(height.floatValue);
                    }
                }
            }

            #endregion

            #region Flip/Position/Distance

            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope("HelpBox"))
                {
                    materialEditor.ShaderProperty(flipHor, new GUIContent("Flip Horizontal", "Swap the UI to render from right to left instead of left to right"));
                    materialEditor.ShaderProperty(flipVer, new GUIContent("Flip Vertical", "Swap the UI to render from top to bottom instead of bottom to top"));
                    materialEditor.ShaderProperty(flipOrder, new GUIContent("Flip Ordering", "Swap the direction the UI orders the icons to vertical instead of horizontal"));
                }

                using (new EditorGUILayout.VerticalScope("HelpBox"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("X Position", "Define where the UI will render in X view space"), GUILayout.Width((float)Screen.width / 4));
                        xPos.floatValue = EditorGUILayout.FloatField(xPos.floatValue);
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("Y Position", "Define where the UI will render in Y view space"), GUILayout.Width((float)Screen.width / 4));
                        yPos.floatValue = EditorGUILayout.FloatField(yPos.floatValue);
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent("Distance", "Define the simulated distance from the view"), GUILayout.Width((float)Screen.width / 4));
                        distance.floatValue = EditorGUILayout.FloatField(distance.floatValue);
                    }
                }
            }

            #endregion

            #region Toggle UI Elements

            if (Mathf.Approximately(flipOrder.floatValue, 0))
            {
                var toggles = new bool[(int)rows.floatValue, (int)columns.floatValue];
                var toggleValues = new int[(int)rows.floatValue, (int)columns.floatValue];

                var initCount = 0;

                for (var i = 0; i < rows.floatValue; i++)
                for (var j = 0; j < columns.floatValue; j++)
                {
                    toggles[i, j] = Mathf.Approximately(togglesMaterial[initCount].floatValue, 1);
                    toggleValues[i, j] = initCount + 1;
                    initCount++;
                }

                EditorGUI.BeginChangeCheck();

                using (new EditorGUILayout.VerticalScope("HelpBox"))
                {
                    EditorGUILayout.LabelField(new GUIContent("Toggle UI Elements", "Toggle the display of each UI icon using the checkboxes below"), GUILayout.Width((float)Screen.width / 2));

                    if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 0))
                        for (var i = (int)rows.floatValue - 1; i >= 0; i--)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (var j = 0; j < columns.floatValue; j++)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    else if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 1))
                        for (var i = 0; i < rows.floatValue; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (var j = 0; j < columns.floatValue; j++)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 0))
                        for (var i = (int)rows.floatValue - 1; i >= 0; i--)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (var j = (int)columns.floatValue - 1; j >= 0; j--)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 1))
                        for (var i = 0; i < rows.floatValue; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (var j = (int)columns.floatValue - 1; j >= 0; j--)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }

                    if (EditorGUI.EndChangeCheck())
                    {
                        var finCount = 0;

                        for (var i = 0; i < rows.floatValue; i++)
                        for (var j = 0; j < columns.floatValue; j++)
                        {
                            togglesMaterial[finCount].floatValue = toggles[i, j] ? 1f : 0f;
                            finCount++;
                        }
                    }
                }
            }
            else
            {
                var toggles = new bool[(int)columns.floatValue, (int)rows.floatValue];
                var toggleValues = new int[(int)columns.floatValue, (int)rows.floatValue];

                var initCount = 0;

                for (var i = 0; i < columns.floatValue; i++)
                for (var j = 0; j < rows.floatValue; j++)
                {
                    toggles[i, j] = Mathf.Approximately(togglesMaterial[initCount].floatValue, 1);
                    toggleValues[i, j] = initCount + 1;
                    initCount++;
                }

                EditorGUI.BeginChangeCheck();

                using (new EditorGUILayout.VerticalScope("HelpBox"))
                {
                    EditorGUILayout.LabelField(new GUIContent("Toggle UI Elements", "Toggle the display of each UI icon using the checkboxes below"), GUILayout.Width((float)Screen.width / 2));

                    EditorGUILayout.Space();

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 0))
                            for (var i = 0; i < columns.floatValue; i++)
                            {
                                EditorGUILayout.BeginVertical();
                                for (var j = (int)rows.floatValue - 1; j >= 0; j--)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 0))
                            for (var i = (int)columns.floatValue - 1; i >= 0; i--)
                            {
                                EditorGUILayout.BeginVertical();
                                for (var j = (int)rows.floatValue - 1; j >= 0; j--)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        else if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 1))
                            for (var i = 0; i < columns.floatValue; i++)
                            {
                                EditorGUILayout.BeginVertical();
                                for (var j = 0; j < rows.floatValue; j++)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 1))
                            for (var i = (int)columns.floatValue - 1; i >= 0; i--)
                            {
                                EditorGUILayout.BeginVertical();
                                for (var j = 0; j < rows.floatValue; j++)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(toggleValues[i, j] + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }

                        if (EditorGUI.EndChangeCheck())
                        {
                            var finCount = 0;

                            for (var i = 0; i < columns.floatValue; i++)
                            for (var j = 0; j < rows.floatValue; j++)
                            {
                                togglesMaterial[finCount].floatValue = toggles[i, j] ? 1f : 0f;
                                finCount++;
                            }
                        }
                    }
                }
            }

            #endregion

            #region Test Value Converter

            using (new EditorGUILayout.VerticalScope())
            {
                using (new EditorGUILayout.HorizontalScope("HelpBox"))
                {
                    EditorGUILayout.LabelField(new GUIContent("Anim Value Converter ", "If you've obtained values you'd like to convert from the ToggleHUD prefab tester, please enter them here."), GUILayout.Width(140), GUILayout.ExpandWidth(false));

                    testValueConverter.target = EditorGUILayout.Foldout(testValueConverter.target, "", true);

                    GUILayout.FlexibleSpace();
                }

                if (EditorGUILayout.BeginFadeGroup(testValueConverter.faded))
                {
                    EditorGUIUtility.labelWidth = 100;
                    using (new EditorGUILayout.VerticalScope("HelpBox"))
                    {
                        testWidth = EditorGUILayout.TextField("Width", testWidth);
                        testHeight = EditorGUILayout.TextField("Height", testHeight);
                    }

                    using (new EditorGUILayout.VerticalScope("HelpBox"))
                    {
                        testXPos = EditorGUILayout.TextField("XPos", testXPos);
                        testYPos = EditorGUILayout.TextField("YPos", testYPos);
                        testDistance = EditorGUILayout.TextField("Distance", testDistance);
                    }

                    EditorGUIUtility.labelWidth = 0;

                    float convertWidth, convertHeight, convertXPos, convertYPos, convertDistance;

                    bool errorWidth = float.TryParse(testWidth, out convertWidth) || string.IsNullOrWhiteSpace(testWidth);
                    bool errorHeight = float.TryParse(testHeight, out convertHeight) || string.IsNullOrWhiteSpace(testHeight);
                    bool errorXPos = float.TryParse(testXPos, out convertXPos) || string.IsNullOrWhiteSpace(testXPos);
                    bool errorYPos = float.TryParse(testYPos, out convertYPos) || string.IsNullOrWhiteSpace(testYPos);
                    bool errorDistance = float.TryParse(testDistance, out convertDistance) || string.IsNullOrWhiteSpace(testDistance);

                    errorValueTypes.target = !(errorWidth && errorHeight && errorXPos && errorYPos && errorDistance);

                    if (EditorGUILayout.BeginFadeGroup(errorValueTypes.faded))
                    {
                        using (new EditorGUILayout.VerticalScope("HelpBox"))
                        {
                            EditorGUILayout.HelpBox("Please input valid numbers in order to convert!", MessageType.Error);
                        }
                    }

                    EditorGUILayout.EndFadeGroup();

                    void ConvertTester(MaterialEditor editor, MaterialProperty property, string test, float convert, string undo)
                    {
                        if (!string.IsNullOrWhiteSpace(test) && float.TryParse(test, out convert))
                        {
                            float newValue = 0;

                            switch (undo)
                            {
                                case "Width":
                                    newValue = convert * 20;
                                    break;
                                case "Height":
                                    newValue = convert * 20;
                                    break;
                                case "X Position":
                                    newValue = (convert - 0.5f) * 100;
                                    break;
                                case "Y Position":
                                    newValue = (convert - 0.5f) * 80;
                                    break;
                                case "Distance":
                                    newValue = convert * 100 * 0.015f + 0.5f;
                                    break;
                                default:
                                    Debug.LogError("<color=yellow>[ToggleHUD]</color> Property Not Found");
                                    break;
                            }

                            property.floatValue = newValue;
                            Undo.RecordObject(editor.target, $"Convert {undo}");
                        }
                    }

                    EditorGUI.BeginDisabledGroup(errorValueTypes.value);
                    if (GUILayout.Button("Convert"))
                    {
                        ConvertTester(materialEditor, width, testWidth, convertWidth, "Width");
                        ConvertTester(materialEditor, height, testHeight, convertHeight, "Height");
                        ConvertTester(materialEditor, xPos, testXPos, convertXPos, "X Position");
                        ConvertTester(materialEditor, yPos, testYPos, convertYPos, "Y Position");
                        ConvertTester(materialEditor, distance, testDistance, convertDistance, "Distance");
                    }

                    EditorGUI.EndDisabledGroup();
                }

                EditorGUILayout.EndFadeGroup();
            }

            #endregion
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        materialEditor.RenderQueueField();

        #region Credits

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        using (new EditorGUILayout.HorizontalScope(GUILayout.Height(35)))
        {
            GUILayout.FlexibleSpace();

            JSCredits("Quantum");
        }

        using (new EditorGUILayout.HorizontalScope(GUILayout.Height(35)))
        {
            GUILayout.FlexibleSpace();

            JSCredits("JustSleightly");
        }

        #endregion
    }

    #region DownloadSampleUIGrid

    //Method to Download a Texture via Web Request and Store in Memory
    private class TextureDownloader
    {
        public Texture2D Texture;

        public TextureDownloader(string url)
        {
            var client = new UnityWebRequest(url);
            client.downloadHandler = new DownloadHandlerBuffer();
            client.timeout = 10;
            client.SendWebRequest().completed += asyncOperation =>
            {
                if (client.isNetworkError || client.isHttpError)
                    Debug.LogError(client.error);

                var downloadedIcon = new Texture2D(512, 512);
                downloadedIcon.LoadImage(client.downloadHandler.data);
                Texture = downloadedIcon;
            };
        }
    }

    //Download Textures On Load
    private static void InitializeTextures()
    {
        if (!_uiGridTexture)
            _uiGridTexture = _uiGridTextureDownloader.Texture;

        if (!_iconJSLogo)
            _iconJSLogo = _iconJSLogoDownloader.Texture;

        if (!_iconDiscord)
            _iconDiscord = _iconDiscordDownloader.Texture;

        if (!_iconGithub)
            _iconGithub = _iconGithubDownloader.Texture;

        if (!_iconStore)
            _iconStore = _iconStoreDownloader.Texture;
    }

    //Method to Download and Display Credits
    private static void JSCredits(string person)
    {
        var creditsLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, fixedHeight = 35, richText = true };

        switch (person)
        {
            case "JustSleightly":
            {
                EditorGUILayout.LabelField("<size=14><i><b><color=#ff6961>JustSleightly#0001</color></b></i></size>", creditsLabel, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(135));

                if (_iconJSLogo != null)
                    if (GUILayout.Button(new GUIContent(_iconJSLogo) { tooltip = "JustSleightly" }, "label", GUILayout.Width(35), GUILayout.Height(35)))
                        Application.OpenURL("https://vrc.sleightly.dev/");

                if (_iconDiscord != null)
                    if (GUILayout.Button(new GUIContent(_iconDiscord) { tooltip = "JustSleightly's Discord" }, "label", GUILayout.Width(35), GUILayout.Height(35)))
                        Application.OpenURL("https://discord.sleightly.dev/");

                if (_iconStore != null)
                    if (GUILayout.Button(new GUIContent(_iconStore) { tooltip = "Store" }, "label", GUILayout.Width(35), GUILayout.Height(35)))
                        Application.OpenURL("https://store.sleightly.dev/");
                break;
            }
            case "Quantum":
            {
                EditorGUILayout.LabelField("<size=14><i><b><color=#ff6961>Quantum#0846</color></b></i></size>", creditsLabel, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(135));

                if (_iconDiscord != null)
                    if (GUILayout.Button(new GUIContent(_iconDiscord) { tooltip = "Quantum's Discord" }, "label", GUILayout.Width(35), GUILayout.Height(35)))
                        Application.OpenURL("https://quantum.sleightly.dev/");

                if (_iconGithub != null)
                    if (GUILayout.Button(new GUIContent(_iconGithub) { tooltip = "GitHub" }, "label", GUILayout.Width(35), GUILayout.Height(35)))
                        Application.OpenURL("https://github.sleightly.dev/togglehud");
                break;
            }
        }
    }

    #endregion
}
#endif