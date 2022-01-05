using FaultIdentifier.MachineLearningModel;

namespace FaultIdentifier;

public sealed partial class MainWindow : Window {

    public MainWindow() {
        this.InitializeComponent();
    }

    private void IdentifyFaultButton_Click(object sender, RoutedEventArgs e) {
        // Create list of dissolved gas content to perform logistic regression
        // Info: logistic regression model is order sensitive
        List<double> dissolvedGasContent = new();
        dissolvedGasContent.Add(H2ContentNumberbox.Value);
        dissolvedGasContent.Add(CH4ContentNumberbox.Value);
        dissolvedGasContent.Add(C2H6ContentNumberbox.Value);
        dissolvedGasContent.Add(C2H4ContentNumberbox.Value);
        dissolvedGasContent.Add(C2H2ContentNumberbox.Value);

        // Update the gas contents in the results
        H2ResultTextbox.Text = $"H₂ content: {dissolvedGasContent[0]}";
        CH4ResultTextbox.Text = $"CH₄ content: {dissolvedGasContent[1]}";
        C2H6ResultTextbox.Text = $"C₂H₆ content: {dissolvedGasContent[2]}";
        C2H4ResultTextbox.Text = $"C₂H₄ content: {dissolvedGasContent[3]}";
        C2H2ResultTextbox.Text = $"C₂H₂ content: {dissolvedGasContent[4]}";

        // Update fault type
        int faultType = SkLearn.FaultType(dissolvedGasContent);
        List<string> faultInfo = StaticFunctions.FaultInfo(faultType);
        FaultTypeResultTextbox.Text = $"{faultInfo[0]} Fault";
        FaultDescriptionResultTextbox.Text = faultInfo[1];
        FaultAdditionalInfoResultTextbox.Text = $"{faultInfo[2]}";
    }
}