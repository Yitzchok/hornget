using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.Dsl;
using horn.services.core.Value;
using Horn.Spec.Framework;
using Horn.Spec.Framework.helpers;
using NUnit.Framework;

namespace Horn.Services.Core.Tests.Unit.PackageSpecs
{
    public class When_mapping_a_build_package_meta_data_to_a_value_object : ContextSpecification 
    {
        private Package _package;

        private IBuildMetaData _buildMetaData;

        protected override void establish_context()
        {
            _buildMetaData = TreeHelper.GetPackageTreeParts(new List<Dependency>());

            _buildMetaData.InstallName = "Nhibernate";

            _buildMetaData.Version = "trunk";

            _buildMetaData.ProjectInfo.Add("forum", "http://groups.google.co.uk/group/nhusers?hl=en");
        }

        protected override void because()
        {            
            _package = new Package(_buildMetaData);
        }

        [Test]
        public void Then_the_value_object_is_created()
        {
            Assert.That(_package.Name, Is.EqualTo("Nhibernate"));

            Assert.That(_package.Version, Is.EqualTo("trunk"));

            Assert.That(_package.MetaData.Count, Is.GreaterThan(0));
        }
    }
}