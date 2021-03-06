#region Copyright Header
// ----------------------------------------------------------------------------
// <copyright file="Update.cs" company="Klarna AB">
//     Copyright 2013 Klarna AB
//
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
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
// [[examples-update]]
namespace Klarna.Kco.Examples
{
    using System;
    using System.Collections.Generic;

    using Klarna.Checkout;

    /// <summary>
    /// The update checkout example.
    /// </summary>
    public class Update
    {
        /// <summary>
        /// The example.
        /// </summary>
        public void Example()
        {
            const string ContentType =
                "application/vnd.klarna.checkout.aggregated-order-v2+json";

            Uri resourceUri = new Uri("https://checkout.testdrive.klarna.com/checkout/orders/ABC123");

            // Cart
            var cartItems = new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                            {
                                { "reference", "123456789" },
                                { "name", "Klarna t-shirt" },
                                { "quantity", 2 },
                                { "unit_price", 12300 },
                                { "discount_rate", 1000 },
                                { "tax_rate", 2500 }
                            },
                        new Dictionary<string, object>
                            {
                                { "type", "shipping_fee" },
                                { "reference", "SHIPPING" },
                                { "name", "Shipping Fee" },
                                { "quantity", 1 },
                                { "unit_price", 4900 },
                                { "tax_rate", 2500 }
                            }
                    };
            var cart = new Dictionary<string, object> { { "items", cartItems } };

            const string SharedSecret = "sharedSecret";
            var connector = Connector.Create(SharedSecret);

            Order order = new Order(connector, resourceUri)
                {
                    ContentType = ContentType
                };

            // Reset cart
            var data = new Dictionary<string, object> { { "cart", cart } };

            order.Update(data);
        }
    }
}
// [[examples-update]]
