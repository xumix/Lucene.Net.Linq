﻿using System.Linq;
using NUnit.Framework;

namespace Lucene.Net.Linq.Tests.Integration
{
    [TestFixture]
    public class WhereTests : IntegrationTestBase
    {
        [SetUp]
        public void AddDocuments()
        {
            AddDocument("0", "apple");
            AddDocument("1", "banana");
            AddDocument("2", "cherries are red");
        }

        [Test]
        public void IdEquals()
        {
            var result = from doc in provider.AsQueryable() where doc.Get("id") == "0" select doc.Get("id");

            Assert.That(result.FirstOrDefault(), Is.EqualTo("0"));
        }

        [Test]
        public void TextEquals()
        {
            var result = from doc in provider.AsQueryable() where doc.Get("text") == "banana" select doc.Get("id");

            Assert.That(result.FirstOrDefault(), Is.EqualTo("1"));
        }

        [Test]
        public void TextEqualsExactMatch()
        {
            var result = from doc in provider.AsQueryable() where doc.Get("text") == "cherry is red" select doc.Get("id");

            Assert.That(result.FirstOrDefault(), Is.Null);
        }

        [Test]
        public void ContainsExactPhrase()
        {
            var result = from doc in provider.AsQueryable() where doc.Get("text").Contains("cherries are red") select doc.Get("id");

            Assert.That(result.FirstOrDefault(), Is.EqualTo("2"));
        }

        [Test, Ignore("TODO")]
        public void ContainsWord()
        {
            var result = from doc in provider.AsQueryable() where doc.Get("text").Contains("cherries") select doc.Get("id");

            Assert.That(result.FirstOrDefault(), Is.EqualTo("2"));
        }
    }


}