using Coldairarrow.Entity.Hkv;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Hkv
{
    public interface IHkVideoBusiness
    {
        //List<HkVideo> GetDataList(Pagination pagination, string condition, string keyword);
        //HkVideo GetTheData(string id);
        List<HkVideoDTO> GetDataList(Pagination pagination, string condition, string keyword);
        HkVideo GetTheData(string id);
        AjaxResult AddData(HkVideo data);
        AjaxResult UpdateData(HkVideo data);
        AjaxResult DeleteData(List<string> ids);
    }
}