using TestProduct.Enums;

namespace TestProduct.Models;

public class ActionLogModel
{
    public Guid Id { get; set; }
    public LogType LogType { get; set; }
    public DateTime TimeStamp { get; set; }
    public string? UserIp { get; set; }
}
