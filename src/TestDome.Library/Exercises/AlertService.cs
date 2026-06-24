namespace TestDome.Library.Exercises;
using System.Collections.Generic;
using System;

public class AlertService
{
    public IAlertDAO AlertDAO { get; set; }

    private readonly AlertDAO storage = new AlertDAO();


    public AlertService(IAlertDAO storage)
    {
        AlertDAO = storage;
    }

    public Guid RaiseAlert()
    {
        return this.storage.AddAlert(DateTime.Now);
    }

    public DateTime GetAlertTime(Guid id)
    {
        return this.storage.GetAlert(id);
    }
}

public class AlertDAO : IAlertDAO
{
    private readonly Dictionary<Guid, DateTime> alerts = new Dictionary<Guid, DateTime>();

    public Guid AddAlert(DateTime time)
    {
        Guid id = Guid.NewGuid();
        this.alerts.Add(id, time);
        return id;
    }

    public DateTime GetAlert(Guid id)
    {
        return this.alerts[id];
    }
}
