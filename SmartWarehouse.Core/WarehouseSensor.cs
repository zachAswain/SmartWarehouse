namespace SmartWarehouse.Core;

public class WarehouseSensor
{
    public string SensorId { get; private set; }
    public string LocationTag { get; set; }
    public double CurrentTemperature { get; set; }
    public bool IsActive { get; set; }
    public bool IsAlertTriggered { get; private set; }
    public double CriticalThresholdCelsius { get; set; }

    public WarehouseSensor(string sensorId, string locationTag, double criticalThresholdCelsius = 4.0)
    {
        if (string.IsNullOrWhiteSpace(sensorId))
        {
            throw new ArgumentException("SensorId is required");
        }
        if (string.IsNullOrWhiteSpace(locationTag))
        {
            throw new ArgumentException("LocationTag is required");
        }
        
        SensorId = sensorId;
        LocationTag = locationTag;
        CriticalThresholdCelsius = criticalThresholdCelsius;
        IsActive = false;
        IsAlertTriggered = false;
        CurrentTemperature = 0.0;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        IsAlertTriggered = false;
    }

    public void RecordReading(double newTemperature)
    {
        if (!IsActive) throw new InvalidOperationException();
        
        if (newTemperature < -50.0 || newTemperature > 80.0) throw new ArgumentOutOfRangeException();
        
        CurrentTemperature = newTemperature;
        
        IsAlertTriggered = newTemperature >= CriticalThresholdCelsius;
    }
}