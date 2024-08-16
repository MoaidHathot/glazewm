using GlazeWM.Infrastructure.Bussing;
using Monitor = GlazeWM.Domain.Monitors.Monitor;

namespace GlazeWM.Domain.Workspaces.Commands;

internal sealed class ActivateWorkspaceCommand : Command
{
  public string WorkspaceName { get; }
  public Monitor TargetMonitor { get; }

  public ActivateWorkspaceCommand(string workspaceName, Monitor targetMonitor)
  {
    WorkspaceName = workspaceName;
    TargetMonitor = targetMonitor;
  }
}
