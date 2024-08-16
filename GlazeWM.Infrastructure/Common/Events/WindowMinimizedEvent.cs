using GlazeWM.Infrastructure.Bussing;

namespace GlazeWM.Infrastructure.Common.Events;

public record WindowMinimizedEvent(IntPtr WindowHandle)
  : Event(InfraEvent.WindowMinimized);
