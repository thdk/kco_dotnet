﻿#region Copyright Header
// ----------------------------------------------------------------------------
// <copyright file="OrderTest.cs" company="Klarna AB">
//     Copyright 2012 Klarna AB
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//         http://www.apache.org/licenses/LICENSE-2.0
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
// <author>Klarna Support: support@klarna.com</author>
// <link>http://integration.klarna.com/</link>
// ----------------------------------------------------------------------------
#endregion
namespace Klarna.Checkout.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// The order test.
    /// </summary>
    [TestFixture]
    public class OrderTest
    {
        #region Private Fields

        /// <summary>
        /// Data used in tests
        /// </summary>
        private const int TheInt = 89;

        /// <summary>
        /// The the string.
        /// </summary>
        private const string TheString = "A string";

        /// <summary>
        /// The the date time.
        /// </summary>
        private readonly DateTime theDateTime = new DateTime(2012, 10, 14, 22, 53, 12);

        /// <summary>
        /// The order.
        /// </summary>
        private Order order;

        #endregion

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            order = new Order();
        }

        /// <summary>
        /// Tests that the content type is correct.
        /// </summary>
        [Test]
        public void ContentType()
        {
            Assert.That(order.ContentType, Is.EqualTo("application/vnd.klarna.checkout.aggregated-order-v1+json"));
        }

        /// <summary>
        /// Tests that the location not is initialized.
        /// </summary>
        [Test]
        public void LocationNull()
        {
            Assert.That(order.Location, Is.Null);
        }

        /// <summary>
        /// Tests set/get location.
        /// </summary>
        [Test]
        public void LocationSetGet()
        {
            const string Url = "http://klarna.com";
            order.Location = new Uri(Url);
            Assert.That(order.Location, Is.EqualTo(new Uri(Url)));
        }

        /// <summary>
        /// Tests that parse works correctly.
        /// </summary>
        public void Parse()
        {
            var newData = new Dictionary<string, object>
                {
                   { "Int", TheInt }, { "String", TheString }, { "DateTime", theDateTime } 
                };

            order.Parse(newData);
            var data = order.Marshal();
            Assert.That(data, Is.TypeOf<Dictionary<string, object>>());
            Assert.That(data["Int"], Is.TypeOf<int>());
            Assert.That((int)data["Int"], Is.EqualTo(TheInt));
            Assert.That(data["String"], Is.TypeOf<string>());
            Assert.That((string)data["String"], Is.EqualTo(TheString));
            Assert.That(data["DateTime"], Is.TypeOf<DateTime>());
            Assert.That((DateTime)data["DateTime"], Is.EqualTo(theDateTime));
        }

        /// <summary>
        /// Tests that marshal works correctly.
        /// </summary>
        [Test]
        public void Marshal()
        {
            order.SetValue("Int", TheInt);
            order.SetValue("String", TheString);
            order.SetValue("DateTime", theDateTime);

            var data = order.Marshal();
            Assert.That(data, Is.TypeOf<Dictionary<string, object>>());
            Assert.That(data["Int"], Is.TypeOf<int>());
            Assert.That((int)data["Int"], Is.EqualTo(TheInt));
            Assert.That(data["String"], Is.TypeOf<string>());
            Assert.That((string)data["String"], Is.EqualTo(TheString));
            Assert.That(data["DateTime"], Is.TypeOf<DateTime>());
            Assert.That((DateTime)data["DateTime"], Is.EqualTo(theDateTime));
        }

        /// <summary>
        /// Tests set/get values.
        /// </summary>
        [Test]
        public void ValuesSetGet()
        {
            order.SetValue("Int", TheInt);
            order.SetValue("String", TheString);
            order.SetValue("DateTime", theDateTime);

            var intData = order.GetValue("Int");
            Assert.That(intData, Is.TypeOf<int>());
            Assert.That((int)intData, Is.EqualTo(TheInt));

            var stringData = order.GetValue("String");
            Assert.That(stringData, Is.TypeOf<string>());
            Assert.That((string)stringData, Is.EqualTo(TheString));

            var dateTimeData = order.GetValue("DateTime");
            Assert.That(dateTimeData, Is.TypeOf<DateTime>());
            Assert.That((DateTime)dateTimeData, Is.EqualTo(theDateTime));
        }

        /// <summary>
        /// Tests that set value with null key throws an exception.
        /// </summary>
        [Test]
        public void ValuesSetException()
        {
            Assert.Throws<ArgumentNullException>(() => order.SetValue(null, TheString));
        }

        /// <summary>
        /// Tests that get value with null key or a non-existing key throws an exception.
        /// </summary>
        [Test]
        public void ValuesGetExeption()
        {
            Assert.Throws<ArgumentNullException>(() => { var x = this.order.GetValue(null); });
            Assert.Throws<KeyNotFoundException>(() => { var x = this.order.GetValue("NonExistingKey"); });
        }
    }
}