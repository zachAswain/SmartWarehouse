using SmartWarehouse.Core;

namespace SmartWarehouse.Tests;

[TestClass]
public sealed class WarehouseSensorTests
{
    [TestMethod]
    public void Constructor_Should_Initialize_Sensor()
    {    
        // Arrange
        string sensorId = "001";
        string locationTag = "Location 1";

        // Act
        WarehouseSensor sensor = new WarehouseSensor(sensorId, locationTag);

        // Assert
        Assert.AreEqual(sensorId, sensor.SensorId);
        Assert.AreEqual(locationTag, sensor.LocationTag);
        Assert.AreEqual(0.0, sensor.CurrentTemperature);
        Assert.IsFalse(sensor.IsActive);
        Assert.IsFalse(sensor.IsAlertTriggered);
        Assert.AreEqual(4.0, sensor.CriticalThresholdCelsius);
    }

    [TestMethod]
    public void Constructor_Should_Throw_Exception_For_Empty_SensorId()
    {
        // Arrange
        string badSensorId = "";
        string locationTag = "Location 1";

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(() => new WarehouseSensor(badSensorId, locationTag));
    }

    [TestMethod]
    public void Constructor_Should_Throw_Exception_For_Null_SensorId()
    {
        // Arrange
        string? badSensorId = null;
        string locationTag = "Location 1";

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(() => new WarehouseSensor(badSensorId!, locationTag));
    }

    [TestMethod]
    public void Constructor_Should_Throw_Exception_For_Whitespace_SensorId()
    {
        // Arrange
        string badSensorId = "   ";
        string locationTag = "Location 1";

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(() => new WarehouseSensor(badSensorId, locationTag));
    }

    [TestMethod]
    public void Constructor_Should_Throw_Exception_For_Empty_LocationTag()
    {
        // Arrange
        string sensorId = "001";
        string badLocationTag = "";

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(() => new WarehouseSensor(sensorId, badLocationTag));
    }

    [TestMethod]
    public void Constructor_Should_Throw_Exception_For_Null_LocationTag()
    {
        // Arrange
        string sensorId = "001";
        string? badLocationTag = null;

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(() => new WarehouseSensor(sensorId, badLocationTag!));
    }

    [TestMethod]
    public void Constructor_Should_Throw_Exception_For_Whitespace_LocationTag()
    {
        // Arrange
        string sensorId = "001";
        string badLocationTag = "   ";

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(() => new WarehouseSensor(sensorId, badLocationTag));
    }

    [TestMethod]
    public void Activate_Should_Set_IsActive_To_True()
    {
        // Arrange
        WarehouseSensor sensor = new WarehouseSensor("001", "Location 1");

        // Act
        sensor.Activate();

        // Assert
        Assert.IsTrue(sensor.IsActive);
    }

    [TestMethod]
    public void Activate_Again_Should_Stay_Active()
    {
        // Arrange
        WarehouseSensor sensor = new WarehouseSensor("001", "Location 1");

        // Act
        sensor.Activate();
        sensor.Activate();

        // Assert
        Assert.IsTrue(sensor.IsActive);
    }

    [TestMethod]
    public void Deactivate_Should_Set_IsActive_And_IsAlertTriggered_To_False()
    {
        // Arrange
        WarehouseSensor sensor = new WarehouseSensor("001", "Location 1");
        sensor.Activate();
        sensor.RecordReading(25.0);
        Assert.IsTrue(sensor.IsAlertTriggered);

        // Act
        sensor.Deactivate();

        // Assert
        Assert.IsFalse(sensor.IsActive);
        Assert.IsFalse(sensor.IsAlertTriggered);
    }

    [TestMethod]
    public void RecordReading_While_Inactive_Should_Throw_Exception()
    {
        // Arrange
        WarehouseSensor sensor = new WarehouseSensor("001", "Location 1");

        // Act and Assert
        Assert.ThrowsExactly<InvalidOperationException>(() => sensor.RecordReading(10.0));
    }


}
