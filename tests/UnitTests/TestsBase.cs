using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Domain.Repositories;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NSubstitute.Extensions;

namespace UnitTests;

[ExcludeFromCodeCoverage]
public abstract class TestsBase
{
    public readonly IFixture _fixture;
    
    public TestsBase()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoNSubstituteCustomization( ){ConfigureMembers = true});
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}