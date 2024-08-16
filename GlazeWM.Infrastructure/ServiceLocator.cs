using Microsoft.Extensions.DependencyInjection;

namespace GlazeWM.Infrastructure;

public static class ServiceLocator
{
  //Todo - null - moaid
  public static IServiceProvider Provider { private get; set; } = default!;

  public static T GetRequiredService<T>() where T : notnull
  {
    //Todo - null - moaid
    return Provider.GetRequiredService<T>();
  }

  public static object GetRequiredService(Type type)
  {
    return Provider.GetRequiredService(type);
  }

  public static IEnumerable<object> GetServices(Type type)
  {
    //Todo - null - moaid
    return Provider.GetServices(type)!;
  }

  public static (T1, T2) GetRequiredServices<T1, T2>()
    where T1 : notnull
    where T2 : notnull
  {
    var service1 = Provider.GetRequiredService<T1>();
    var service2 = Provider.GetRequiredService<T2>();
    return (service1, service2);
  }

  public static (T1, T2, T3) GetRequiredServices<T1, T2, T3>()
    where T1 : notnull
    where T2 : notnull
    where T3 : notnull
  {
    var service1 = Provider.GetRequiredService<T1>();
    var service2 = Provider.GetRequiredService<T2>();
    var service3 = Provider.GetRequiredService<T3>();
    return (service1, service2, service3);
  }
}
