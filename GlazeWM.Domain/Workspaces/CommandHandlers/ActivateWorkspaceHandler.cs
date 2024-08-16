using GlazeWM.Domain.Common.Enums;
using GlazeWM.Domain.Containers.Commands;
using GlazeWM.Domain.Workspaces.Commands;
using GlazeWM.Domain.Workspaces.Events;
using GlazeWM.Infrastructure.Bussing;

namespace GlazeWM.Domain.Workspaces.CommandHandlers;

internal sealed class ActivateWorkspaceHandler : ICommandHandler<ActivateWorkspaceCommand>
{
  private readonly Bus _bus;

  public ActivateWorkspaceHandler(Bus bus)
  {
    _bus = bus;
  }

  public CommandResponse Handle(ActivateWorkspaceCommand command)
  {
    var workspaceName = command.WorkspaceName;
    var targetMonitor = command.TargetMonitor;

    var tilingDirection = targetMonitor.Height > targetMonitor.Width
      ? TilingDirection.Vertical
      : TilingDirection.Horizontal;

    var newWorkspace = new Workspace(workspaceName, tilingDirection);

    // Attach the created workspace to the specified monitor.
    _bus.Invoke(new AttachContainerCommand(newWorkspace, targetMonitor));
    _bus.Emit(new WorkspaceActivatedEvent(newWorkspace));

    return CommandResponse.Ok;
  }
}
