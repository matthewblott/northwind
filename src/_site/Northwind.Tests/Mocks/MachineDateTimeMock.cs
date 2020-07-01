namespace Northwind.Tests.Mocks
{
  using System;
  using Common;
  using FakeItEasy;

  public class MachineDateTimeMock
  {
    public static IDateTime Instance
    {
      get
      {
        var provider = A.Fake<IDateTime>();
        
        A.CallTo(() => provider.Now).Returns(new DateTime(2020,5, 15));

        return provider;

      }
    }
    
  }
}