// <copyright file="JobContextTest.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.Data.DataAccess.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JobContextTest
    {
        /*
        [TestMethod]
        public void GetExperimentsTest()
        {
            var builder = new DbContextOptionsBuilder<EntityContext>();
            builder.UseInMemoryDatabase("local");

            var options = builder.Options;

            using (var context = new EntityContext())
            {
                var modules = new List<Module>
                    {
                        new Module() { Name = "Module1" },
                        new Module() { Name = "Module2" },
                        new Module() { Name = "Module3" }
                    };

                context.AddRange(modules);
                context.SaveChanges();
            }

            using (var context = new EntityContext(options))
            {
                var experiment = new Experiment() { Name = "Exp1", Jobs = new List<Job>() };

                var module1 = context.Modules.FirstOrDefault(m => m.Name == "Module1");
                var module2 = context.Modules.FirstOrDefault(m => m.Name == "Module2");
                var module3 = context.Modules.FirstOrDefault(m => m.Name == "Module3");

                var job1 = new Job()
                               {
                                   State = State.New,
                                   ModuleId = module1.Id
                               };
                context.Jobs.Add(job1);

                var job2 = new Job()
                               {
                                   State = State.New,
                                   ModuleId = module2.Id
                               };
                context.Jobs.Add(job2);

                var job3 = new Job()
                               {
                                   State = State.New,
                                   ModuleId = module3.Id
                               };
                context.Jobs.Add(job3);

                experiment.Jobs.Add(job1);
                experiment.Jobs.Add(job2);
                experiment.Jobs.Add(job3);
                context.Experiments.Add(experiment);

                context.SaveChanges();
            }

            using (var context = new EntityContext(options))
            {
                context.Experiments.Load();
                context.Modules.Load();
                context.Jobs.Load();

                var experiment = context.Experiments.FirstOrDefault(e => e.Name == "Exp1");
                Assert.IsNotNull(experiment);
                Assert.IsTrue(experiment.Jobs.Count == 3);
                Assert.IsTrue(experiment.Jobs.Any(j => j.Module.Name == "Module1"));
            }
        }*/
    }
}
