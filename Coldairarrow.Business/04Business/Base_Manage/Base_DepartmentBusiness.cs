using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_DepartmentBusiness : BaseBusiness<Base_Department>, IBase_DepartmentBusiness, IDependency
    {
        #region �ⲿ�ӿ�

        public List<Base_DepartmentTreeDTO> GetTreeDataList(string parentId = null)
        {
            var where = LinqHelper.True<Base_Department>();
            if (!parentId.IsNullOrEmpty())
                where = where.And(x => x.ParentId == parentId);
            var list = GetIQueryable().Where(where).ToList()
                .Select(x => new Base_DepartmentTreeDTO
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Text = x.Name,
                    Value = x.Id
                }).ToList();

            return TreeHelper.BuildTree(list);
        }

        public List<string> GetChildrenIds(string departmentId)
        {
            var allNode = GetIQueryable().Select(x => new TreeModel
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Text = x.Name,
                Value = x.Id
            }).ToList();

            var children = TreeHelper
                .GetChildren(allNode, allNode.Where(x => x.Id == departmentId).FirstOrDefault(), true)
                .Select(x => x.Id)
                .ToList();

            return children;
        }

        public Base_Department GetTheData(string id)
        {
            return GetEntity(id);
        }

        [DataRepeatValidate(new string[] { "Name" }, new string[] { "������" })]
        [DataAddLog(LogType.���Ź���, "Name", "������")]
        public AjaxResult AddData(Base_Department newData)
        {
            Insert(newData);

            return Success();
        }

        [DataRepeatValidate(new string[] { "Name" }, new string[] { "������" })]
        [DataEditLog(LogType.���Ź���, "Name", "������")]
        public AjaxResult UpdateData(Base_Department theData)
        {
            Update(theData);

            return Success();
        }

        [DataDeleteLog(LogType.���Ź���, "Name", "������")]
        public AjaxResult DeleteData(List<string> ids)
        {
            if (GetIQueryable().Any(x => ids.Contains(x.ParentId)))
                return Error("��ֹɾ��������ɾ�������Ӽ���");

            Delete(ids);

            return Success();
        }
        /// <summary>
        /// ͨ���û�ID��ȡ ����
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Base_Department> GetDepartMentList(string userid)
        {
            Base_User quer = Service.GetIQueryable<Base_User>().Where(x => x.Id == userid).FirstOrDefault() ; 
            var allNode = Service.GetIQueryable<Base_Department>().Where(q => q.ParentId == quer.DepartmentId)
                .Select(s=> new Base_Department
                {
                    Id = s.Id,
                    ParentId = s.ParentId,
                    Name = s.Name
                }).ToList(); 
            return allNode;
        }

        #endregion

        #region ˽�г�Ա

        #endregion
    }

    public class Base_DepartmentTreeDTO : TreeModel
    {
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
    }
}