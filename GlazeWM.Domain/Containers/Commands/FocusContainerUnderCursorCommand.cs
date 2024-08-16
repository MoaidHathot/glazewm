using GlazeWM.Infrastructure.Bussing;

using Point = GlazeWM.Infrastructure.WindowsApi.Point;

namespace GlazeWM.Domain.Containers.Commands;

public class FocusContainerUnderCursorCommand : Command
{
  public Point TargetPoint { get; }
  public FocusContainerUnderCursorCommand(Point targetPoint)
  {
    TargetPoint = targetPoint;
  }
}
