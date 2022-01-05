namespace FaultIdentifier.MachineLearningModel;

public static class SkLearn {

    private static readonly double[,] coefficients = new double[,] {
       { 0.0113972 , -0.01466646,  0.00717755, -0.01592884,  0.13646105},
       { 0.01117098, -0.01459809,  0.00039438, -0.0168826 ,  0.13698422},
       { 0.01024814, -0.01212668, -0.0018921 , -0.01103386,  0.1346966 },
       {-0.00866123,  0.01371575,  0.01238371, -0.00192937, -0.11782316},
       {-0.01428717,  0.01722566, -0.00598082,  0.01888522, -0.13419396},
       {-0.00986793,  0.01044982, -0.01208273,  0.02688944, -0.15612477}
    };

    private static readonly double[] intercepts = new double[]
        { -1.23993567, -0.4794639, -0.13030279, 1.21611427, 0.5090545, 0.12453359 };

    public static int FaultType(List<double> dissolvedGasContent) {
        // Logistic regression using `coefficient * DGA + intercepts`
        List<double> weight = LinearMath.MatrixListDotProduct(coefficients, dissolvedGasContent);
        List<double> scores = LinearMath.ListArraySum(weight, intercepts);

        // Fault type is the maximum value in this list
        double max = -1;
        foreach(double score in scores) { if(score > max) { max = score; } }
        return (int)Math.Round(max);
    }
}

public static class LinearMath {

    public static List<double> MatrixListDotProduct(double[,] matrix, List<double> array) {
        List<double> dotProduct = new();
        int width = matrix.Length;
        int height = array.Count;

        for(int i = 0 ; i < width ; i++) {
            double sum = 0;
            for(int j = 0 ; j < height ; j++) { sum += matrix[i, j] * array[j]; }
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