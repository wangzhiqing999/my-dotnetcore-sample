using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

using MyFramework.Service;

namespace MyFramework.ServiceImpl
{

    public abstract class DefaultCommonService : ICommonService
    {





        /// <summary>
        /// 更新属性值.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uiData"></param>
        /// <param name="dbData"></param>
        protected void UpdateProperty<T>(T uiData, T dbData)
        {
            // 对象类型.
            Type dataType = uiData.GetType();
            // 对象的所有属性.
            PropertyInfo[] dataPropArray = dataType.GetProperties();

            // 遍历所有的属性.
            foreach (PropertyInfo dataProp in dataPropArray)
            {
                if (!dataProp.CanRead || !dataProp.CanWrite)
                {
                    // 忽略掉 只读 / 只写 的属性.
                    continue;
                }

                if (dataProp.Name == "CreateUser" || dataProp.Name == "CreateTime")
                {
                    // 忽略 创建人 / 创建时间
                    continue;
                }

                //if (dataProp.Name == "LastUpdateUser" || dataProp.Name == "LastUpdateTime")
                //{
                //    // 忽略  最后更新人 / 最后更新时间.
                //    continue;
                //}

                if (dataProp.CustomAttributes.Count(p => p.AttributeType.Name == "ColumnAttribute") == 0)
                {
                    // 仅仅处理有  [Column("列名")]  这样的标记的.
                    // 其他的忽略.
                    continue;
                }

                if (dataProp.CustomAttributes.Count(p => p.AttributeType.Name == "KeyAttribute") > 0)
                {
                    // 遇到有 [Key]  这样的标记的， 忽略掉. 
                    // 因为主键列是不更新的.
                    continue;
                }

                // 获取数据.
                Object uiDataVal = dataProp.GetValue(uiData, null);

                // 填写数据.
                dataProp.SetValue(dbData, uiDataVal);
            }
        }


    }

}
