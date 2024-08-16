namespace GlazeWM.Infrastructure.WindowsApi;

public class SystemTrayIconConfig
{
  public required string HoverText { get; init; }
  public required string IconResourceName { get; init; }
  public required Dictionary<string, Action> Actions { get; init; }
}
