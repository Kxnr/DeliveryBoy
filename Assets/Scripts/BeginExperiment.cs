using UnityEngine;
using UnityEngine.SceneManagement;
using Luminosity.IO;
using System.Linq;

public class BeginExperiment : MonoBehaviour
{
    public UnityEngine.GameObject greyedOutButton;
    public UnityEngine.GameObject beginExperimentButton;
    public UnityEngine.GameObject loadingButton;
    public UnityEngine.GameObject finishedButton;
    public UnityEngine.GameObject languageMismatchButton;
    public UnityEngine.UI.InputField participantCodeInput;
    public UnityEngine.UI.Toggle useRamulatorToggle;
    public UnityEngine.UI.Toggle useNiclsToggle;
    public UnityEngine.UI.Toggle useSameTownLayout;
    public UnityEngine.UI.Text beginButtonText;
    public UnityEngine.UI.InputField sessionInput;

    // LC: Add UseElemem toggle
    public UnityEngine.UI.Toggle useElememToggle;

    // TODO: JPB: Make these configuration variables
    private const bool EFR_COURIER = false;
    private const bool NICLS_COURIER = true;
    private const bool VALUE_COURIER = false;

    private const string scene_name = "MainGame";

    public const string EXP_NAME_COURIER = "Courier";
    public const string EXP_NAME_EFR = "EFRCourier";
    // Changed from NiclsCourier to CourierReinstate1 to appease Rhino
    public const string EXP_NAME_NICLS = "CourierReinstate1";
    public const string EXP_NAME_VALUE = "ValueCourier";

    private void OnEnable() 
    {
        // #if UNITY_WEBGL
        //     SceneManager.LoadScene(scene_name);
        // #endif // UNITY_WEBGL

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (DeliveryItems.ItemsExhausted())
        {
            beginExperimentButton.SetActive(false);
            finishedButton.SetActive(true);
        }
        else
        {
            finishedButton.SetActive(false);
        }
        if (LanguageMismatch())
        {
            beginExperimentButton.SetActive(false);
            languageMismatchButton.SetActive(true);
        }
        else
        {
            languageMismatchButton.SetActive(false);
        }
    }

    public void UpdateParticipant() 
    {
        if (IsValidParticipantName(participantCodeInput.text))
        {
            UnityEPL.ClearParticipants();
            beginExperimentButton.SetActive(true);
            greyedOutButton.SetActive(false);
            // LC: commented out, because this creates useless directory
            // int nextSessionNumber = NextSessionNumber();
            int nextSessionNumber = 0;
            sessionInput.text = nextSessionNumber.ToString();
            beginButtonText.text = LanguageSource.GetLanguageString("begin session") + " " + nextSessionNumber.ToString();
        }
        else
        {
            greyedOutButton.SetActive(true);
            beginExperimentButton.SetActive(false);
        }
    }

    public void UpdateSession() 
    {
        int session;
         
        if(System.Int32.TryParse(sessionInput.text, out session)) 
        {
            beginButtonText.text = LanguageSource.GetLanguageString("begin session") + " " + session.ToString();
            UnityEPL.SetSessionNumber(session);
            beginExperimentButton.SetActive(true);
            greyedOutButton.SetActive(false);
        }
        else {
            greyedOutButton.SetActive(true);
            beginExperimentButton.SetActive(false);
        }
    }

    private string GetLanguageFilePath()
    {
        string dataPath = UnityEPL.GetParticipantFolder();
        System.IO.Directory.CreateDirectory(dataPath);
        string languageFilePath = System.IO.Path.Combine(dataPath, "language");
        if (!System.IO.File.Exists(languageFilePath))
            System.IO.File.Create(languageFilePath).Close();
        return languageFilePath;
    }

    private bool LanguageMismatch()
    {
        if (UnityEPL.GetParticipants()[0].Equals("unspecified_participant"))
            return false;
        if (System.IO.File.ReadAllText(GetLanguageFilePath()).Equals(""))
            return false;
        return !LanguageSource.current_language.ToString().Equals(System.IO.File.ReadAllText(GetLanguageFilePath()));
    }

    private void LockLanguage()
    {
        System.IO.File.WriteAllText(GetLanguageFilePath(), LanguageSource.current_language.ToString());
    }

