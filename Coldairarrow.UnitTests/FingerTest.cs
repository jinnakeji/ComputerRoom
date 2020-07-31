using System;
using System.Collections.Generic;
using System.Text;
using libzkfpcsharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Coldairarrow.UnitTests
{
    [TestClass]
    public class FingerTest
    {
        [TestMethod]
        public void Test()
        {
            zkfp2.Init();
            var handler = libzkfpcsharp.zkfp2.DBInit();
            var dir = @"G:\project\outsource\luke\Computer\Coldairarrow.Api\新建文件夹";

            var temp1 = File.ReadAllText(dir + "\\temp1.txt");
            var temp2 = File.ReadAllText(dir + "\\temp2.txt");
            var temp3 = File.ReadAllText(dir + "\\temp3.txt");

            var arr1 = zkfp2.Base64ToBlob(temp1);
            var arr2 = zkfp2.Base64ToBlob(temp2);
            var arr3 = zkfp2.Base64ToBlob(temp3);


            byte[] regTemp = new byte[2048];
            int len = 0;
            var aa = zkfp2.DBMerge(handler, arr1, arr2, arr3, regTemp, ref len);

            zkfp2.DBAdd(handler, 1, new byte[] { });
        }
    }
}
