using System.Diagnostics.CodeAnalysis;
using AutoFixture;

namespace UnitTests;

[ExcludeFromCodeCoverage]
public abstract class TestsBase
{
    public  IFixture Fixture { get; }
    
    public TestsBase()
    {
        Fixture = new Fixture();
    }
}