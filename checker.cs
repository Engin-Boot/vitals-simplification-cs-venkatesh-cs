using System;
using System.Diagnostics;

internal class Checker
{
    //Convection followed: lower, upper
    private readonly int[] _spo2Limits  = new int[] {90 , 100};
    private readonly int[] _respRateLimits = new int[] {30 , 95};
    private readonly int[] _bpmLimits = new int[] {70 , 150};

    public bool CheckVitals(float bpm, float spo2, float respRate)
    {
        return (
            IsVitalInLimits(_spo2Limits[0], _spo2Limits[1], spo2, "spo2") && 
            IsVitalInLimits(_respRateLimits[0], _respRateLimits[1], respRate, "RespRate") &&
            IsVitalInLimits(_bpmLimits[0], _bpmLimits[1], bpm, "BPM") );

    }

    private bool IsVitalInLimits(float lowerLimit, float upperLimit, float value, string parameter)
    {
        if (!(value > upperLimit) && !(value < lowerLimit)) return true;
        string warningMessage = "Warning!!!!" + parameter + " value is out of range!" + parameter + " = " + value;
        Warn(warningMessage);
        return false;
    }

    private void Warn(string warningMessage)
    {
        Console.WriteLine(warningMessage);
    }

    public static int Main() 
    {
        var obj = new  Checker();
        Debug.Assert( obj.CheckVitals(75,99,65) == true);
        
        //BPM out of range
        Debug.Assert( obj.CheckVitals(60,99,65) == false);
        Debug.Assert( obj.CheckVitals(10,99,65) == false);

        //SpO2 out of range
        Debug.Assert(obj.CheckVitals(75, 80,65) == false);
        Debug.Assert(obj.CheckVitals(75, 110,65) == false);

        // Respiratory Rate out of range
        Debug.Assert(obj.CheckVitals(75,99,20));
        Debug.Assert(obj.CheckVitals(75,99,100));

        Debug.Assert(obj.IsVitalInLimits(10,100,20,"Check1") == true);
        Debug.Assert(obj.IsVitalInLimits(12.3f, 15.4f, 12.299f, "Check2") == false);

        System.Console.WriteLine("Reached the end, all tests passing");
        return 0;
    }
}

