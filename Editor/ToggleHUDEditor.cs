#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

public class ToggleHUDEditor : ShaderGUI
{
    //Declare Icons
    private static Texture2D UIGridTexture;
    private static Texture2D IconJSLogo;
    private static Texture2D IconDiscord;
    private static Texture2D IconGithub;
    private static Texture2D IconStore;
    private static TextureDownloader UIGridTextureDownloader;
    private static TextureDownloader IconJSLogoDownloader;
    private static TextureDownloader IconDiscordDownloader;
    private static TextureDownloader IconGithubDownloader;
    private static TextureDownloader IconStoreDownloader;
    private static bool TexturesInitialized = false;

    public ToggleHUDEditor(){
        if(UIGridTextureDownloader == null)
            UIGridTextureDownloader = new TextureDownloader("https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Sample/Textures/UI%20Grid%20Numbered.png");

        if(IconJSLogoDownloader == null)
            IconJSLogoDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/JSLogo.png");

        if(IconDiscordDownloader == null)
            IconDiscordDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png");

        if(IconGithubDownloader == null)
            IconGithubDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/GitHub.png");

        if(IconStoreDownloader == null)
            IconStoreDownloader = new TextureDownloader("https://github.com/JustSleightly/Resources/raw/main/Icons/Store.png");
    }
    
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        InitializeTextures();

        #region Declarations

        Material targetMat = materialEditor.target as Material;

        MaterialProperty mainTexture2D = ShaderGUI.FindProperty("_MainTex", properties);
        MaterialProperty xPos = ShaderGUI.FindProperty("_RotX", properties);
        MaterialProperty yPos = ShaderGUI.FindProperty("_RotY", properties);
        MaterialProperty distance = ShaderGUI.FindProperty("_Dist", properties);
        MaterialProperty width = ShaderGUI.FindProperty("_Width", properties);
        MaterialProperty height = ShaderGUI.FindProperty("_Height", properties);
        MaterialProperty rows = ShaderGUI.FindProperty("_Rows", properties);
        MaterialProperty columns = ShaderGUI.FindProperty("_Columns", properties);
        MaterialProperty flipHor = ShaderGUI.FindProperty("_FlipHorizontal", properties);
        MaterialProperty flipVer = ShaderGUI.FindProperty("_FlipVertical", properties);
        MaterialProperty flipOrder = ShaderGUI.FindProperty("_OrderVertical", properties);

        MaterialProperty[] togglesMaterial = new MaterialProperty[16]
        {
            ShaderGUI.FindProperty("_Toggle_0", properties),
            ShaderGUI.FindProperty("_Toggle_1", properties),
            ShaderGUI.FindProperty("_Toggle_2", properties),
            ShaderGUI.FindProperty("_Toggle_3", properties),
            ShaderGUI.FindProperty("_Toggle_4", properties),
            ShaderGUI.FindProperty("_Toggle_5", properties),
            ShaderGUI.FindProperty("_Toggle_6", properties),
            ShaderGUI.FindProperty("_Toggle_7", properties),
            ShaderGUI.FindProperty("_Toggle_8", properties),
            ShaderGUI.FindProperty("_Toggle_9", properties),
            ShaderGUI.FindProperty("_Toggle_10", properties),
            ShaderGUI.FindProperty("_Toggle_11", properties),
            ShaderGUI.FindProperty("_Toggle_12", properties),
            ShaderGUI.FindProperty("_Toggle_13", properties),
            ShaderGUI.FindProperty("_Toggle_14", properties),
            ShaderGUI.FindProperty("_Toggle_15", properties)
        };

        #endregion

        EditorGUILayout.Space(); //add space
        EditorGUILayout.Space(); //add space

        //Set Inputs
        
        //Sample Grid Texture
        if (UIGridTexture != null)
        {
            EditorGUILayout.LabelField("Please configure your texture sheet to be a 4 x 4 grid with the following numbering scheme:", (GUIStyle)"WordWrappedLabel");

            using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
            {
                GUILayout.FlexibleSpace();

                GUIContent buttonUIGridTexture = new GUIContent(UIGridTexture);
                buttonUIGridTexture.tooltip = "Sample UI Texture Sheet";

                if (GUILayout.Button(buttonUIGridTexture, "label", GUILayout.Width(150), GUILayout.Height(150)))
                {
                    Application.OpenURL("https://github.com/JustSleightly/ToggleHUD/tree/main/Sample/Textures");
                }

                GUILayout.FlexibleSpace();
            }

            EditorGUILayout.Space(); //add space
        }
        else
        {
            EditorGUILayout.LabelField("Please configure your texture sheet to be a 4 x 4 grid with the first icon starting in the bottom left, and continuing right:", (GUIStyle)"WordWrappedLabel");

            EditorGUILayout.Space(); //add space
        }

        using (new EditorGUILayout.VerticalScope("ObjectPickerPreviewBackground")) //Vertical Formatting
        {
            using (new EditorGUILayout.HorizontalScope("HelpBox")) //Horizontal Formatting
            {
                materialEditor.ShaderProperty(mainTexture2D, new GUIContent("UI Texture", "Provide an appropriate texture sheet/grid for your desired icons"));
            }

            using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
            {
                using (new EditorGUILayout.VerticalScope("HelpBox")) //Vertical Formatting
                {
                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("Rows", "Define how many rows you want to be displayed"), GUILayout.Width((float) Screen.width / 4));

                        EditorGUI.BeginChangeCheck();

                        rows.floatValue = EditorGUILayout.IntField((int) rows.floatValue);

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (rows.floatValue < 1)
                            {
                                rows.floatValue = 1;
                                Debug.LogWarning("[ToggleHUD] Rows cannot be less than 1");
                            }
                            else if ((rows.floatValue * columns.floatValue) > 16)
                            {
                                rows.floatValue = 16 / (int) columns.floatValue;
                                Debug.LogWarning("[ToggleHUD] Rows multiplied by Columns cannot be greater than 16");
                            }
                            else
                            {
                                rows.floatValue = Mathf.RoundToInt(rows.floatValue);
                            }
                        }
                    }

                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("Columns", "Define how many rows you want to be displayed"), GUILayout.Width((float) Screen.width / 4));

                        EditorGUI.BeginChangeCheck();

                        columns.floatValue = EditorGUILayout.IntField((int) columns.floatValue);

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (columns.floatValue < 1)
                            {
                                columns.floatValue = 1;
                                Debug.LogWarning("[ToggleHUD] Columns cannot be less than 1");
                            }
                            else if ((rows.floatValue * columns.floatValue) > 16)
                            {
                                columns.floatValue = 16 / (int) rows.floatValue;
                                Debug.LogWarning("[ToggleHUD] Rows multiplied by Columns cannot be greater than 16");
                            }
                            else
                            {
                                columns.floatValue = Mathf.RoundToInt(columns.floatValue);
                            }
                        }
                    }
                }

                using (new EditorGUILayout.VerticalScope("HelpBox")) //Vertical Formatting
                {
                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("Width", "Define the X scale of the UI"), GUILayout.Width((float)Screen.width / 4));
                        width.floatValue = EditorGUILayout.FloatField(width.floatValue);
                    }

                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("Height", "Define the Y scale of the UI"), GUILayout.Width((float)Screen.width / 4));
                        height.floatValue = EditorGUILayout.FloatField(height.floatValue);
                    }
                }
            }

            using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
            {
                using (new EditorGUILayout.VerticalScope("HelpBox")) //Vertical Formatting
                {
                    materialEditor.ShaderProperty(flipHor, new GUIContent("Flip Horizontal", "Swap the UI to render from right to left instead of left to right"));
                    materialEditor.ShaderProperty(flipVer, new GUIContent("Flip Vertical", "Swap the UI to render from top to bottom instead of bottom to top"));
                    materialEditor.ShaderProperty(flipOrder, new GUIContent("Flip Ordering", "Swap the direction the UI orders the icons to vertical instead of horizontal"));
                }

                using (new EditorGUILayout.VerticalScope("HelpBox")) //Vertical Formatting
                {
                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("X Position", "Define where the UI will render in X viewspace"), GUILayout.Width((float) Screen.width / 4));
                        xPos.floatValue = EditorGUILayout.FloatField(xPos.floatValue);
                    }

                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("Y Position", "Define where the UI will render in Y viewspace"), GUILayout.Width((float) Screen.width / 4));
                        yPos.floatValue = EditorGUILayout.FloatField(yPos.floatValue);
                    }
                    
                    using (new EditorGUILayout.HorizontalScope()) //Horizontal Formatting
                    {
                        EditorGUILayout.LabelField(new GUIContent("Distance", "Define the simulated distance from the view"), GUILayout.Width((float)Screen.width / 4));
                        distance.floatValue = EditorGUILayout.FloatField(distance.floatValue);
                    }
                }
            }

            if (Mathf.Approximately(flipOrder.floatValue, 0))
            {
                bool[,] toggles = new bool[(int)rows.floatValue, (int)columns.floatValue];
                int[,] togglevalues = new int[(int)rows.floatValue, (int)columns.floatValue];

                int initCount = 0;

                for (int i = 0; i < rows.floatValue; i++)
                {
                    for (int j = 0; j < columns.floatValue; j++)
                    {
                        toggles[i, j] = Mathf.Approximately(togglesMaterial[initCount].floatValue, 1);
                        togglevalues[i, j] = initCount + 1;
                        initCount++;
                    }
                }

                EditorGUI.BeginChangeCheck();

                using (new EditorGUILayout.VerticalScope("HelpBox")) //Vertical Formatting
                {
                    EditorGUILayout.LabelField(new GUIContent("Toggle UI Elements", "Toggle the display of each UI icon using the checkboxes below"), GUILayout.Width((float) Screen.width / 2));

                    EditorGUILayout.Space(); //add space

                    if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 0))
                    {
                        for (int i = (int) rows.floatValue - 1; i >= 0; i--)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (int j = 0; j < columns.floatValue; j++)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    else if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 1))
                    {
                        for (int i = 0; i < rows.floatValue; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (int j = 0; j < columns.floatValue; j++)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 0))
                    {
                        for (int i = (int) rows.floatValue - 1; i >= 0; i--)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (int j = (int) columns.floatValue - 1; j >= 0; j--)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 1))
                    {
                        for (int i = 0; i < rows.floatValue; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            for (int j = (int)columns.floatValue - 1; j >= 0; j--)
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                EditorGUILayout.EndVertical();
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        int finCount = 0;

                        for (int i = 0; i < rows.floatValue; i++)
                        {
                            for (int j = 0; j < columns.floatValue; j++)
                            {
                                togglesMaterial[finCount].floatValue = toggles[i, j] ? 1f : 0f;
                                finCount++;
                            }
                        }
                    }
                }
            }
            else
            {
                bool[,] toggles = new bool[(int)columns.floatValue, (int)rows.floatValue];
                int[,] togglevalues = new int[(int)columns.floatValue, (int)rows.floatValue];

                int initCount = 0;

                for (int i = 0; i < columns.floatValue; i++)
                {
                    for (int j = 0; j < rows.floatValue; j++)
                    {
                        toggles[i, j] = Mathf.Approximately(togglesMaterial[initCount].floatValue, 1);
                        togglevalues[i, j] = initCount + 1;
                        initCount++;
                    }
                }

                EditorGUI.BeginChangeCheck();

                using (new EditorGUILayout.VerticalScope("HelpBox")) //Vertical Formatting
                {
                    EditorGUILayout.LabelField(new GUIContent("Toggle UI Elements", "Toggle the display of each UI icon using the checkboxes below"), GUILayout.Width((float)Screen.width / 2));

                    EditorGUILayout.Space(); //add space

                    using (new EditorGUILayout.HorizontalScope()) //Vertical Formatting
                    {
                        if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 0))
                        {
                            for (int i = 0; i < columns.floatValue; i++)
                            {
                                EditorGUILayout.BeginVertical();
                                for (int j = (int)rows.floatValue - 1; j >= 0; j--)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        }
                        else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 0))
                        {
                            for (int i = (int)columns.floatValue - 1; i >= 0; i--)
                            {
                                EditorGUILayout.BeginVertical();
                                for (int j = (int)rows.floatValue - 1; j >= 0; j--)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        }
                        else if (Mathf.Approximately(flipHor.floatValue, 0) && Mathf.Approximately(flipVer.floatValue, 1))
                        {
                            for (int i = 0; i < columns.floatValue; i++)
                            {
                                EditorGUILayout.BeginVertical();
                                for (int j = 0; j < rows.floatValue; j++)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        }
                        else if (Mathf.Approximately(flipHor.floatValue, 1) && Mathf.Approximately(flipVer.floatValue, 1))
                        {
                            for (int i = (int)columns.floatValue - 1; i >= 0; i--)
                            {
                                EditorGUILayout.BeginVertical();
                                for (int j = 0; j < rows.floatValue; j++)
                                {
                                    EditorGUILayout.BeginHorizontal("HelpBox");
                                    toggles[i, j] = EditorGUILayout.ToggleLeft(new GUIContent(togglevalues[i, j].ToString() + " - " + (toggles[i, j] ? "On " : "Off"), "Toggles this UI " + (toggles[i, j] ? "Off " : "On")), toggles[i, j], GUILayout.Width(70));
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.EndVertical();
                            }
                        }

                        if (EditorGUI.EndChangeCheck())
                        {
                            int finCount = 0;

                            for (int i = 0; i < columns.floatValue; i++)
                            {
                                for (int j = 0; j < rows.floatValue; j++)
                                {
                                    togglesMaterial[finCount].floatValue = toggles[i, j] ? 1f : 0f;
                                    finCount++;
                                }
                            }
                        }
                    }
                }
            }
        }

        EditorGUILayout.Space(); //add space
        EditorGUILayout.Space(); //add space

        materialEditor.RenderQueueField();

        #region Credits

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // Horizontal Line Divider

        //Credits GUI
        using (new EditorGUILayout.HorizontalScope(GUILayout.Height(35))) //Horizontal Formatting
        {

            GUILayout.FlexibleSpace(); // Fill Space to Right Align

            JSCredits("Quantum");
        }
        using (new EditorGUILayout.HorizontalScope(GUILayout.Height(35))) //Horizontal Formatting
        {

            GUILayout.FlexibleSpace(); // Fill Space to Right Align

            JSCredits("JustSleightly");
        }

        #endregion
    }

    #region DownloadSampleUIGrid

    //Method to download a texture via webrequest and store in memory
    private class TextureDownloader
    {
        public Texture2D texture;
        private UnityWebRequest client;

        public TextureDownloader(string url)
        {
            client = new UnityWebRequest(url);
            client.downloadHandler = new DownloadHandlerBuffer();
            client.timeout = 10;
            client.SendWebRequest().completed += (asyncOperation) =>
            {
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.LogError(client.error);
                }

                Texture2D DownloadedIcon = new Texture2D(512, 512);
                DownloadedIcon.LoadImage(client.downloadHandler.data);
                texture = DownloadedIcon;
            };
        }
    }

    //Download Textures On Load
    private static void InitializeTextures()
    {
        if (!UIGridTexture)
            UIGridTexture = UIGridTextureDownloader.texture;

        if (!IconJSLogo)
            IconJSLogo = IconJSLogoDownloader.texture;

        if (!IconDiscord)
            IconDiscord = IconDiscordDownloader.texture;

        if (!IconGithub)
            IconGithub = IconGithubDownloader.texture;

        if (!IconStore)
            IconStore = IconStoreDownloader.texture;
    }

    //Method to download and display credits
    private static void JSCredits(string person)
    {
        //Create GUI Style for Name
        GUIStyle CreditsLabel = new GUIStyle(GUI.skin.label);
        CreditsLabel.alignment = TextAnchor.MiddleRight;
        CreditsLabel.fixedHeight = 35;
        CreditsLabel.richText = true;

        if (person == "JustSleightly")
        {
            //Name Label
            EditorGUILayout.LabelField("<size=14><i><b><color=#ff6961>JustSleightly#0001</color></b></i></size>", CreditsLabel, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(135));

            //JSLogo Icon
            if (IconJSLogo != null)
            {
                GUIContent buttonJSLogocontent = new GUIContent(IconJSLogo);
                buttonJSLogocontent.tooltip = "JustSleightly";

                if (GUILayout.Button(buttonJSLogocontent, "label", GUILayout.Width(35), GUILayout.Height(35)))
                {
                    Application.OpenURL("https://vrc.sleightly.dev/");
                }
            }

            //Discord Icon
            if (IconDiscord != null)
            {
                GUIContent buttonDiscordcontent = new GUIContent(IconDiscord);
                buttonDiscordcontent.tooltip = "JustSleightly's Discord";

                if (GUILayout.Button(buttonDiscordcontent, "label", GUILayout.Width(35), GUILayout.Height(35)))
                {
                    Application.OpenURL("https://discord.sleightly.dev/");
                }
            }

            //Store Icon
            if (IconStore != null)
            {
                GUIContent buttonStorecontent = new GUIContent(IconStore);
                buttonStorecontent.tooltip = "Store";

                if (GUILayout.Button(buttonStorecontent, "label", GUILayout.Width(35), GUILayout.Height(35)))
                {
                    Application.OpenURL("https://store.sleightly.dev/");
                }
            }
        }
        else if (person == "Quantum")
        {
            //Name Label
            EditorGUILayout.LabelField("<size=14><i><b><color=#ff6961>Quantum#0846</color></b></i></size>", CreditsLabel, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(135));

            //Discord Icon
            if (IconDiscord != null)
            {
                GUIContent buttonDiscordcontent = new GUIContent(IconDiscord);
                buttonDiscordcontent.tooltip = "Quantum's Discord";

                if (GUILayout.Button(buttonDiscordcontent, "label", GUILayout.Width(35), GUILayout.Height(35)))
                {
                    Application.OpenURL("https://discord.gg/UnuEEQGjn3");
                }
            }

            //GitHub Icon
            if (IconGithub != null)
            {
                GUIContent buttonGitHubcontent = new GUIContent(IconGithub);
                buttonGitHubcontent.tooltip = "GitHub";

                if (GUILayout.Button(buttonGitHubcontent, "label", GUILayout.Width(35), GUILayout.Height(35)))
                {
                    Application.OpenURL("https://github.sleightly.dev/ToggleHUD");
                }
            }
        }
    }

    #endregion
}
#endif
