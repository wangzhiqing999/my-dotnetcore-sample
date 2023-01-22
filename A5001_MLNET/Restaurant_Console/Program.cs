﻿
// This file was auto-generated by ML.NET Model Builder. 

using Restaurant_Console;

// Create single instance of sample data from first line of dataset for model input
RestaurantViolationsPrediction.ModelInput sampleData = new RestaurantViolationsPrediction.ModelInput()
{
    InspectionType = @"Routine - Unscheduled",
    ViolationDescription = @"Inadequately cleaned or sanitized food contact surfaces",
};

// Make a single prediction on the sample data and print results
var predictionResult = RestaurantViolationsPrediction.Predict(sampleData);

Console.WriteLine("Using model to make single prediction -- Comparing actual RiskCategory with predicted RiskCategory from sample data...\n\n");


Console.WriteLine($"InspectionType: {@"Routine - Unscheduled"}");
Console.WriteLine($"ViolationDescription: {@"Inadequately cleaned or sanitized food contact surfaces"}");
Console.WriteLine($"RiskCategory: {@"Moderate Risk"}");


Console.WriteLine($"\n\nPredicted RiskCategory: {predictionResult.PredictedLabel}\n\n");
Console.WriteLine("=============== End of process, hit any key to finish ===============");
Console.ReadKey();
