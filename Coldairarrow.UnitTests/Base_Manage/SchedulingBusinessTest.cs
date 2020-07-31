using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.UnitTests
{
    [TestClass]
    public class SchedulingBusinessTest
    {
        SchedulingBusiness schedulingBusiness;

        public SchedulingBusinessTest()
        {
            schedulingBusiness = new SchedulingBusiness();
        }

        [TestMethod]
        public void Test()
        {
            schedulingBusiness.AddData(new Scheduling()
            {
                CreateTime = DateTime.Now,
                CreatorId = Guid.NewGuid().ToString(),
                CreatorRealName = "张三",
                Id = Guid.NewGuid().ToString(),
                OfficeDate = DateTime.Now,
                ShiftsId = null,
                TeamTableId = null
            },
                new string[] { "2020-05-19", "2020-05-31" },
                new string[] {
                    "1242346508381065216",
                    "1242404620207132672",
                    "1242629051986743296"
                },
                new string[] {
                    "1242434583920644096",
                    "1242435913695367168",
                    "1242360779714334720"
                },
                new string[] { "6", "7" });
        }
    }
}