    public void DoBeginExperiment()
    {   
        #if !UNITY_WEBGL
            if (!IsValidParticipantName(participantCodeInput.text)) {
                loadingButton.SetActive(false);
                greyedOutButton.SetActive(true);
                beginExperimentButton.SetActive(false);

                throw new UnityException("You are trying to start the experiment with an invalid participant name!");
            }

            // LC: reset the participant code and experiment name for retry
            UnityEPL.ClearParticipants();
            UnityEPL.AddParticipant(participantCodeInput.text);

            string experiment_name = EFR_COURIER ? EXP_NAME_EFR :
                                NICLS_COURIER ? EXP_NAME_NICLS :
                                VALUE_COURIER ? EXP_NAME_VALUE :
                                EXP_NAME_COURIER;

            if (experiment_name == EXP_NAME_NICLS && useNiclsToggle.isOn)
                experiment_name += "ClosedLoop";
            else if (experiment_name == EXP_NAME_EFR && useElememToggle.isOn)
                experiment_name += "OpenLoop";
            else
                //experiment_name += "ReadOnly";
                //experiment_name += "_v2.0";
                //experiment_name += "Reinstate1";
                

            UnityEPL.SetExperimentName(experiment_name);

            // LC: check for any existing sessions
            string dataPath = UnityEPL.GetParticipantFolder();
            Debug.Log(dataPath);
            System.IO.Directory.CreateDirectory(dataPath);

            bool sessionExists = false;
            bool isFirstSession = false;
            int currentSessionNumber = System.Int32.Parse(sessionInput.text);
            string[] existingSessionFolders = System.IO.Directory.GetDirectories(dataPath);

            foreach (string folder in existingSessionFolders)
            {
                if (folder.Substring(folder.LastIndexOf("_")+1).Equals(currentSessionNumber.ToString()))
                    sessionExists = true;
                    // Debug.Log("Session Exists. Re-enter session number");
            }

            if (!sessionExists)
            {
                LockLanguage();
                // LC: if current session is 0, check whether this is the actual "first" session
                if (currentSessionNumber == 0)
                {
                    // Debug.Log("Session number is 0");
                    string defaultRoot = "";
                    //if (Application.isEditor)
                    //    defaultRoot = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                    //else
                    //    defaultRoot = System.IO.Path.GetFullPath(".");
                    //defaultRoot = System.IO.Path.Combine(defaultRoot, "data");
                    defaultRoot = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                    defaultRoot = System.IO.Path.Combine(defaultRoot, "UnityTasks");
                    defaultRoot = System.IO.Path.Combine(defaultRoot, "data");

                    string[] folders = System.IO.Directory.GetDirectories(defaultRoot);

                    // LC: check if there is another session 0 folder
                    string otherExperimentName = useElememToggle.isOn ? "EFRCourierReadOnly" : "EFRCourierOpenLoop";
                    string otherDirectory = System.IO.Path.Combine(defaultRoot, otherExperimentName);
                    otherDirectory = System.IO.Path.Combine(otherDirectory, string.Join("", UnityEPL.GetParticipants()));
                    otherDirectory = System.IO.Path.Combine(otherDirectory, "session_" + currentSessionNumber.ToString());
                    // Debug.Log("OTHER: " + otherDirectory);

                    if (System.IO.Directory.Exists(otherDirectory))
                        isFirstSession = false;
                    else
                        isFirstSession = true;
                }

                // TODO: JPB: Use NextSessionNumber()
                DeliveryExperiment.ConfigureExperiment(useRamulatorToggle.isOn, useNiclsToggle.isOn, useElememToggle.isOn,
                                                    UnityEPL.GetSessionNumber(), experiment_name, isFirstSession, useSameTownLayout.isOn);
                Debug.Log("Ram On: " + useRamulatorToggle.isOn);
                Debug.Log("Nicls On: " + useNiclsToggle.isOn);
                Debug.Log("Elemem On: " + useElememToggle.isOn);
                Debug.Log("First session: " + isFirstSession.ToString());
                SceneManager.LoadScene(scene_name);
            }
            else
            {
                loadingButton.SetActive(false);
                greyedOutButton.SetActive(true);
                beginExperimentButton.SetActive(false);
            }

        #else
            UnityEPL.ClearParticipants();
            UnityEPL.AddParticipant(participantCodeInput.text);

            string experiment_name = EFR_COURIER ? EXP_NAME_EFR :
                                     NICLS_COURIER ? EXP_NAME_NICLS :
                                     VALUE_COURIER ? EXP_NAME_VALUE :
                                     EXP_NAME_COURIER;

            if (experiment_name == EXP_NAME_NICLS && useNiclsToggle.isOn)
                experiment_name += "ClosedLoop";
            else if (experiment_name == EXP_NAME_EFR && useElememToggle.isOn)
                experiment_name += "OpenLoop";
            else
                experiment_name += "ReadOnly";

            UnityEPL.SetExperimentName(experiment_name);
            int sessionNumber = System.Int32.Parse(sessionInput.text);
            bool isFirstSession = true;

            DeliveryExperiment.ConfigureExperiment(useRamulatorToggle.isOn, useNiclsToggle.isOn, useElememToggle.isOn,
                                                    sessionNumber, experiment_name, isFirstSession);
            SceneManager.LoadScene(scene_name);
        #endif
    }

    private int NextSessionNumber()
    {
        string dataPath = UnityEPL.GetParticipantFolder();
        System.IO.Directory.CreateDirectory(dataPath);
        Debug.Log(dataPath);
        string[] sessionFolders = System.IO.Directory.GetDirectories(dataPath);
        int mostRecentSessionNumber = -1;
        foreach (string folder in sessionFolders)
        {
            int thisSessionNumber = -1;
            if (int.TryParse(folder.Substring(folder.LastIndexOf('_')+1), out thisSessionNumber) && thisSessionNumber > mostRecentSessionNumber)
                mostRecentSessionNumber = thisSessionNumber;
        }
        return mostRecentSessionNumber + 1;
    }

    private bool IsValidParticipantName(string name)
    {

        if (name.Length < 1) {
            return false;
        }
        return true;

        // bool isTest = name.Equals("TEST");
        // if (isTest)
        //     return true;

        // if (name.Length != 6)
        //     return false;

        // bool isValidRAMName = name[0].Equals('R') && name[1].Equals('1') && char.IsDigit(name[2]) && char.IsDigit(name[3]) && char.IsDigit(name[4]) && char.IsUpper(name[5]);
        // bool isValidSCALPName = char.IsUpper(name[0]) && char.IsUpper(name[1]) && char.IsUpper(name[2]) && char.IsDigit(name[3]) && char.IsDigit(name[4]) && char.IsDigit(name[5]);
        // Debug.Log(isValidSCALPName);
        // return isValidRAMName || isValidSCALPName;
    }
}