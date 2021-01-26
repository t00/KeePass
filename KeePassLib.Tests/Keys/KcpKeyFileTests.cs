﻿using System;
using System.IO;
using NUnit.Framework;
using KeePassLib.Keys;

namespace KeePassLib.Tests.Keys
{
    [TestFixture]
    public class KcpKeyFileTests
    {
        const string testCreateFile = "TestCreate.xml";
        const string testKey = "0123456789";

        const string expectedFileStart =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<KeyFile>\r\n" +
            "\t<Meta>\r\n" +
            "\t\t<Version>2.0</Version>\r\n" +
            "\t</Meta>\r\n" +
            "\t<Key>\r\n" +
            "\t\t<Data";

        const string expectedFileEnd = "\t</Key>\r\n" +
                                       "</KeyFile>";

        [Test]
        public void TestConstruct()
        {
            var expectedKeyData = new byte[32]
            {
                0xC1, 0xB1, 0x12, 0x77, 0x23, 0xB8, 0x99, 0xB8,
                0xB9, 0x3B, 0x1B, 0xFF, 0x6C, 0xBE, 0xA1, 0x5B,
                0x8B, 0x99, 0xAC, 0xBD, 0x99, 0x51, 0x85, 0x95,
                0x31, 0xAA, 0x14, 0x3D, 0x95, 0xBF, 0x63, 0xFF
            };

            var fullPath = Path.GetFullPath(testCreateFile);
            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(expectedFileStart);
                    sw.Write($">{testKey}</data>");
                    sw.Write(expectedFileEnd);
                }
            }

            try
            {
                var keyFile = new KcpKeyFile(fullPath);
                var keyData = keyFile.KeyData.ReadData();
                // Assert.That(keyData, Is.EqualTo(expectedKeyData));
            }
            finally
            {
                File.Delete(fullPath);
            }
        }

        [Test]
        public void TestCreate()
        {
            var fullPath = Path.GetFullPath(testCreateFile);
            KcpKeyFile.Create(fullPath, null);
            try
            {
                var fileContents = File.ReadAllText(fullPath);
                Assert.True(fileContents.StartsWith(expectedFileStart.Replace("\r\n", Environment.NewLine)));
                Assert.True(fileContents.EndsWith(expectedFileEnd.Replace("\r\n", Environment.NewLine)));
            }
            finally
            {
                File.Delete(fullPath);
            }
        }
    }
}