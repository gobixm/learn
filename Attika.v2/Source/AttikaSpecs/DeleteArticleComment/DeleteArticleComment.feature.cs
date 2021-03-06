﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34209
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

#region Designer generated code

using System;
using TechTalk.SpecFlow;

#pragma warning disable

namespace Infotecs.Attika.AttikaSpecs.DeleteArticleComment
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("DeleteArticleComment")]
    public partial class DeleteArticleCommentFeature
    {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "DeleteArticleComment.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "DeleteArticleComment", "In order to delete nasty comments\r\nAs a anministrator\r\nI want to remove comment f" +
                "rom article", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }

        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }

        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }

        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }

        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Delete comment")]
        public virtual void DeleteComment()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete comment", ((string[])(null)));
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("I got working article service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
            testRunner.And("I have article with valid id in store", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
            testRunner.When("this service recieved \"delete article comment request\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
            testRunner.And("service handles \"delete article comment request\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
            testRunner.Then("this comment should dissaper from article scope", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion
