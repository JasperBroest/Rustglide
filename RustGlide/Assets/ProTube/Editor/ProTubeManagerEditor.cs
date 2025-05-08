using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;


[CustomEditor(typeof(ProTubeManager))]
public class ProTubeManagerEditor : Editor
{
    bool isAutoShooting = false; // Track the auto-shoot state
    double lastShootTime = 0;
    double currentTime = 0;
    float autoShootInterval = 0.13f;
    Button autoShootBtn = null; // Declare the button to update its text dynamically
    private ForceTubeVRChannel channelTarget;
    private PopupField<ProTubeSettings> settingsDropdown;
    private Button saveButton;
    // Declare sliders as class-level variables
    private SliderWithField kickPowerSliderWithField;
    private SliderWithField rumblePowerSliderWithField;
    private SliderWithField rumbleDurationSliderWithField;
    private SliderWithField autoShootIntervalSliderWithField;

    byte rumblePower = 255; // Default shoot power
    float rumbleDuration = 0.5f; // Default shoot duration
    byte kickPower = 255; // Default kick power

    [MenuItem("GameObject/ProTube/Add ProTubeManager", false, 10)]
    [MenuItem("ProTube/Add ProTubeManager", false, 10)]
    public static void CreateProTubeManager()
    {
        // Check if a GameObject named "ProTubeManager" already exists in the scene
        var existingManager = GameObject.Find("ProTubeManager");
        if (existingManager != null)
        {
            Debug.LogWarning("A GameObject named 'ProTubeManager' already exists in the scene.");
            Selection.activeGameObject = existingManager; // Select the existing GameObject
            return;
        }

        // Create a new GameObject named "ProTubeManager"
        var proTubeManager = new GameObject("ProTubeManager");

        // Add the ProTubeManager component to the GameObject
        proTubeManager.AddComponent<ProTubeManager>();

        // Select the newly created GameObject in the hierarchy
        Selection.activeGameObject = proTubeManager;

        // Log a message to confirm creation
        Debug.Log("ProTubeManager GameObject created and added to the scene.");
    }

    public override VisualElement CreateInspectorGUI()
    {
        channelTarget = channelTarget; // Set the channelTarget channel to all by default
                         // Create a new VisualElement to be the root of the inspector UI
        var root = new VisualElement();

        root.Add(Logo()); // Add the logo to the root
        root.Add(ConnectedDevices()); // Add the connected devices section to the root
        root.Add(Settings()); // Add the settings section to the root
        root.Add(TestFunctions()); // Add the test functions section to the ro

        return root;
    }

    void AutoShoot()
    {
        // Get the current time in seconds
        currentTime = EditorApplication.timeSinceStartup;

        // Calculate the time difference since the last shot
        double timeSinceLastShot = currentTime - lastShootTime;

        // Debug log to verify timing
        Debug.Log($"Current Time: {currentTime}, Last Shoot Time: {lastShootTime}, Time Since Last Shot: {timeSinceLastShot}, Interval: {autoShootInterval}");

        // Check if the interval has passed
        if (timeSinceLastShot >= autoShootInterval)
        {
            lastShootTime = currentTime; // Update the last shoot time
            ForceTubeVRInterface.Shoot(kickPower, rumblePower, rumbleDuration, channelTarget); // Perform the shoot action
            Debug.Log("AutoShoot Triggered"); // Log when the shoot action is triggered
        }
    }

    void OnDisable()
    {
        if (isAutoShooting)
        {
            isAutoShooting = false; // Stop auto-shooting
            autoShootBtn.text = "Auto Shoot"; // Reset the button text
            EditorApplication.update -= AutoShoot; // Unsubscribe from the update event
        }
    }
    VisualElement Settings()
    {


        var settingsContainer = new VisualElement
        {
            style = {
            flexDirection = FlexDirection.Column,
            marginTop = 10
        }
        };

        var SettingsLabel = new Label("Settings")
        {
            style = {
            fontSize = 18,
            unityFontStyleAndWeight = FontStyle.Bold,
            marginTop = 10,
            marginBottom = 5
        }
        };

        


        // Dropdown for selecting ProTubeSettings ScriptableObjects
        var settingsList = AssetDatabase.FindAssets("t:ProTubeSettings") // Find all ProTubeSettings assets
            .Select(guid => AssetDatabase.LoadAssetAtPath<ProTubeSettings>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(asset => asset != null)
            .ToList();

        settingsDropdown = new PopupField<ProTubeSettings>(
            "Profile",
            settingsList,
            0, // Default index
            settings => settings != null ? settings.name : "None", // Display name
            settings => settings != null ? settings.name : "None"  // Tooltip
        )
        {
            style =
    {
        flexGrow = 1 // Make the dropdown take the full available width
    }
        };

        var addFoldout = new VisualElement
        {
            style = {
        flexDirection = FlexDirection.Column,
        // marginTop = 10,
        marginBottom = 10,
        display = DisplayStyle.None // Initially hidden
    }
        };
    

        // Input field for the new settings name
        var nameInputField = new TextField("Name")
        {
            value = "ProTube Settings",
            style = {
        flexGrow = 1,
        marginBottom = 5
    }
        };


        // Button to save the new settings
        var saveNewButton = new Button(() =>
        {
            var newName = nameInputField.value;
            if (string.IsNullOrEmpty(newName))
            {
                Debug.LogWarning("Name cannot be empty.");
                return;
            }

            // Create a new ProTubeSettings asset
            var newSettings = ScriptableObject.CreateInstance<ProTubeSettings>();
            newSettings.name = newName; // Set the name property of the asset
            var path = $"Assets/ProTube/Profiles/{newName}.asset";
            AssetDatabase.CreateAsset(newSettings, path);
            AssetDatabase.SaveAssets();

            // Add the new settings to the dropdown list
            settingsList.Add(newSettings);
            settingsDropdown.choices = settingsList;

            Debug.Log($"Created new settings: {newName}");
            addFoldout.style.display = DisplayStyle.None; // Hide the container after saving
        })
        {
            text = "Create",
            style = {
        minWidth = 20,
        height = 20
    }
        };

        var nameAndButtonGroup = new VisualElement
        {
            style = {
        flexDirection = FlexDirection.Row,

        justifyContent = Justify.SpaceBetween,

    }
        };
     

        var pingButton = new Button(() =>
         {
             var selectedSettings = settingsDropdown.value;
             if (selectedSettings != null)
             {
                 //  ping the selected settings object in the project window
                 EditorUtility.FocusProjectWindow();
                 EditorGUIUtility.PingObject(selectedSettings); // Ping the selected settings object in the project window
             }


         })
        {
            text = "🔎",
            style = {
            minWidth = 20,
            height = 20 // Set a fixed height for the button
        }
        };
        var trashButton = new Button(() =>
        {
            var selectedSettings = settingsDropdown.value;
            if (selectedSettings != null)
            {
                //    delete the selected settings object in the project window
                var path = AssetDatabase.GetAssetPath(selectedSettings);
                if (EditorUtility.DisplayDialog("Delete Settings", $"Are you sure you want to delete {selectedSettings.name}?", "Delete", "Cancel"))
                {
                    AssetDatabase.DeleteAsset(path); // Delete the selected settings object
                    settingsList.Remove(selectedSettings); // Remove from the list
                                                           // settingsDropdown.Refresh(); // Refresh the dropdown
                    settingsContainer.Remove(settingsDropdown); // Remove the old dropdown

                    // Recreate the dropdown with the updated settings list
                    settingsDropdown = new PopupField<ProTubeSettings>(
                        "Settings",
                        settingsList,
                        0, // Default index
                        settings => settings != null ? settings.name : "None", // Display name
                        settings => settings != null ? settings.name : "None"  // Tooltip
                    )
                    {
                        style =
    {
        flexGrow = 1 // Make the dropdown take the full available width
    }
                    };

                    // Add the new dropdown to the container
                    settingsContainer.Insert(0, settingsDropdown);
                    Debug.Log($"Deleted {selectedSettings.name}.");

                }
            }
            else
            {
                Debug.LogWarning("No ProTubeSettings selected to delete.");
            }


        })
        {
            text = "🗑️",
            style = {
            minWidth = 20,
            height = 20 // Set a fixed height for the button
        }
        };

        var addButton = new Button(() =>
        {
            addFoldout.style.display =
              addFoldout.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;
        })
        {
            text = "➕",
            style = {
            minWidth = 20,
            height = 20 // Set a fixed height for the button
        }
        };

        // Button to save the current settings to the selected ScriptableObject
        saveButton = new Button(() =>
            {
                var selectedSettings = settingsDropdown.value;
                if (selectedSettings != null)
                {
                    selectedSettings.kickPower = kickPower;
                    selectedSettings.rumblePower = rumblePower;
                    selectedSettings.rumbleDuration = rumbleDuration;
                    selectedSettings.tempo = autoShootInterval; // Map autoShootInterval to tempo
                    EditorUtility.SetDirty(selectedSettings); // Mark the ScriptableObject as dirty to save changes
                    AssetDatabase.SaveAssets(); // Save the changes to the asset database
                    Debug.Log($"Settings saved to {selectedSettings.name}.");
                    UpdateSaveButtonState();
                }
                else
                {
                    Debug.LogWarning("No ProTubeSettings selected to save.");
                }

            })
        {
            text = "💾",
            style = {
            minWidth = 20,
            height = 20 // Set a fixed height for the button
        }
        };

        // Initially disable the button
        saveButton.SetEnabled(false);

        // Handle selection changes
        settingsDropdown.RegisterValueChangedCallback(evt =>
{
    var selectedSettings = evt.newValue;
    if (selectedSettings != null)
    {
        // Update the variables
        kickPower = selectedSettings.GetKickPower();
        rumblePower = selectedSettings.rumblePower;
        rumbleDuration = selectedSettings.rumbleDuration;
        autoShootInterval = selectedSettings.tempo;

        // Update the sliders and input fields
        kickPowerSliderWithField.IntValue = kickPower;
        kickPowerSliderWithField.IntInputField.value = kickPower;

        rumblePowerSliderWithField.IntValue = rumblePower;
        rumblePowerSliderWithField.IntInputField.value = rumblePower;

        rumbleDurationSliderWithField.FloatValue = rumbleDuration;
        rumbleDurationSliderWithField.FloatInputField.value = rumbleDuration;

        autoShootIntervalSliderWithField.FloatValue = autoShootInterval;
        autoShootIntervalSliderWithField.FloatInputField.value = autoShootInterval;

        // Enable the button if the current settings differ from the ScriptableObject
        saveButton.SetEnabled(
            kickPower != selectedSettings.kickPower ||
            rumblePower != selectedSettings.rumblePower ||
            rumbleDuration != selectedSettings.rumbleDuration ||
            autoShootInterval != selectedSettings.tempo
        );
    }
});

        // Update the button state when sliders change
        void UpdateSaveButtonState()
        {
            var selectedSettings = settingsDropdown.value;
            if (selectedSettings != null)
            {
                saveButton.SetEnabled(
                    kickPower != selectedSettings.kickPower ||
                    rumblePower != selectedSettings.rumblePower ||
                    rumbleDuration != selectedSettings.rumbleDuration ||
                    autoShootInterval != selectedSettings.tempo
                );
            }
        }

        // set the default values of the sliders to the selected settings
        if (settingsList.Count > 0)
        {
            var selectedSettings = settingsList[0]; // Get the first settings object as default
            kickPower = selectedSettings.GetKickPower(); // Get the kick power from the selected ScriptableObject
            rumblePower = selectedSettings.rumblePower; // Get the rumble power
            rumbleDuration = selectedSettings.rumbleDuration; // Get the rumble duration
            autoShootInterval = selectedSettings.tempo; // Map tempo to autoShootInterval
        }

        // Horizontal container for the dropdown and button
        var dropdownContainer = new VisualElement
        {
            style = {
            flexDirection = FlexDirection.Row,
            alignItems = Align.Center,
            marginBottom = 10
        }
        };

        // settinglabel and addButton next to it
        var settingsLabelContainer = new VisualElement
        {
            style = {
            flexDirection = FlexDirection.Row,
            alignItems = Align.Center,
            marginRight = 5,
            justifyContent = Justify.SpaceBetween
        }
        };

        settingsLabelContainer.Add(SettingsLabel); // Add the label to the container
        settingsLabelContainer.Add(addButton); // Add the add button to the container
        settingsContainer.Add(settingsLabelContainer); // Add the label container to the settings container

        
        settingsContainer.Add(addFoldout);


        nameAndButtonGroup.Add(nameInputField);


        nameAndButtonGroup.Add(saveNewButton);
        addFoldout.Add(nameAndButtonGroup); // Add the name and button group to the foldout container

        // checkbox for the property dontDestroyOnLoad
        var dontDestroyOnLoadCheckbox = new Toggle("Dont Destroy On Load")
        {
            value = true, // Set the default value to true
            style = {
                marginTop = 10,
                marginBottom = 10
            }
        };

        dontDestroyOnLoadCheckbox.RegisterValueChangedCallback(evt =>
        {
            // Update the dontDestroyOnLoad property based on the checkbox value
            var proTubeManager = (ProTubeManager)target;
            proTubeManager.dontDestroyOnLoad = evt.newValue;
            Debug.Log($"Dont Destroy On Load: {proTubeManager.dontDestroyOnLoad}");
        });
        settingsContainer.Add(dontDestroyOnLoadCheckbox); // Add the checkbox to the settings container


        dropdownContainer.Add(settingsDropdown);
        dropdownContainer.Add(pingButton); // Add the ping button to the container
        dropdownContainer.Add(saveButton);
        dropdownContainer.Add(trashButton); // Add the trash button to the container

        // Add the dropdown container to the settings container
        settingsContainer.Add(dropdownContainer);

        // Kick Power SliderWithField
        kickPowerSliderWithField = new SliderWithField("Kick Power", 0, 255, kickPower, 50);
        kickPowerSliderWithField.IntInputField.RegisterValueChangedCallback(evt =>
        {
            kickPower = (byte)Mathf.Clamp(evt.newValue, 0, 255);
            kickPowerSliderWithField.IntValue = kickPower; // Update the slider value
            UpdateSaveButtonState(); // Update the button state
        });
        kickPowerSliderWithField.Slider.RegisterValueChangedCallback(evt =>
        {
            kickPower = (byte)Mathf.Clamp(evt.newValue, 0, 255);
            kickPowerSliderWithField.IntInputField.value = kickPower; // Update the input field value
            UpdateSaveButtonState(); // Update the button state
        });
        settingsContainer.Add(kickPowerSliderWithField);

        // Rumble Power SliderWithField
        rumblePowerSliderWithField = new SliderWithField("Rumble Power", 0, 255, rumblePower, 50);
        rumblePowerSliderWithField.IntInputField.RegisterValueChangedCallback(evt =>
        {
            rumblePower = (byte)Mathf.Clamp(evt.newValue, 0, 255);
            rumblePowerSliderWithField.IntValue = rumblePower; // Update the slider value
            UpdateSaveButtonState(); // Update the button state
        });
        rumblePowerSliderWithField.Slider.RegisterValueChangedCallback(evt =>
        {
            rumblePower = (byte)Mathf.Clamp(evt.newValue, 0, 255);
            rumblePowerSliderWithField.IntInputField.value = rumblePower; // Update the input field value
            UpdateSaveButtonState(); // Update the button state
        });
        settingsContainer.Add(rumblePowerSliderWithField);

        // Rumble Duration SliderWithField
        rumbleDurationSliderWithField = new SliderWithField("Rumble Duration", 0.01f, 1f, rumbleDuration, 50);
        rumbleDurationSliderWithField.FloatInputField.RegisterValueChangedCallback(evt =>
        {
            rumbleDuration = Mathf.Clamp(evt.newValue, 0.01f, 1f);
            rumbleDurationSliderWithField.FloatValue = rumbleDuration; // Update the slider value
            UpdateSaveButtonState(); // Update the button state
        });
        rumbleDurationSliderWithField.Slider.RegisterValueChangedCallback(evt =>
        {
            rumbleDuration = Mathf.Clamp(evt.newValue, 0.01f, 1f);
            rumbleDurationSliderWithField.FloatInputField.value = rumbleDuration; // Update the input field value
            UpdateSaveButtonState(); // Update the button state
        });
        settingsContainer.Add(rumbleDurationSliderWithField);

        

        return settingsContainer;
    }

    VisualElement TestFunctions()
    {
        var testFunctionsContainer = new VisualElement
        {
            style = {
                    flexDirection = FlexDirection.Column,
                    marginTop = 10
                }
        };
        var debugLabel = new Label("Test functions")
        {
            style = {
                    fontSize = 18,
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginBottom = 5
                }
        };
        testFunctionsContainer.Add(debugLabel);

        var channelDropdown = new EnumField("Select Channel", ForceTubeVRChannel.all)
        {
            style = {
                    minWidth = 150
                }
        };

        // Update the channelTarget channel when the dropdown value changes
        channelDropdown.RegisterValueChangedCallback(evt =>
        {
            channelTarget = (ForceTubeVRChannel)evt.newValue;
            Debug.Log($"Selected Channel: {channelTarget}");
        });

        // Add the dropdown to the root element
        testFunctionsContainer.Add(channelDropdown);

        // Replace the existing slider and field for "Shoot Interval" with SliderWithField
        var shotSpeedSliderWithField = new SliderWithField("Shoot Interval", 0.1f, 1f, autoShootInterval, 50);

        // Synchronize the value of the custom slider with the `autoShootInterval` variable
        shotSpeedSliderWithField.FloatInputField.RegisterValueChangedCallback(evt =>
        {
            autoShootInterval = Mathf.Clamp(evt.newValue, 0.1f, 1f);
            shotSpeedSliderWithField.FloatValue = autoShootInterval; // Update the slider value
        });
        shotSpeedSliderWithField.Slider.RegisterValueChangedCallback(evt =>
        {
            autoShootInterval = evt.newValue;
            shotSpeedSliderWithField.FloatInputField.value = autoShootInterval; // Update the input field value
        });

        // Add the custom slider to the root container
        testFunctionsContainer.Add(shotSpeedSliderWithField);


        var shootBtn = new Button(() => ForceTubeVRInterface.Shoot(kickPower, rumblePower, rumbleDuration, channelTarget))
        {
            text = "Shoot",
            style = {
                    marginTop = 10,
                    flexGrow = 1, // Allow the button to grow
                    flexBasis = 0, // Allow the button to shrink
                    marginLeft = 5,
                    marginRight = 5,
                    minWidth = 100, // Set a minimum width for the button
                    height = 40 // Set a fixed height for the button
                }
        };

        var kickBtn = new Button(() => ForceTubeVRInterface.Kick(kickPower, channelTarget))
        {
            text = "Kick",
            style = {
                    marginTop = 10,
                    flexGrow = 1,
                    flexBasis = 0,
                    marginLeft = 5,
                    marginRight = 5,
                    minWidth = 100,
                    height = 40 // Set a fixed height for the button
                }
        };

        var rumbleBtn = new Button(() => ForceTubeVRInterface.Rumble(rumblePower, rumbleDuration, channelTarget))
        {
            text = "Rumble",
            style = {
                    marginTop = 10,
                    flexGrow = 1,
                    flexBasis = 0,
                    marginLeft = 5,
                    marginRight = 5,
                    minWidth = 100,
                    height = 40 // Set a fixed height for the button
                }
        };

        autoShootBtn = new Button(() =>
        {
            isAutoShooting = !isAutoShooting; // Toggle the auto-shoot state
            autoShootBtn.text = isAutoShooting ? "Stop Auto" : "Auto Shoot"; // Update button text

            if (isAutoShooting)
            {
                EditorApplication.update += AutoShoot; // Start auto-shooting
            }
            else
            {
                EditorApplication.update -= AutoShoot; // Stop auto-shooting
            }
        })
        {
            text = "Auto Shoot",
            style = {
                    marginTop = 10,
                    flexGrow = 1,
                    flexBasis = 0,
                    marginLeft = 5,
                    marginRight = 5,
                    minWidth = 100,
                    height = 40 // Set a fixed height for the button
                }
        };

        // Add the Auto Shoot button to the grid

        // Adjust the grid layout to ensure proper spacing and alignment
        var buttonGrid = new VisualElement
        {
            style = {
                    flexDirection = FlexDirection.Row,
                    flexWrap = Wrap.Wrap,
                    justifyContent = Justify.SpaceAround,
                }
        };

        buttonGrid.Add(shootBtn);
        buttonGrid.Add(kickBtn);
        buttonGrid.Add(rumbleBtn);
        buttonGrid.Add(autoShootBtn);
        testFunctionsContainer.Add(buttonGrid);

        // Add your test functions UI elements here

        return testFunctionsContainer;
    }

    VisualElement ConnectedDevices()
    {
        var connectedDevicesContainer = new VisualElement
        {
            style = {
                    flexDirection = FlexDirection.Column,
                }
        };

        // Create a container for the connected devices list
        var devicesContainer = new VisualElement();
        devicesContainer.style.flexDirection = FlexDirection.Column;
        devicesContainer.style.marginTop = 10;





        // label: Connected
        var connectedLabel = new Label("Connected Devices")
        {
            style = {
                    fontSize = 18,
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginBottom = 5
                }
        };
        // Add a button to scan connected devices
        var scanButton = new Button(() =>
        {
            string updatedJson = ForceTubeVRInterface.ListChannels(); // Get updated JSON
            PopulateDevicesList(updatedJson); // Repopulate the devices list

            // remove the previous list of connected devices
            // devicesContainer.Clear(); // Clear the container before repopulating
            // remove the protube from its channel
            // ForceTubeVRInterface.ClearChannel(); // Clear the channel


            Debug.Log(ForceTubeVRInterface.ListConnectedForceTube()); // Log the connected devices
        })
        {
            text = "↻"
        };

        // Style the button to float in the top-right corner
        // scanButton.style.position = Position.Absolute;
        // scanButton.style.top = 120;
        // scanButton.style.right = 5;
        scanButton.style.width = 20;
        scanButton.style.height = 20;
        scanButton.style.unityTextAlign = TextAnchor.MiddleCenter;

        var buttonGroup = new VisualElement
        {
            style = {
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceBetween,
                    marginBottom = 5
                }
        };

        buttonGroup.Add(scanButton); // Add the scan button to the button group
        var labelContainer = new VisualElement
        {
            style = {
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceBetween, // Corrected from Align.SpaceBetween
                    marginTop = 15,
                    marginBottom = 5
                }
        };
        labelContainer.Add(connectedLabel);
        labelContainer.Add(buttonGroup); // Add the scan button to the label container


        // Parse the JSON data


        // Method to populate the connected devices list
        void PopulateDevicesList(string json)
        {
            devicesContainer.Clear(); // Clear the container before repopulating

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("JSON data is null or empty.");
                return;
            }

            Debug.Log($"JSON Data: {json}"); // Log the JSON data for debugging

            JObject parsedJson;
            try
            {
                parsedJson = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to parse JSON: {ex.Message}");
                return;
            }

            if (parsedJson["channels"] == null)
            {
                Debug.LogWarning("No channels found in the JSON data.");
                return;
            }



            // Add a label for each connected device
            foreach (var channel in parsedJson["channels"])
            {
                string channelName = channel.Path; // Get the channel name (e.g., "rifleButt")
                var devicesInChannel = channel.First()
                    .OfType<JObject>()
                    .Select(device => new
                    {
                        Name = device["name"]?.ToString(),
                        BatteryLevel = device["batteryLevel"]?.ToString()
                    })
                    .Where(device => device.Name != null && device.BatteryLevel != null);

                foreach (var device in devicesInChannel)
                {
                    // Create a horizontal container for the device info
                    var deviceContainer = new VisualElement();
                    deviceContainer.style.flexDirection = FlexDirection.Row;
                    deviceContainer.style.alignItems = Align.Center;
                    deviceContainer.style.marginTop = 5;

                    // Determine the color based on the battery level
                    Color batteryColor;
                    int batteryLevel = int.Parse(device.BatteryLevel);
                    if (batteryLevel < 30)
                        batteryColor = Color.red;
                    else if (batteryLevel < 50)
                        batteryColor = Color.yellow;
                    else
                        batteryColor = Color.green;

                    // Create a small circle to represent the battery level
                    var batteryIndicator = new VisualElement();
                    batteryIndicator.style.width = 10;
                    batteryIndicator.style.height = 10;
                    batteryIndicator.style.backgroundColor = batteryColor;
                    batteryIndicator.style.borderTopLeftRadius = 50;
                    batteryIndicator.style.borderTopRightRadius = 50;
                    batteryIndicator.style.borderBottomLeftRadius = 50;
                    batteryIndicator.style.borderBottomRightRadius = 50;
                    batteryIndicator.style.marginRight = 5;

                    // Add the battery indicator to the container
                    deviceContainer.Add(batteryIndicator);

                    // Create a label for the battery percentage and device name
                    var deviceLabel = new Label($"{device.BatteryLevel}% - {device.Name}");
                    deviceLabel.style.unityFontStyleAndWeight = FontStyle.Bold;

                    // Add the label to the container
                    deviceContainer.Add(deviceLabel);

                    // Create a dropdown for the channel name
                    var channelDropdown = new PopupField<string>(
                        "",
                        parsedJson["channels"].Select(channel => channel.Path).ToList(), // Populate with channel names
                        channelName // Set the current channel as the selected option
                    )
                    {
                        style = {
        marginLeft = 5
    }
                    };

                    // Handle value changes in the dropdown
                    channelDropdown.RegisterValueChangedCallback(evt =>
                    {
                        // item 1 is 0, item 2 is 1, etc.
                        int fromSelectedIndex = parsedJson["channels"].Select(channel => channel.Path).ToList().IndexOf(channelName);
                        int toSelectedIndex = parsedJson["channels"].Select(channel => channel.Path).ToList().IndexOf(evt.newValue);

                        ForceTubeVRInterface.SwitchChannel(fromSelectedIndex, toSelectedIndex, device.Name); // Move the channel to the new position

                        // Get the selected channel name
                        

                    });
                    // Add the channel dropdown to the container
                    deviceContainer.Add(channelDropdown);

                    // Add the container to the devices container
                    devicesContainer.Add(deviceContainer);
                }
            }
        }

        // Parse the initial JSON data and populate the devices list
        string initialJson = ForceTubeVRInterface.ListChannels();
        PopulateDevicesList(initialJson);

        connectedDevicesContainer.Add(labelContainer); // Add the label container to the main container
        connectedDevicesContainer.Add(devicesContainer);


        return connectedDevicesContainer;
    }

    VisualElement Logo()
    {
        var logoContainer = new VisualElement
        {
            style = {
                    flexDirection = FlexDirection.Column,
                }
        };

        // Get the relative path to the logo
        var scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
        var directoryPath = System.IO.Path.GetDirectoryName(scriptPath);
        var logoPath = System.IO.Path.Combine(directoryPath, "Logo/PT_Logo.png").Replace("\\", "/");

        // Load the logo as a sprite
        var logoSprite = AssetDatabase.LoadAssetAtPath<Sprite>(logoPath);
        if (logoSprite != null)
        {
            var logoImage = new Image
            {
                image = logoSprite.texture,
                scaleMode = ScaleMode.ScaleToFit
            };
            logoImage.style.height = 100; // Set the height of the logo
            logoContainer.Add(logoImage);
        }
        else
        {
            Debug.LogWarning($"Logo not found at path: {logoPath}");
        }

        return logoContainer;
    }
}
