using Coldairarrow.Entity.Hkv;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Hkv
{
    public interface IT_VideoBusiness
    {
        List<T_Video> GetDataList(Pagination pagination, string condition, string keyword);

        List<T_Video> GetDataAll();

        T_Video GetTheData(int id);
        AjaxResult AddData(T_Video data);
        AjaxResult UpdateData(T_Video data);
        AjaxResult DeleteData(List<string> ids);
    }
}