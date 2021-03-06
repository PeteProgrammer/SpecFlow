﻿using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow.Generator;
using Should;
using TechTalk.SpecFlow.Generator.Interfaces;

namespace GeneratorTests
{
    [TestFixture]
    public class TestGeneratorFactoryTests : TestGeneratorTestsBase
    {
        private TestGeneratorFactory factory;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            factory = new TestGeneratorFactory();
        }

        [Test]
        public void GetGeneratorVersion_should_return_a_version()
        {
            factory.GetGeneratorVersion().ShouldNotBeNull();
        }

        [Test]
        public void Should_be_able_to_create_generator_with_default_config()
        {
            factory.CreateGenerator(net35CSProjectSettings).ShouldNotBeNull();
        }

        private class DummyGenerator : ITestGenerator
        {
            public TestGeneratorResult GenerateTestFile(FeatureFileInput featureFileInput, GenerationSettings settings)
            {
                throw new NotImplementedException();
            }

            public Version DetectGeneratedTestVersion(FeatureFileInput featureFileInput)
            {
                throw new NotImplementedException();
            }

            public string GetTestFullPath(FeatureFileInput featureFileInput)
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                //nop;
            }
        }

        [Test]
        public void Should_create_custom_generator_when_configured_so()
        {
            var configurationHolder = new SpecFlowConfigurationHolder(string.Format(@"
                <specFlow>
                  <generator>  
                  <dependencies>
                    <register type=""{0}"" as=""{1}""/>
                  </dependencies>
                  </generator>
                </specFlow>",
                typeof(DummyGenerator).AssemblyQualifiedName,
                typeof(ITestGenerator).AssemblyQualifiedName));

            var projectSettings = net35CSProjectSettings;
            projectSettings.ConfigurationHolder = configurationHolder;
            var generator = factory.CreateGenerator(projectSettings);
            generator.ShouldBeType(typeof(DummyGenerator));
        }
    }
}
