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

namespace Infotecs.Attika.AttikaSpecs.AddArticleComment
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("AddArticleComment")]
    public partial class AddArticleCommentFeature
    {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "AddArticleComment.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "AddArticleComment", "In order to add comment to article\r\nAs a nasty commenter\r\nI want to see my commen" +
                "t seen to others", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Add valid comment")]
        public virtual void AddValidComment()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add valid comment", ((string[])(null)));
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("I got working article service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
            testRunner.And("I have article with valid id in store", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
            testRunner.When("this service recieved \"addcomment request with valid id\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
            testRunner.And("service handles \"addcomment request with valid id\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
            testRunner.Then("this comment should be seen in valid article", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add invalid comment")]
        public virtual void AddInvalidComment()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add invalid comment", ((string[])(null)));
#line 13
            this.ScenarioSetup(scenarioInfo);
#line 14
            testRunner.Given("I got working article service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
            testRunner.And("I have article with valid id in store", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
            testRunner.When("this service recieved \"addcomment request with invalid text\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
            testRunner.And("service handles \"addcomment request with invalid text\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
            testRunner.Then("this comment should not be stored", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion