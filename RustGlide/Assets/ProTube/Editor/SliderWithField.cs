using System; // Include this for Convert
using UnityEngine;
using UnityEngine.UIElements;

public class SliderWithField : VisualElement
{
    public Slider Slider { get; private set; }
    public BaseField<float> FloatInputField { get; private set; }
    public BaseField<int> IntInputField { get; private set; }
    public float FloatValue
    {
        get => Slider.value; // Slider value is already a float
        set
        {
            Slider.value = value;
            if (FloatInputField != null)
            {
                FloatInputField.value = value;
            }
        }
    }

    public int IntValue
    {
        get => Mathf.RoundToInt(Slider.value); // Convert slider value to int
        set
        {
            Slider.value = value;
            if (IntInputField != null)
            {
                IntInputField.value = value;
            }
        }
    }

    public SliderWithField(string label, float minValue, float maxValue, float initialValue, float inputFieldWidth = 50)
    {
        CreateFloatSliderWithField(label, minValue, maxValue, initialValue, inputFieldWidth);
    }

    public SliderWithField(string label, int minValue, int maxValue, int initialValue, float inputFieldWidth = 50)
    {
        CreateIntSliderWithField(label, minValue, maxValue, initialValue, inputFieldWidth);
    }

    private void CreateFloatSliderWithField(string label, float minValue, float maxValue, float initialValue, float inputFieldWidth)
    {
        // Create the slider
        Slider = new Slider(label, minValue, maxValue)
        {
            value = initialValue,
            style =
            {
                flexGrow = 1, // Allow the slider to fill available width
                marginBottom = 10
            }
        };

        // Create the input field
        FloatInputField = new FloatField
        {
            value = initialValue,
            style =
            {
                marginLeft = 5,
                width = inputFieldWidth // Fixed width for the input field
            }
        };

        // Synchronize slider and input field
        Slider.RegisterValueChangedCallback(evt =>
        {
            FloatInputField.value = evt.newValue;
        });
        FloatInputField.RegisterValueChangedCallback(evt =>
        {
            Slider.value = evt.newValue;
        });

        AddToContainer();
    }

    private void CreateIntSliderWithField(string label, int minValue, int maxValue, int initialValue, float inputFieldWidth)
    {
        // Create the slider
        Slider = new Slider(label, minValue, maxValue)
        {
            value = initialValue,
            style =
            {
                flexGrow = 1, // Allow the slider to fill available width
                marginBottom = 10
            }
        };

        // Create the input field
        IntInputField = new IntegerField
        {
            value = initialValue,
            style =
            {
                marginLeft = 5,
                width = inputFieldWidth // Fixed width for the input field
            }
        };

        // Synchronize slider and input field
        Slider.RegisterValueChangedCallback(evt =>
        {
            IntInputField.value = Mathf.RoundToInt(evt.newValue);
        });
        IntInputField.RegisterValueChangedCallback(evt =>
        {
            Slider.value = evt.newValue;
        });

        AddToContainer();
    }

    private void AddToContainer()
    {
        // Create a container for the slider and input field
        var container = new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center
            }
        };

        container.Add(Slider);
        if (FloatInputField != null)
        {
            container.Add(FloatInputField);
        }
        else if (IntInputField != null)
        {
            container.Add(IntInputField);
        }

        // Add the container to the custom field
        Add(container);
    }
}