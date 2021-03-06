﻿#region Copyright Header
// ----------------------------------------------------------------------------
// <copyright file="Digest.cs" company="Klarna AB">
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
// <link>http://developers.klarna.com/</link>
// ----------------------------------------------------------------------------
#endregion
namespace Klarna.Checkout
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// This class is used to create a digest.
    /// </summary>
    public class Digest
    {
        /// <summary>
        /// Creates a digest from a string..
        /// </summary>
        /// <param name="data">
        /// The input data.
        /// </param>
        /// <returns>
        /// The digest <see cref="string"/>.
        /// </returns>
        public string Create(string data)
        {
            using (HashAlgorithm algorithm = new SHA256Managed())
            {
                var bytes = Encoding.GetEncoding("UTF-8").GetBytes(data);
                var hash = algorithm.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
