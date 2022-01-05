namespace FaultIdentifier.MachineLearningModel;

public static class SkLearn {

    private static readonly double[,] coefficients = new double[,] {
       { 0.0113972 , -0.01466646,  0.00717755, -0.01592884,  0.13646105 },
       { 0.01117098, -0.01459809,  0.00039438, -0.0168826 ,  0.13698422 },
       { 0.01024814, -0.01212668, -0.0018921 , -0.01103386,  0.13469660 },
       {-0.00866123,  0.01371575,  0.01238371, -0.00192937, -0.11782316 },
       {-0.01428717,  0.01722566, -0.00598082,  0.01888522, -0.13419396 },
       {-0.00986793,  0.01044982, -0.01208273,  0.02688944, -0.15612477 }
    };

    private static readonly double[] intercepts = new double[]
       { -1.23993567, -0.4794639, -0.13030279, 1.21611427, 0.5090545, 0.12453359 };

    public static int FaultType(List<double> dissolvedGasContent) {
        // Logistic regression using `coefficient * DGA + intercepts`
        List<double> weight = LinearMath.MatrixListDotProduct(coefficients, dissolvedGasContent);
        List<double> scores = LinearMath.ListArraySum(weight, intercepts);

        // Fault type is the maximum value in this list
        double max = double.MinValue;
        foreach(double score in scores) { if(score > max) { max = score; } }
        return scores.IndexOf(max) + 1; // +1 because the fault types start from 1 in the .csv
    }
}

public static class LinearMath {

    public static List<double> MatrixListDotProduct(double[,] matrix, List<double> list) {
        List<double> dotProduct = new();
        const int width = 5;
        const int height = 6;

        for(int i = 0 ; i < height ; i++) {
            double sum = 0;
            for(int j = 0 ; j < width ; j++) { sum += matrix[i, j] * list[j]; }
            dotProduct.Add(sum);
        }

        return dotProduct;
    }

    public static List<double> ListArraySum(List<double> list, double[] array) {
        List<double> arrayArraySum = new();
        for(int i = 0 ; i < list.Count ; i++) { arrayArraySum.Add(list[i] + array[i]); }
        return arrayArraySum;
    }
}

public static class StaticFunctions {
    public static List<string> FaultInfo(int faultType) {
        List<string> faultInfo = new();
        switch(faultType) {
            case 1:
            faultInfo.Add("PD");
            faultInfo.Add("Low Energy Partial Discharge");
            faultInfo.Add("Partial discharge");
            break;

            case 2:
            faultInfo.Add("D1");
            faultInfo.Add("High Energy PD, Low Energy Discharge");
            faultInfo.Add("Continuous spark, partial discharge with tracking");
            break;

            case 3:
            faultInfo.Add("D2");
            faultInfo.Add("High Energy Discharge");
            faultInfo.Add("Arc with a high-energy density");
            break;

            case 4:
            faultInfo.Add("T1");
            faultInfo.Add("Low Thermal Fault");
            faultInfo.Add("Thermal fault 150-300 °C");
            break;

            case 5:
            faultInfo.Add("T2");
            faultInfo.Add("Medium Thermal Fault");
            faultInfo.Add("Thermal fault 300-700 °C, circulating current in windings");
            break;

            case 6:
            faultInfo.Add("T3");
            faultInfo.Add("High Thermal Fault");
            faultInfo.Add("Thermal fault > 700 °C, circulating current in core, overheated joints");
            break;

            default:
            new Exception("Unexpected fault type passed. Expected in range [1, 6]");
            return faultInfo;
        }

        return faultInfo;
    }

}