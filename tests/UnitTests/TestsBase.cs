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
    public  IFixture Fixture { get; }
    
    public TestsBase()
    {
        Fixture = new Fixture();
        Fixture.Customize(new AutoNSubstituteCustomization( ){ConfigureMembers = true});
    }
}