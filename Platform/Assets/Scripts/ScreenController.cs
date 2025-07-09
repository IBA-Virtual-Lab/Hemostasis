using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Script is attached to Machine1 (STart4)

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    public Material physicalScreenMaterialOn;
    [SerializeField]
    private Material physicalScreenMaterialOff;

    // The actual screen in world space. Used to change material on it.
    // It has its own script calling interact events.
    public GameObject physicalScreen;
    
    private int patientId1;
    private int patientId2;
    private int patientId3;

    private GameObject displayText;
    private GameObject selectedOption;
    // Inputs for the patient ID
    private GameObject inputText1;
        private GameObject inputText2;
            private GameObject inputText3;
    private TMP_Text inputText1Text;
        private TMP_Text inputText2Text;
            private TMP_Text inputText3Text;
    private GameObject screenHeader;
    private TMP_Text screenHeaderText;
    private GameObject enterCodeNumber;
    private string menuAddress;
    // Make the variables above read-only. So other scripts can read them for reference and for state checks.
    public string getMenuAddress
    {
        get { return menuAddress; }
    }

    public GameObject screenTile; // The green UI Background for the screen
    private TMP_Text Display_Text; // A gameobject under UI/Display & Progress Tracker
    private TextMeshProUGUI selectedOptionText;
    
    private CPLENTER enter;
    public int CPLN; // Modified by the CPLNX Script. When a button is pressed this variable is updated.
    public int patientID1;
    public int patientID2;
    public int patientID3;
    public bool inputMode = false; // When true, the next CPLN pressed will be used to enter a number instead of navigating the menu

    void Start()
    {
        menuAddress = "/";
        Display_Text = GameObject.Find("Display_Text").GetComponent<TMP_Text>();
        enter = GameObject.Find("ENTER").GetComponent<CPLENTER>();
        physicalScreen = GameObject.Find("SCREEN");
        displayText = GameObject.Find("Display_Text");
        inputText1 = GameObject.Find("Input_Text1");
        inputText2 = GameObject.Find("Input_Text2");
        inputText3 = GameObject.Find("Input_Text3");
        inputText1Text = inputText1.GetComponent<TextMeshProUGUI>();
        inputText2Text = inputText2.GetComponent<TextMeshProUGUI>();
        inputText3Text = inputText3.GetComponent<TextMeshProUGUI>();
        screenHeader = GameObject.Find("Screen Header");
        selectedOption = GameObject.Find("selectedOption");
        enterCodeNumber = GameObject.Find("Enter Code Number");
        screenTile = GameObject.Find("Screen Tile");
        screenTile.SetActive(false);
        selectedOptionText = selectedOption.GetComponent<TextMeshProUGUI>();
        patientID1 = 0;
        patientID2 = 0;
        patientID3 = 0;
    }

    private void updateMenuOptions (string text){
        // Updates the text of the TextMeshPro object
        Display_Text.text = text;
    }

