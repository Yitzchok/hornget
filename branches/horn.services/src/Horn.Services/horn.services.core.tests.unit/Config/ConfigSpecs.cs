using System;
using Horn.Core.Config;
using Horn.Spec.Framework;
using NUnit.Framework;

namespace Horn.Services.Core.Tests.Unit.Config
{
    public class When_reading_the_base_path_from_the_config : ContextSpecification
    {
        protected override void establish_context()
        {
        }

        protected override void because()
        {
        }

        [Test]
        public void Then_the_base_path_is_returned()
        {
            Assert.That(HornConfig.Settings.DropDirectory.Length, Is.GreaterThan(0));
        }
    }
}
