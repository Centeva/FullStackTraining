using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetArchTest.Rules;

namespace FullStackTraining.UnitTests.Architecture;

public class ProjectDependencies
{
    [Fact]
    public void CoreShouldNotHaveDependencies()
    {
        var result = Types
            .InNamespace("FullStackTraining.Core")
            .ShouldNot()
            .HaveDependencyOnAny("FullStackTraining.Infrastructure", "FullStackTraining.Application")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationShouldNotDependOnInfrastructure()
    {
        var result = Types
            .InNamespace("FullStackTraining.Application")
            .ShouldNot()
            .HaveDependencyOn("FullStackTraining.Infrastructure")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void InfrastructureShouldDependOnApplication()
    {
        var result = Types
            .InNamespace("FullStackTraining.Core")
            .Should()
            .HaveDependencyOn("FullStackTraining.Application")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void WebApiShouldDependOnInfrastructureAndApplication()
    {
        var result = Types
            .InNamespace("FullStackTraining.WebApi")
            .Should()
            .HaveDependencyOnAll("FullStackTraining.Application", "FullStackTraining.Infrastructure")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