// Decide which menu to display based on the menu address
    public void refreshMenu(string newMenuAdress){
        string text = "";
        menuAddress = newMenuAdress;
        enterCodeNumber.SetActive(true);
        if(newMenuAdress == "/"){
            text = @"1: Test Mode 	    2: Calibration
3: Test Parameters   4: System Check";
        }else if(newMenuAdress == "/Test Mode"){
            text = @"1: PT            		  2: APTT
3: Fibrinogen   		  4: Factors
5: Heparin 		 	  6: Others";
        }else if(newMenuAdress == "/Test Mode/Others"){
            text= @"1: Inhibitors        2: TCT        3: Lupus
4: SPA   	       5: Others";
        }else if(menuAddress == "/Test Mode/Others/SPA/Running Test"){
            text = "Running SPA Test";   
        }else if(menuAddress == "/Test Mode/PT/Running Test"){
            // disable 'Enter code number' text
            enterCodeNumber.SetActive(false);
            // Disable patient ID ui
            inputText1Text.text = "";
            inputText2Text.text = "";
            inputText3Text.text = "";
            text = "Running PT Test";
        }else if(newMenuAdress == "/Test Mode/Others/SPA" || newMenuAdress == "/Test Mode/PT"){
            enterCodeNumber.SetActive(false);
            if(patientID1 == 0){
                text = @"Pat   ID#    1: ";
            }else if(patientID2 == 0){
                text = @"Pat   ID#    1: 
Pat   ID#    2:
                ";
            }else if(patientID3 == 0){
                text = @"Pat   ID#    1:
Pat   ID#    2:
Pat   ID#    3:
                ";
            }
        }
        else{
            text = "404 Route not found";
        }
        updateMenuOptions(text);
    }
    // A function to update CPLN
    public void updateCPLN(int newCPLN){
        CPLN = newCPLN;
    }
    // A function to get the next menu address based on CPLN pressed so selectedOptionText.text can be updated
    void updateMenuAddress() {
        string text = getRoute();
        string newMenuAdress = menuAddress + "/"+text;
        menuAddress = newMenuAdress;
    }

    // Called after CPLN is pressed from CPLNX script.
    // The getRoute uses the CPLN value to return the selected option
    public void updateSelectedOption (bool clear) {
        if(clear){
            selectedOptionText.text = "";
        }else if(menuAddress == "/Test Mode/Others/SPA" || menuAddress == "/Test Mode/PT"){
            Debug.Log("CPLN entered: " + CPLN);
            if(CPLN != 0){
                // If a patient ID hasn't been set yet, you can change the selected ID
                if(patientID1 == 0){
                    inputText1Text.text = CPLN.ToString();
                    patientId1 = CPLN; // Stored patient ID
                }else if(patientID2 == 0){
                    inputText2Text.text = CPLN.ToString();
                    patientId2 = CPLN; // Stored patient ID
                }else if(patientID3 == 0){
                    inputText3Text.text = CPLN.ToString();
                    patientId3 = CPLN; // Stored patient ID
                }
            }
            else{
                string text = getRoute();
                selectedOptionText.text = text;
            }
        }else{
            Debug.Log("CPLN entered: " + CPLN);
            string text = getRoute();
            selectedOptionText.text = text;
        }
    } 

    // A function to get the next menu address based on which CPLN is selected (when enter is pressed)
    public string getRoute (){
        // By default the Enter Code Number text is active
        enterCodeNumber.SetActive(true);
        if(menuAddress == "/"){
            return CPLN switch
            {
                1 => "Test Mode",
                2 => "Calibration",
                3 => "Test Parameters",
                4 => "System Check",
                _ => "404 Route not found",
            };
        }
        else if(menuAddress == "/Test Mode"){
            return CPLN switch
            {
                1 => "PT",
                2 => "APTT",
                3 => "Fibrinogen",
                4 => "Factors",
                5 => "Heparin",
                6 => "Others",
                _ => "404 Route not found",
            };
        }
        else if(menuAddress == "/Test Mode/Others"){
            return CPLN switch
            {
                1 => "Inhibitors",
                2 => "TCT",
                3 => "Lupus",
                4 => "SPA",
                5 => "Others",
                _ => "404 Route not found",
            };
        }
        else if(menuAddress == "/Test Mode/PT" || menuAddress == "/Test Mode/Others/SPA"){
            return CPLN switch
            {
                0 => "Running Test",
                _ => "",// If something else than 0 is entered, that means a patient ID was entered, and the route should not be changed.
            };
        }
        else if(menuAddress == "/Test Mode/PT/Running Test/Finished"){
            return "INR Test has finished.";
        }
        return "404 Route not found";
    }

// Called when enter is pressed
    public void enterWasPressed () {
        if((menuAddress == "/Test Mode/PT" || menuAddress == "/Test Mode/Others/SPA") && CPLN != 0){
            // If the user is in a test setup, the number is used to enter patient ID.
            // This code changes the saved patientID when enter is pressed.
            if(patientID1 == 0){
                patientID1 = CPLN;
                refreshMenu(menuAddress);
            }else if(patientID2 == 0){
                patientID2 = CPLN;
                refreshMenu(menuAddress);
            }else if(patientID3 == 0){
                patientID3 = CPLN;
                refreshMenu(menuAddress);
            }
            updateCPLN(0);
        }else if(menuAddress == "/Test Mode/PT/Running Test" || menuAddress == "/Test Mode/Others/SPA/Running Test"){
            // Case for when pressing enter, but the test is still running.
            // Wait for the test to complete. This will auto-navigate. (not implemented yet).
        }
        else{
            // If not in patient-id mode or if 0(no number) was entered patient-id mode: append the new route and refresh the menu
            string newRoute;
            if(menuAddress == "/"){
                newRoute = menuAddress + getRoute();
            }else{
                newRoute = menuAddress + "/" + getRoute();
            }
            Debug.Log(newRoute);
            refreshMenu(newRoute);
            updateCPLN(0);
        }
        updateSelectedOption(true); // Clears the selected option
    }

    public void screenOff() {
        //physicalScreen.SetActive(false);
        physicalScreen.GetComponent<MeshRenderer>().material = physicalScreenMaterialOff;
        displayText.SetActive(false);
        selectedOption.SetActive(false);
        inputText1.SetActive(false);
        inputText2.SetActive(false);
        inputText3.SetActive(false);
        screenHeader.SetActive(false);
        enterCodeNumber.SetActive(false);
        screenTile.SetActive(false);
    }
    public void screenOn() {
        physicalScreen.GetComponent<MeshRenderer>().material = physicalScreenMaterialOn;
        displayText.SetActive(true);
        selectedOption.SetActive(true);
        inputText1.SetActive(true);
        inputText2.SetActive(true);
        inputText3.SetActive(true);
        screenHeader.SetActive(true);
        enterCodeNumber.SetActive(true);
        screenTile.SetActive(true);
        refreshMenu("/");
        patientID1 = 0;
        patientID2 = 0;
        patientID3 = 0;
        // reset stored patient id's (I know the names are dumb, change them if u want)
        patientId1 = 0;
        patientId2 = 0;
        patientId3 = 0;
    }
}