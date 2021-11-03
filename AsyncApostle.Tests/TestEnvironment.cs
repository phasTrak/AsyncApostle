using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;
using static System.Threading.ApartmentState;

[assembly: Apartment(STA)]

[ZoneDefinition]
public class TestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone> { }

[SetUpFixture]
public class ReSharperTestEnvironmentAssembly : ExtensionTestEnvironmentAssembly<TestEnvironmentZone> { }
