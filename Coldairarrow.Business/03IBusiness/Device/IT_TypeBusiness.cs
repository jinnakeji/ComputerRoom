using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_TypeBusiness
    {
        List<T_Type> GetDataList(Pagination pagination, string condition, string keyword);

        List<T_Type> GetList();

        T_Type GetTheData(string id);

        AjaxResult AddData(T_Type data);

        AjaxResult UpdateData(T_Type data);

        AjaxResult DeleteData(List<string> ids);
    }
}