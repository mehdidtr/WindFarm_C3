using System.Collections;
using UnityEngine;
using Michsky.MUIP; // Michsky.MUIP namespace
using TMPro; // For TextMeshPro

public class RPM : MonoBehaviour
{
    [SerializeField] private CustomDropdown timeDropdown; // Reference to your Michsky CustomDropdown
    [SerializeField] private TMP_Text rpmText; // Text placeholder for countdown
    [SerializeField] private TurbineDataContainer turbineDataContainer; // Reference to your TurbineDataContainer

    private int currentIndex; // Track the current dropdown index

    private float currentRPM = 0;

    void Start()
    {
        // Ensure the dropdown value change listener is added
        if (timeDropdown != null)
        {
            currentIndex = timeDropdown.selectedItemIndex;
            timeDropdown.onValueChanged.AddListener(OnDropdownValueChanged); // Add listener to dropdown value change
            for (int i = 0; i < turbineDataContainer.turbines.Length; i++)
            {
                currentRPM += turbineDataContainer.turbines[i].rotorSpeeds[currentIndex];
            }
            currentRPM /= turbineDataContainer.turbines.Length; // Calculate the average RPM
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the turbineDataContainer is valid and the currentIndex is within bounds
        if (turbineDataContainer != null)
        {

            // Update the display with the current RPM
            rpmText.text = $"{currentRPM} rpm"; // Format RPM text
        }
        else
        {
            rpmText.text = "No data available";
        }
    }

    // Method to handle dropdown value change
    private void OnDropdownValueChanged(int selectedIndex)
    {
        currentIndex = selectedIndex; // Update the current index when the dropdown value changes
        currentRPM = 0;
        for (int i = 0; i < turbineDataContainer.turbines.Length; i++)
        {
            currentRPM += turbineDataContainer.turbines[i].rotorSpeeds[currentIndex];
        }
        currentRPM /= turbineDataContainer.turbines.Length; // Calculate the average RPM
        //Debug.Log($"Selected dropdown index: {currentIndex}");
    }
}
